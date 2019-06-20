using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ConnectLibrary.SQLRepository.Interfaces;
using HelperLibrary.Models;
using Moq;

namespace ConnectionLibrary.Tests
{
    [TestFixture]
    public class SubjectServiceTests
    {
        private ISubjectRepository _mockSubjectRepository;
        readonly Mock<ISubjectRepository> MockSubjectRepository = new Mock<ISubjectRepository>();
        
        [Test]
        public void FindSubjects_ValidCall()
        {
            Mock<ISubjectRepository> mockGroupRepository = MockSubjectRepository;
            mockGroupRepository.Setup(mr => mr.FindAll()).Returns(GetSampleSubjects());
            _mockSubjectRepository = mockGroupRepository.Object;


            IEnumerable<SubjectModel> subject = _mockSubjectRepository.FindAll();
 
            Assert.True(subject != null); // Test if null
            Assert.AreEqual(GetSampleSubjects().Count(), subject.Count()); // Verify the correct Number
        }
        
        [Test]
        public void FindSubjectsById_ValidCall()
        {
            Mock<ISubjectRepository> mockGradeRepository = MockSubjectRepository;
            mockGradeRepository.Setup(mr => mr.FindByID(It.IsAny<int>()))
                .Returns((int s) => GetSampleSubjects().Where(x => x.Id == s).FirstOrDefault());
            _mockSubjectRepository = mockGradeRepository.Object;


            int checkingId = 1;
            SubjectModel expected = _mockSubjectRepository.FindByID(checkingId);
            var t = GetSampleSubjects().Where(x => x.Id == checkingId);
 
            Assert.True(expected != null); // Test if null
            Assert.That(expected.Title, Is.EqualTo(GetSampleSubjects().FirstOrDefault(x => x.Id == checkingId).Title));
        }
        
        [Test]
        public void UpdateSubject_Successful()
        {
            Mock<ISubjectRepository> mockGradeRepository = MockSubjectRepository;
            mockGradeRepository.Setup(mr => mr.UpdateSubject(It.IsAny<SubjectModel>()))
                .Returns((SubjectModel s) => s);
            _mockSubjectRepository = mockGradeRepository.Object;
            
            SubjectModel expected = new SubjectModel()
            {
                Id = 3,
                Title = "Physics"
            };

            
            var actual = _mockSubjectRepository.UpdateSubject(expected);
            Assert.IsNotNull(expected); // Test if null
            Assert.AreEqual(expected.Id, actual.Id); // Verify the expected Number pre-insert
        }
        
        [Test]
        public void AddGrade_Successfulessful()
        {
            Mock<ISubjectRepository> mockGradeRepository = MockSubjectRepository;
            mockGradeRepository.Setup(mr => mr.AddSubject(It.IsAny<SubjectModel>()))
                .Returns(GetSampleSubjects().Max(u => u.Id)+1);
            _mockSubjectRepository = mockGradeRepository.Object;
            
            SubjectModel expected = new SubjectModel()
            {
                Id = 3,
                Title = "Drawing"
            };

            
            int count = _mockSubjectRepository.AddSubject(expected);
            Assert.IsNotNull(expected); // Test if null
            Assert.AreEqual(expected.Id+1, count); // Verify the expected Number pre-insert
        }
        
        private IEnumerable<SubjectModel> GetSampleSubjects(SubjectModel subject = null)
        {
            List<SubjectModel> common = new List<SubjectModel>()
            {
                new SubjectModel() { Id = 1, Title = "Math"},
                new SubjectModel() { Id = 2, Title = "Physics"},
                new SubjectModel() { Id = 3, Title = "Algebra"},
            };
            if (subject != null)
            {
                common[subject.Id] = subject;
            }

            return common;
        }
    }
}