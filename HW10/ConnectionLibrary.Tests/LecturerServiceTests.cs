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
    public class LecturerServiceTests
    {
        private ILecturerRepository _mockLecturerRepository;
        readonly Mock<ILecturerRepository> MockLecturerRepository = new Mock<ILecturerRepository>();
        
        [Test]
        public void FindLecturers_ValidCall()
        {
            Mock<ILecturerRepository> mockStudentRepository = MockLecturerRepository;
            mockStudentRepository.Setup(mr => mr.FindAll()).Returns(GetSampleLecturers());
            _mockLecturerRepository = mockStudentRepository.Object;


            IEnumerable<LecturerModel> Grades = _mockLecturerRepository.FindAll();
 
            Assert.True(Grades != null); // Test if null
            Assert.AreEqual(GetSampleLecturers().Count(), Grades.Count()); // Verify the correct Number
        }
        
        [Test]
        public void FindLecturersById_ValidCall()
        {
            Mock<ILecturerRepository> mockGradeRepository = MockLecturerRepository;
            mockGradeRepository.Setup(mr => mr.FindByID(It.IsAny<int>()))
                .Returns((int s) => GetSampleLecturers().Where(x => x.Id == s).FirstOrDefault());
            _mockLecturerRepository = mockGradeRepository.Object;


            int checkingId = 1;
            LecturerModel expected = _mockLecturerRepository.FindByID(checkingId);
            var t = GetSampleLecturers().Where(x => x.Id == checkingId);
 
            Assert.True(expected != null); // Test if null
            Assert.That(expected.Le_Last_Name, Is.EqualTo(GetSampleLecturers().FirstOrDefault(x => x.Id == checkingId).Le_Last_Name));
        }
        
        [Test]
        public void UpdateLecturers_Successful()
        {
            Mock<ILecturerRepository> mockGradeRepository = MockLecturerRepository;
            mockGradeRepository.Setup(mr => mr.UpdateLecturer(It.IsAny<LecturerModel>()))
                .Returns((LecturerModel s) => s);
            _mockLecturerRepository = mockGradeRepository.Object;
            
            LecturerModel expected = new LecturerModel()
            {
                Id = 4,
                Le_Last_Name = "dd",
                Le_First_Name = "s",
                Phone = "8 999 999-99-99",
                Email = "23233d@dad.ru",
            };

            
            var actual = _mockLecturerRepository.UpdateLecturer(expected);
            Assert.IsNotNull(expected); // Test if null
            Assert.AreEqual(expected.Id, actual.Id); // Verify the expected Number pre-insert
        }
        
        [Test]
        public void AddLecturers_Successful()
        {
            Mock<ILecturerRepository> mockGradeRepository = MockLecturerRepository;
            mockGradeRepository.Setup(mr => mr.AddLecturer(It.IsAny<LecturerModel>()))
                .Returns(GetSampleLecturers().Max(u => u.Id)+1);
            _mockLecturerRepository = mockGradeRepository.Object;
            
            LecturerModel expected = new LecturerModel()
            {
                Id = 3,
                Le_Last_Name = "dd",
                Le_First_Name = "s",
                Phone = "8 999 999-99-99",
                Email = "23233d@dad.ru",
            };

            
            int count = _mockLecturerRepository.AddLecturer(expected);
            Assert.IsNotNull(expected); // Test if null
            Assert.AreEqual(expected.Id+1, count); // Verify the expected Number pre-insert
        }
        
        [Test]
        public void AddPhone_Exception()
        {
            LecturersService service = new LecturersService();
            
            LecturerModel expected = new LecturerModel()
            {
                Id = 4,
                Le_Last_Name = "ddds",
                Le_First_Name = "ssad",
                Phone = "42s4d323",
                Email = "23233ds@mail.ru",
            };
            
            Assert.Throws<InvalidPhoneException>(() => service.AddLecturer(expected));
        }
        
        [Test]
        public void AddName_Exception()
        {
            LecturersService service = new LecturersService();
            
            LecturerModel expected = new LecturerModel()
            {
                Id = 4,
                Le_Last_Name = "242df34",
                Le_First_Name = "s41s",
                Phone = "8 999 222-11-11",
                Email = "23233dd@mail.ru"
            };
            
            Assert.Throws<InvalidNameException>(() => service.AddLecturer(expected));
        }
        
        [Test]
        public void AddMail_Exception()
        {
            LecturersService service = new LecturersService();
            
            LecturerModel expected = new LecturerModel()
            {
                Id = 4,
                Le_Last_Name = "ddad",
                Le_First_Name = "sasd",
                Phone = "8 999 222-11-11",
                Email = "23233.ru"
            };
            
            Assert.Throws<InvalidMailExeption>(() => service.AddLecturer(expected));
        }
        
        private IEnumerable<LecturerModel> GetSampleLecturers(LecturerModel student = null)
        {
            List<LecturerModel> common = new List<LecturerModel>()
            {
                new LecturerModel() { Id = 1, Email = "ddd@dd.com", Phone = "8 911 929-22-22", Le_First_Name = "Bob", Le_Last_Name = "Marley"},
                new LecturerModel() { Id = 2, Email = "add@dd.com", Phone = "8 811 919-22-22", Le_First_Name = "Bob1", Le_Last_Name = "Marley1"},
                new LecturerModel() { Id = 3, Email = "zdd@dd.com", Phone = "8 911 939-22-22", Le_First_Name = "Bob2", Le_Last_Name = "Marley2"},
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