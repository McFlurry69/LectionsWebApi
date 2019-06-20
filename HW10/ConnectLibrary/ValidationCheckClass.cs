using System;
using System.Linq;
using System.Text.RegularExpressions;
using ConnectLibrary.Logger;
using ConnectLibrary.Service;
using HelperLibrary.CustomExceptions;
using HelperLibrary.Models;
using HelperLibrary.Models.Interfaces;
using static ConnectLibrary.LibrarySettings;

namespace ConnectLibrary
{
    public static class ValidationCheckClass
    {
        //(+X (XXX) XXX-XX-XX or X XXX XXX-XX-XX or +XXX (XX) XXX-XXXX
        const string RegexPhone = @"\+?\d\s\(?\d{3}\)?\s\d{3}-?\d{2}-?\d{2}|\+?\d{3}\(?\d{2}\)?\s\d{3}\s\-?\s\d{4}";
        //name consists no numbers
        const string RegexName = @"^[a-zA-Z]|[а-яА-Я]";
        //like that joe@aol.com | ssmith@aspalliance.com | a@b.cc
        const string RegexMail = @"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$";

        private static Regex _regex;

        private static LecturersService _lecturersService = new LecturersService();
        private static SubjectsService _subjectService = new SubjectsService();

        public static void ValidateStudent(StudentModel std)
        {
            ValidateStudentName(std);
            ValidateMail(std);
            ValidatePhone(std);
        }
        
        public static void ValidateLecturer(LecturerModel lecturer)
        {
            ValidateLecturerName(lecturer);
            ValidateMail(lecturer);
            ValidatePhone(lecturer);
        }
        
        public static void ValidateGrade(GradeModel grade)
        {
            if (grade == null) throw new ArgumentNullException(nameof(grade));
            
            if ((grade.Grade < 0 || grade.Grade > 5))
            {
                Log.MakeLog(LoggerOperations.Error, "InvalidGradeException");
                throw new InvalidGradeException(grade.Grade);
            }
        }
        
        public static void ValidateLecturer(string lecturer)
        {
            if (lecturer == null) throw new ArgumentNullException(nameof(lecturer));

            var lecturers = from lecturerModel in _lecturersService.FindAllLecturers() select lecturerModel.Le_Last_Name;


            if (!lecturers.Contains(lecturer))
            {
                Log.MakeLog(LoggerOperations.Error, "InvalidLecturerNameException");
                throw new InvalidLecturerNameException(lecturer);
            }
        }
        
        public static void ValidateSubject(string subject)
        {
            if (subject == null) throw new ArgumentNullException(nameof(subject));

            var subjects = from subjectModel in _subjectService.FindAllSubjects() select subjectModel.Title;
            
            if (!subjects.Contains(subject))
            {
                Log.MakeLog(LoggerOperations.Error, "InvalidSubjectNameException");
                throw new InvalidSubjectNameException(subject);
            }
        }

        private static void ValidateStudentName(StudentModel std)
        {
            if (std == null) throw new ArgumentNullException(nameof(std));
            
            _regex = new Regex(RegexName);

            if (!_regex.IsMatch(std.St_First_Name))
            {
                Log.MakeLog(LoggerOperations.Error, "InvalidNameException");
                throw new InvalidNameException(std.St_First_Name);
            }

            if (!_regex.IsMatch(std.St_Last_Name))
            {
                Log.MakeLog(LoggerOperations.Error, "InvalidNameException");
                throw new InvalidNameException(std.St_Last_Name);
            }
        }
        
        private static void ValidateLecturerName(LecturerModel lec)
        {
            if (lec == null) throw new ArgumentNullException(nameof(lec));
            
            _regex = new Regex(RegexName);

            if (!_regex.IsMatch(lec.Le_First_Name))
            {
                Log.MakeLog(LoggerOperations.Error, "InvalidNameException");
                throw new InvalidNameException(lec.Le_First_Name);
            }

            if (!_regex.IsMatch(lec.Le_Last_Name))
            {
                Log.MakeLog(LoggerOperations.Error, "InvalidNameException");
                throw new InvalidNameException(lec.Le_Last_Name);
            }
        }

        private static void ValidateMail(IPerson person)
        {
            if (person == null) throw new ArgumentNullException(nameof(person));
            
            _regex = new Regex(RegexMail);
            
            if (!_regex.IsMatch(person.Email))
            {
                Log.MakeLog(LoggerOperations.Error, "InvalidMailExeption");
                throw new InvalidMailExeption(person.Email);
            }
        }
        
        private static void ValidatePhone(IPerson person)
        {
            if (person == null) throw new ArgumentNullException(nameof(person));
            
            _regex = new Regex(RegexPhone);

            if (person is LecturerModel && person.Phone is null)
            {
                return;
            }

            if (!_regex.IsMatch(person.Phone))
            {
                Log.MakeLog(LoggerOperations.Error, "InvalidPhoneException");
                throw new InvalidPhoneException(person.Phone);
            }
        }
    }
}