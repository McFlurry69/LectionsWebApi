using HelperLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using ConnectLibrary.Logger;
using ConnectLibrary.SQLRepository.Interfaces;
using ConnectLibrary.SQLRepository;
using MediaTypeHeaderValue = System.Net.Http.Headers.MediaTypeHeaderValue;
using static ConnectLibrary.LibrarySettings;

namespace ConnectLibrary.Service
{
    public class CommonService
    {
        private readonly ICommonRepository _commonRepository;

        public CommonService()
        {
            _commonRepository = SetCommonRepository;
        }
        
        public IEnumerable<IGrouping<string, CommonModel>> FindStudentGrades()
        {
            var allGrades = _commonRepository.FindAllStudentGrades();
            var groupedGrades = from students in allGrades group students by students.St_Last_Name;
            return groupedGrades;
        }
        
        public IEnumerable<IGrouping<string, CommonModel>> GetStudentsByLecture(string subject, string lecturer)
        {
            if (subject == null)
            {
                Log.MakeLog(LoggerOperations.Error, "Argument null exception");
                throw new ArgumentNullException(nameof(subject));
            }

            if (lecturer == null)
            {
                Log.MakeLog(LoggerOperations.Error, "Argument null exception");
                throw new ArgumentNullException(nameof(lecturer));
            }
            
            return _commonRepository.GetStudentsOfLecturer(subject, lecturer).GroupBy(students => students.St_Last_Name + " " + students.St_First_Name);
        }
        
        public IEnumerable<IGrouping<string, CommonModel>> GetStudentsByLastName(string name)
        {
            if (name == null)
            {
                Log.MakeLog(LoggerOperations.Error, "Argument null exception");
                throw new ArgumentNullException(nameof(name));
            }
            
            return _commonRepository.GetLectionOfStudent(name)
                .GroupBy(students => students.Le_Last_Name + " " + students.Title);
        }

        public IEnumerable<string> GetAmountOfVisitedLections(string name, bool createRaport = false, FileFormat format = FileFormat.txt)
        {
            if (name == null)
            {
                Log.MakeLog(LoggerOperations.Error, "Argument null exception");
                throw new ArgumentNullException(nameof(name));
            }
            var amountOfLectionsVisited = _commonRepository.GetAmountOfVisitedLections(name);
            
            if (createRaport)
            {
                CreateFileRaport(amountOfLectionsVisited, name, format);
                Log.MakeLog(LoggerOperations.Info, "File has been created");
            }

            return amountOfLectionsVisited;
        }
        
        public IEnumerable<string> GetAmountOfVisitedLections(string subject, string lecturer, bool createRaport = false, FileFormat format = FileFormat.txt)
        {
            if (subject == null) throw new ArgumentNullException(nameof(subject));
            if (lecturer == null) throw new ArgumentNullException(nameof(lecturer));
            var amountOfLectionsVisited = _commonRepository.GetAmountOfVisitedLections(subject, lecturer);
            
            if (createRaport)
            {
                CreateFileRaport(amountOfLectionsVisited, $"{subject}-{lecturer}", format);
                Log.MakeLog(LoggerOperations.Info, "File has been created");
            }

            return amountOfLectionsVisited;
        }

        public void CreateFileRaport(IEnumerable<string> raport, string filename, FileFormat format, string path = null)
        {
            string _path;

            if (raport == null)
            {
                Log.MakeLog(LoggerOperations.Error, "Argument null exception");
                throw new ArgumentNullException(nameof(raport));
            }

            if (filename == null)
            {
                Log.MakeLog(LoggerOperations.Error, "Argument null exception");
                throw new ArgumentNullException(nameof(filename));
            }

            if (path == null)
            {
                var systemPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                _path = Path.Combine(systemPath , "Raports");
            }
            else
            {
                _path = path;
            }
            

            if (!Directory.Exists(_path)) 
            { 
                Directory.CreateDirectory(_path); 
            } 
            //Creates file on Desktop
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(_path, $"{filename}.{format}")))
            {
                foreach (var listOfResult in raport)
                {
                    outputFile.WriteLine(listOfResult);
                }
            }
        }
    }
}
