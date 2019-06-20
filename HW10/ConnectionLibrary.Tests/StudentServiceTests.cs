using System.Collections.Generic;
using System.Linq;
using ConnectLibrary.Service;
using NUnit.Framework;
using ConnectLibrary.SQLRepository.Interfaces;
using HelperLibrary.CustomExceptions;
using HelperLibrary.Models;
using Moq;

namespace ConnectionLibrary.Tests
{
    [TestFixture]
    public class StudentServiceTests
    {
        private IStudentRepository _mockStudentRepository;
        readonly Mock<IStudentRepository> MockStudentRepository = new Mock<IStudentRepository>();
        
        [Test]
        public void FindStudents_ValidCall()
        {
            Mock<IStudentRepository> mockStudentRepository = MockStudentRepository;
            mockStudentRepository.Setup(mr => mr.FindAll()).Returns(GetSampleStudents());
            _mockStudentRepository = mockStudentRepository.Object;


            IEnumerable<StudentModel> Grades = _mockStudentRepository.FindAll();
 
            Assert.True(Grades != null); // Test if null
            Assert.AreEqual(GetSampleStudents().Count(), Grades.Count()); // Verify the correct Number
        }
        
        [Test]
        public void FindStudentsById_ValidCall()
        {
            Mock<IStudentRepository> mockGradeRepository = MockStudentRepository;
            mockGradeRepository.Setup(mr => mr.FindByID(It.IsAny<int>()))
                .Returns((int s) => GetSampleStudents().Where(x => x.Id == s).FirstOrDefault());
            _mockStudentRepository = mockGradeRepository.Object;


            int checkingId = 1;
            StudentModel expected = _mockStudentRepository.FindByID(checkingId);
            var t = GetSampleStudents().Where(x => x.Id == checkingId);
 
            Assert.True(expected != null); // Test if null
            Assert.That(expected.St_Last_Name, Is.EqualTo(GetSampleStudents().FirstOrDefault(x => x.Id == checkingId).St_Last_Name));
        }
        
        [Test]
        public void UpdateStudent_Successful()
        {
            Mock<IStudentRepository> mockGradeRepository = MockStudentRepository;
            mockGradeRepository.Setup(mr => mr.UpdateStudent(It.IsAny<StudentModel>()))
                .Returns((StudentModel s) => s);
            _mockStudentRepository = mockGradeRepository.Object;
            
            StudentModel expected = new StudentModel()
            {
                Id = 4,
                St_Last_Name = "dd",
                St_First_Name = "s",
                Phone = "8-999-999-99-99",
                Email = "23233d@dad.ru",
                GroupNumberId = 2
            };

            
            var actual = _mockStudentRepository.UpdateStudent(expected);
            Assert.IsNotNull(expected); // Test if null
            Assert.AreEqual(expected.Id, actual.Id); // Verify the expected Number pre-insert
        }
        
        [Test]
        public void AddStudent_Successful()
        {
            Mock<IStudentRepository> mockGradeRepository = MockStudentRepository;
            mockGradeRepository.Setup(mr => mr.AddStudent(It.IsAny<StudentModel>()))
                .Returns(GetSampleStudents().Max(u => u.Id)+1);
            _mockStudentRepository = mockGradeRepository.Object;
            
            StudentModel expected = new StudentModel()
            {
                Id = 3,
                St_Last_Name = "dd",
                St_First_Name = "s",
                Phone = "8-999-999-99-99",
                Email = "23233d@dad.ru",
                GroupNumberId = 2
            };

            
            int count = _mockStudentRepository.AddStudent(expected);
            Assert.IsNotNull(expected); // Test if null
            Assert.AreEqual(expected.Id+1, count); // Verify the expected Number pre-insert
        }
        
        [Test]
        public void AddPhone_Exception()
        {
            StudentsService service = new StudentsService();
            
            StudentModel expected = new StudentModel()
            {
                Id = 4,
                St_Last_Name = "ddds",
                St_First_Name = "ssad",
                Phone = "42s4d323",
                Email = "23233ds@mail.ru",
                GroupNumberId = 2
            };
            
            Assert.Throws<InvalidPhoneException>(() => service.AddStudent(expected));
        }
        
        [Test]
        public void AddName_Exception()
        {
            StudentsService service = new StudentsService();
            
            StudentModel expected = new StudentModel()
            {
                Id = 4,
                St_Last_Name = "131231",
                St_First_Name = "s41s",
                Phone = "8 999 222-11-11",
                Email = "23233dd@mail.ru",
                GroupNumberId = 2
            };
            
            Assert.Throws<InvalidNameException>(() => service.AddStudent(expected));
        }
        
        [Test]
        public void AddMail_Exception()
        {
            StudentsService service = new StudentsService();
            
            StudentModel expected = new StudentModel()
            {
                Id = 4,
                St_Last_Name = "ddad",
                St_First_Name = "sasd",
                Phone = "8 999 222-11-11",
                Email = "23233.ru",
                GroupNumberId = 2
            };
            
            Assert.Throws<InvalidMailExeption>(() => service.AddStudent(expected));
        }
        
        private IEnumerable<StudentModel> GetSampleStudents(StudentModel student = null)
        {
            List<StudentModel> common = new List<StudentModel>()
            {
                new StudentModel() { Id = 1, Email = "ddd@dd.com", Phone = "8 911 929-22-22", GroupNumberId = 1, St_First_Name = "Bob", St_Last_Name = "Marley"},
                new StudentModel() { Id = 2, Email = "add@dd.com", Phone = "8 911 919-22-22", GroupNumberId = 2, St_First_Name = "Bob1", St_Last_Name = "Marley1"},
                new StudentModel() { Id = 3, Email = "zdd@dd.com", Phone = "8 911 939-22-22", GroupNumberId = 3, St_First_Name = "Bob2", St_Last_Name = "Marley2"},
            };
            if (student != null)
            {
                common[student.Id] = student;
            }
            //var result = from commonModel in common group commonModel by commonModel.St_Last_Name;

            return common;
        }
    }
}