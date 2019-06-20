using System;
using System.Collections.Generic;
using System.Linq;
using ConnectLibrary.SQLRepository.Interfaces;
using HelperLibrary.Models;
using Moq;
using NUnit.Framework;

namespace ConnectionLibrary.Tests
{
    [TestFixture]
    public class CommonServiceTests
    {
        private ICommonRepository _mockCommonRepository;
        readonly Mock<ICommonRepository> MockCommonRepository = new Mock<ICommonRepository>();
        
        [Test] 
        public void FindStudents_ValidCall()
        {
            MockCommonRepository.Setup(mr => mr.FindAllStudentGrades()).Returns(GetSampleStudents());
            _mockCommonRepository = MockCommonRepository.Object;

            IEnumerable<CommonModel> expected = _mockCommonRepository.FindAllStudentGrades();

            Assert.True(expected != null); // Test if null
            Assert.AreEqual(GetSampleStudents().Count(), expected.Count()); // Verify the correct Number
        }
        
        
        [Test] 
        public void GetLectionsSudents_ValidCall()
        {
            MockCommonRepository.Setup(mr => mr.GetLectionOfStudent(It.IsAny<string>()))
                .Returns((string s) => GetSampleStudents().Where(x => x.St_Last_Name == s));
            _mockCommonRepository = MockCommonRepository.Object;


            string checkName = "Smith";
            IEnumerable<CommonModel> expected = _mockCommonRepository.GetLectionOfStudent(checkName);

            var actual = GetSampleStudents().Where(x => x.St_Last_Name == checkName);
            
            Assert.True(expected != null); // Test if null
            Assert.AreEqual(expected.Count(), actual.Count()); // Verify the correct Number
            Assert.That(expected.Count(n => !actual.Select(n1 => n1.FullInfoForLection).Contains(n.FullInfoForLection)) == 0);
        }
        
        [Test] 
        public void GetSudentsOnLection_ValidCall()
        {
            MockCommonRepository.Setup(mr => mr.GetStudentsOfLecturer(It.IsAny<string>(), It.IsAny<string>())).Returns(
                (string s1, string s2) => GetSampleStudents().Where(x => x.Title == s1 && x.Le_Last_Name == s2));
            _mockCommonRepository = MockCommonRepository.Object;

            string checkSubject = "Math";
            string checkLeName = "Kyragin";
            IEnumerable<CommonModel> expected = _mockCommonRepository.GetStudentsOfLecturer("Math", "Kyragin");
            
            var actual = GetSampleStudents().Where(x => x.Title == checkSubject && x.Le_Last_Name == checkLeName);

            Assert.True(expected != null); // Test if null
            Assert.AreEqual(expected.Count(), actual.Count()); // Verify the correct Number
            Assert.That(expected.Count(n => !actual.Select(n1 => n1.FullInfoForStudent).Contains(n.FullInfoForStudent)) == 0);
        }


        private IEnumerable<CommonModel> GetSampleStudents()
        {
            new CommonModel(2, "Math", DateTime.Now, "John", "Smith", "Anatoly", "Kyragin");
            List<CommonModel> common = new List<CommonModel>()
            {
                new CommonModel(2, "Math", DateTime.Now, "John", "Smith", "Anatoly", "Kyragin"),
                new CommonModel(3, "French", DateTime.Now, "Mol", "Jolis", "Nikolay", "Rostov"),
                new CommonModel(3, "Physics", DateTime.Now, "John", "Smith", "Natasha", "Rostova")
            };

            return common;
        }
    }
}