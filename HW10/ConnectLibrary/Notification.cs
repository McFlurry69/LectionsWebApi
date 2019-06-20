using System;
using System.Linq;
using ConnectLibrary.Logger;
using ConnectLibrary.Service;
using ConnectLibrary.SQLRepository;
using ConnectLibrary.SQLRepository.Interfaces;
using HelperLibrary;
using HelperLibrary.Models;
using static  ConnectLibrary.LibrarySettings;

namespace ConnectLibrary
{
    public class Notification
    {
        readonly CommonService _studentGrades = new CommonService();
        readonly IStudentRepository _students = SetStudentRepository;
        readonly ILecturerRepository _lecturer = SetLecturerRepository;
        readonly ILectionsRepository _lection = SetLectionsRepository;
        
        public void SendMessage(StudentEventArgs args)
        {
            var lections = _studentGrades.FindStudentGrades();
            string st = _students.FindByID(args.grade.StudentId).St_Last_Name;
            string lt = _lecturer.FindByID(_lection.FindByID(args.grade.LectionId).LecturersId).Le_Last_Name;
            
            var notifyThreeLections = lections.Where(grades => grades.Key == st)
                .SelectMany(n => n, (n, k) => k.Grade).Reverse().Take(3).ToList();
            var notifyAverageGrade = lections.Where(grades => grades.Key == st)
                .SelectMany(n => n, (n, k) => k.Grade).Average();

            if (notifyThreeLections.TrueForAll(p => p.Equals(0)))
            {
                Log.MakeLog(LoggerOperations.Error, "NotImplementedException");
                throw new NotImplementedException();
                //Send mail!
            }

            if (notifyAverageGrade < 4)
            {
                Log.MakeLog(LoggerOperations.Error, "NotImplementedException");
                //Send SMS to lt!
            }
        }
        
        public void OnGradeAdded(object sourse, StudentEventArgs args)
        {
            SendMessage(args);
        }
    }
}
