using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ConnectLibrary.SQLRepository.Interfaces;
using HelperLibrary.Models;
using Moq;

namespace ConnectionLibrary.Tests
{
    [TestFixture]
    public class LectionServiceTests
    {
        private ILectionsRepository _mockLectionRepository;
        readonly Mock<ILectionsRepository> MockLectionRepository = new Mock<ILectionsRepository>();
        
        [Test]
        public void FindLections_ValidCall()
        {
            Mock<ILectionsRepository> mockGroupRepository = MockLectionRepository;
            mockGroupRepository.Setup(mr => mr.FindAll()).Returns(GetSampleLection());
            _mockLectionRepository = mockGroupRepository.Object;


            IEnumerable<LectionModel> expected = _mockLectionRepository.FindAll();
 
            Assert.True(expected != null); // Test if null
            Assert.AreEqual(GetSampleLection().Count(), expected.Count()); // Verify the correct Number
        }
        
        [Test]
        public void FindLectionsById_ValidCall()
        {
            Mock<ILectionsRepository> mockGradeRepository = MockLectionRepository;
            mockGradeRepository.Setup(mr => mr.FindByID(It.IsAny<int>()))
                .Returns((int s) => GetSampleLection().Where(x => x.Id == s).FirstOrDefault());
            _mockLectionRepository = mockGradeRepository.Object;


            int checkingId = 1;
            LectionModel expected = _mockLectionRepository.FindByID(checkingId);
            var t = GetSampleLection().Where(x => x.Id == checkingId);
 
            Assert.True(expected != null); // Test if null
            Assert.That(expected.LecturersId, Is.EqualTo(GetSampleLection().FirstOrDefault(x => x.Id == checkingId).LecturersId));
        }
        
        [Test]
        public void UpdateLections_Successful()
        {
            Mock<ILectionsRepository> mockGradeRepository = MockLectionRepository;
            mockGradeRepository.Setup(mr => mr.UpdateLection(It.IsAny<LectionModel>()))
                .Returns((LectionModel s) => s);
            _mockLectionRepository = mockGradeRepository.Object;
            
            LectionModel expected = new LectionModel()
            {
                Id = 3,
                LecturersId = 2,
                SubjectsId = 3
            };

            
            var actual = _mockLectionRepository.UpdateLection(expected);
            Assert.IsNotNull(expected); // Test if null
            Assert.AreEqual(expected.Id, actual.Id); // Verify the expected Number pre-insert
        }
        
        [Test]
        public void AddLection_Successful()
        {
            Mock<ILectionsRepository> mockGradeRepository = MockLectionRepository;
            mockGradeRepository.Setup(mr => mr.AddLection(It.IsAny<LectionModel>()))
                .Returns(GetSampleLection().Max(u => u.Id)+1);
            _mockLectionRepository = mockGradeRepository.Object;
            
            LectionModel expected = new LectionModel()
            {
                Id = 3,
                LecturersId = 2,
                SubjectsId = 3
            };

            
            int count = _mockLectionRepository.AddLection(expected);
            Assert.IsNotNull(expected); // Test if null
            Assert.AreEqual(expected.Id+1, count); // Verify the expected Number pre-insert
        }
        
        private IEnumerable<LectionModel> GetSampleLection(LectionModel lection = null)
        {
            List<LectionModel> common = new List<LectionModel>()
            {
                new LectionModel() { Id = 1, LecturersId = 1, SubjectsId = 3},
                new LectionModel() { Id = 2, LecturersId = 2, SubjectsId = 2},
                new LectionModel() { Id = 3, LecturersId = 3, SubjectsId = 1},
            };
            if (lection != null)
            {
                common[lection.Id] = lection;
            }

            return common;
        }
    }
}