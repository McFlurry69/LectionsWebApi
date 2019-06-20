using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using Dapper;
using HelperLibrary.Models;
using static ConnectLibrary.DataAccess;
using static ConnectLibrary.LibrarySettings;
using static HelperLibrary.Tools;
using System.IO;
using ConnectLibrary.Logger;
using ConnectLibrary.SQLRepository.Interfaces;
using static ConnectLibrary.ValidationCheckClass;

namespace ConnectLibrary.SQLRepository
{
    public class CommonRepository : ICommonRepository
    {
        readonly IDbConnection _connection = Connection;
        
        public IEnumerable<CommonModel> FindAllStudentGrades()
        {
            using (_connection)
            {
                var result = _connection.Query<CommonModel>(
                    @"SELECT gr.Grade, gr.Date, lect.Le_Last_Name, sub.Title, st.St_First_Name, st.St_Last_Name
                                        FROM Grades gr
                                        INNER JOIN Students st ON gr.StudentId = st.Id
										INNER JOIN Lections lection ON lection.Id = gr.LectionId
										INNER JOIN Subjects sub ON lection.SubjectsId = sub.Id
										INNER JOIN Lecturers lect On lection.LecturersId = lect.Id");
                return result;
            }
        }
        
        public IEnumerable<CommonModel> GetLectionOfStudent(string name)
        {
            if (name == null)
            {
                Log.MakeLog(LoggerOperations.Error, "ArgumentNullException");
                throw new ArgumentNullException(nameof(name));
            }

            var result = FindAllStudentGrades();
            return from u in result
                where u.St_Last_Name == name
                select u;
        }
        
        public IEnumerable<CommonModel> GetStudentsOfLecturer(string subject, string lecturer)
        {
            if (subject == null)
            {
                Log.MakeLog(LoggerOperations.Error, "ArgumentNullException");
                throw new ArgumentNullException(nameof(subject));
            }

            if (lecturer == null)
            {
                Log.MakeLog(LoggerOperations.Error, "ArgumentNullException");
                throw new ArgumentNullException(nameof(lecturer));
            }
            ValidateSubject(subject);
            ValidateLecturer(lecturer);
            
            return from u in FindAllStudentGrades()
                    where u.Title == subject && u.Le_Last_Name == lecturer
                    select u;
        }
        
        public IEnumerable<string> GetAmountOfVisitedLections(string name)
        {
            if (name == null)
            {
                Log.MakeLog(LoggerOperations.Error, "ArgumentNullException");
                throw new ArgumentNullException(nameof(name));
            }

            var allLections = FindAllStudentGrades();

            var allVisitedLections = allLections
                .Where(students => students.St_Last_Name == name)
                .Select(students => students).GroupBy(lection => $"Lecturer: {lection.Le_Last_Name}, Subject: {lection.Title}");
            
            var amountOfLections = allLections
                .Select(students => students).GroupBy(lection => lection.Le_Last_Name).Distinct().Count();


            List<string> results = new List<string>();

            foreach (var groups in allVisitedLections)
            {
                results.Add($" {groups.Key} - visits: {groups.Select(count => count.Date).Distinct().Count()}/{amountOfLections}");
            }

            return results;
        }
        
        public IEnumerable<string> GetAmountOfVisitedLections(string subject, string lecturer)
        {
            if (subject == null)
            {
                Log.MakeLog(LoggerOperations.Error, "ArgumentNullException");
                throw new ArgumentNullException(nameof(subject));
            }

            if (lecturer == null)
            {
                Log.MakeLog(LoggerOperations.Error, "ArgumentNullException");
                throw new ArgumentNullException(nameof(lecturer));
            }
            ValidateSubject(subject);
            ValidateLecturer(lecturer);
            
            var allLections = FindAllStudentGrades();
            
            var allVisitedLections = allLections.Where(lection => (lection.Title == subject && lection.Le_Last_Name == lecturer))
                .Select(lection => lection).GroupBy(lection => $"{lection.St_Last_Name} {lection.St_First_Name}");

            int amountOfLections = allLections.Where(lection => (lection.Title == subject && lection.Le_Last_Name == lecturer))
                .Select(lection => lection).Count();
            
            List<string> results = new List<string>();

            foreach (var student in allVisitedLections)
            {
                results.Add($"Student: {student.Key} visited {student.Select(count => count.Date).Distinct().Count()}/{amountOfLections}");
            }

            return results;
        }
    }
}