using HelperLibrary.Models;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ConnectLibrary.SQLRepository.Interfaces;
using HelperLibrary.CustomExceptions;
using ConnectLibrary.SQLRepository;
using static ConnectLibrary.LibrarySettings;
using static ConnectLibrary.ValidationCheckClass;

namespace ConnectLibrary.Service
{
    public class StudentsService
    {
        private readonly IStudentRepository _studentRepository = null;

        public StudentsService()
        {
            _studentRepository = SetStudentRepository;
        }

        public List<StudentModel> GetStudentsByName(string lastName, string firstname)
        {
            return _studentRepository.GetPersonByName(PeopleTable.Students, lastName, firstname);
        }

        public int AddStudent(StudentModel student)
        {
            ValidateStudent(student);
            return _studentRepository.AddStudent(student);
        }

        public void ExcludeStudent(int studentId)
        {
            _studentRepository.DeleteById(studentId);
        }

        public StudentModel FindStudentById(int studentId)
        {
            return _studentRepository.FindByID(studentId);
        }

        public IEnumerable<StudentModel> FindAllStudents()
        {
            return _studentRepository.FindAll();
        }

        public StudentModel UpdateStudentInfo(StudentModel student)
        {
            ValidateStudent(student);
            return _studentRepository.UpdateStudent(student);
        }
    }
}
