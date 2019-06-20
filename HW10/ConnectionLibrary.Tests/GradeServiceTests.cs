using System;
using System.Collections.Generic;
using System.Linq;
using ConnectLibrary;
using ConnectLibrary.Service;
using ConnectLibrary.SQLRepository;
using NUnit.Framework;
using ConnectLibrary.SQLRepository.Interfaces;
using HelperLibrary;
using HelperLibrary.CustomExceptions;
using HelperLibrary.Models;
using Moq;

namespace ConnectionLibrary.Tests
{
    [TestFixture]
    public class GradeServiceTests
    {
        private IGradeRepository _mockGradeRepository;
        readonly Mock<IGradeRepository> MockGradeRepository = new Mock<IGradeRepository>();
        
        [Test]
        public void FindGrades_ValidCall()
        {
            Mock<IGradeRepository> mockGradeRepository = MockGradeRepository;
            mockGradeRepository.Setup(mr => mr.FindAll()).Returns(GetSampleGrade());
            _mockGradeRepository = mockGradeRepository.Object;


            IEnumerable<GradeModel> Grades = _mockGradeRepository.FindAll();
 
            Assert.True(Grades != null); // Test if null
            Assert.AreEqual(GetSampleGrade().Count(), Grades.Count()); // Verify the correct Number
        }
        
        [Test]
        public void FindGradesById_ValidCall()
        {
            Mock<IGradeRepository> mockGradeRepository = MockGradeRepository;
            mockGradeRepository.Setup(mr => mr.FindByID(It.IsAny<int>()))
                .Returns((int s) => GetSampleGrade().Where(x => x.Id == s).FirstOrDefault());
            _mockGradeRepository = mockGradeRepository.Object;


            int checkingId = 1;
            GradeModel expected = _mockGradeRepository.FindByID(checkingId);
            var t = GetSampleGrade().Where(x => x.Id == checkingId);
 
            Assert.True(expected != null); // Test if null
            Assert.That(expected.StudentId, Is.EqualTo(GetSampleGrade().FirstOrDefault(x => x.Id == checkingId).StudentId));
        }
        
        [Test]
        public void UpdateGrade_Successful()
        {
            Mock<IGradeRepository> mockGradeRepository = MockGradeRepository;
            mockGradeRepository.Setup(mr => mr.UpdateGrade(It.IsAny<GradeModel>()))
                .Returns((GradeModel s) => s);
            _mockGradeRepository = mockGradeRepository.Object;
            
            GradeModel expected = new GradeModel()
            {
                Id = 3,
                StudentId = 1,
                LectionId = 2,
                Grade = 3,
                Date = DateTime.Now
            };

            
            var actual = _mockGradeRepository.UpdateGrade(expected);
            Assert.IsNotNull(expected); // Test if null
            Assert.AreEqual(expected.Id, actual.Id); // Verify the expected Number pre-insert
        }
        
        [Test]
        public void AddGrade_Successful()
        {
            Mock<IGradeRepository> mockGradeRepository = MockGradeRepository;
            mockGradeRepository.Setup(mr => mr.AddGrade(It.IsAny<GradeModel>()))
                .Returns(GetSampleGrade().Max(u => u.Id)+1);
            _mockGradeRepository = mockGradeRepository.Object;
            
            GradeModel expected = new GradeModel()
            {
                Id = 2,
                StudentId = 1,
                LectionId = 2,
                Grade = 3,
                Date = DateTime.Now
            };

            
            int count = _mockGradeRepository.AddGrade(expected);
            Assert.IsNotNull(expected); // Test if null
            Assert.AreEqual(expected.Id+1, count); // Verify the expected Number pre-insert
        }
                
        [Test]
        public void AddThreeZeroMarks_Successful()
        {
            //assert
            string st = "Smith";
            
            Assert.Throws<NotImplementedException>(() => SendMessage(st));
        }

        private void SendMessage(string st)
        {
            //Same as Notification.SendMessage
            var lections = from u in ForNotificationCheck() group u by u.St_Last_Name;

            var notifyThreeLections = lections.Where(grades => grades.Key == st)
                .SelectMany(n => n, (n, k) => k.Grade).Reverse().Take(3).ToList();
            var notifyAverageGrade = lections.Where(grades => grades.Key == st)
                .SelectMany(n => n, (n, k) => k.Grade).Average();

            if (notifyThreeLections.TrueForAll(p => p.Equals(0)))
            {
                throw new NotImplementedException();
                //Send mail!
            }

            if (notifyAverageGrade < 4)
            {
                //Send SMS to lt!
            }
        }


        private IEnumerable<CommonModel> ForNotificationCheck()
        {
            IEnumerable<CommonModel> lections = new List<CommonModel>()
            {
                new CommonModel()
                {
                    Date = new DateTime(2008, 10, 12), Grade = 0, St_First_Name = "John", St_Last_Name = "Smith",
                    Le_Last_Name = "Depp", Le_First_Name = "Johny", Title = "Math"
                },
                new CommonModel()
                {
                    Date = new DateTime(2009, 10, 12), Grade = 0, St_First_Name = "John", St_Last_Name = "Smith",
                    Le_Last_Name = "Depp", Le_First_Name = "Johny", Title = "Math"
                },
                new CommonModel()
                {
                    Date = new DateTime(2010, 10, 12), Grade = 0, St_First_Name = "John", St_Last_Name = "Smith",
                    Le_Last_Name = "Depp", Le_First_Name = "Johny", Title = "Math"
                }
            };
            return lections;
        }


        [Test]
        public void AddGrade_Exception()
        {
            GradesService service = new GradesService();
            
            GradeModel expected = new GradeModel()
            {
                Id = 6,
                StudentId = 1,
                LectionId = 2,
                Grade = 8,
                Date = DateTime.Now
            };
            
            Assert.Throws<InvalidGradeException>(() => service.AddGrade(expected));
        }
        
        private IEnumerable<GradeModel> GetSampleGrade(GradeModel grade = null)
        {
            List<GradeModel> common = new List<GradeModel>()
            {
                new GradeModel() { Id = 0, Grade = 3, Date = DateTime.Now, LectionId = 1, StudentId = 2},
                new GradeModel() { Id = 1, Grade = 1, Date = DateTime.Now, LectionId = 2, StudentId = 1},
                new GradeModel() { Id = 2, Grade = 2, Date = DateTime.Now, LectionId = 3, StudentId = 3}
            };
            if (grade != null)
            {
                common.Add(grade);
            }
            //var result = from commonModel in common group commonModel by commonModel.St_Last_Name;

            return common;
        }
    }
}