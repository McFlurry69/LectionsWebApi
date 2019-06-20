using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ConnectLibrary.SQLRepository.Interfaces;
using HelperLibrary.Models;
using Moq;

namespace ConnectionLibrary.Tests
{
    [TestFixture]
    public class GroupServiceTests
    {
        private IGroupRepository _mockGroupRepository;
        readonly Mock<IGroupRepository> MockGroupRepository = new Mock<IGroupRepository>();
        
        [Test]
        public void FindGroups_ValidCall()
        {
            Mock<IGroupRepository> mockGroupRepository = MockGroupRepository;
            mockGroupRepository.Setup(mr => mr.FindAll()).Returns(GetSampleGroup());
            _mockGroupRepository = mockGroupRepository.Object;


            IEnumerable<GroupModel> expected = _mockGroupRepository.FindAll();
 
            Assert.True(expected != null); // Test if null
            Assert.AreEqual(GetSampleGroup().Count(), expected.Count()); // Verify the correct Number
        }
        
        [Test]
        public void FindGradesById_ValidCall()
        {
            Mock<IGroupRepository> mockGradeRepository = MockGroupRepository;
            mockGradeRepository.Setup(mr => mr.FindByID(It.IsAny<int>()))
                .Returns((int s) => GetSampleGroup().Where(x => x.Id == s).FirstOrDefault());
            _mockGroupRepository = mockGradeRepository.Object;


            int checkingId = 1;
            GroupModel expected = _mockGroupRepository.FindByID(checkingId);
            var t = GetSampleGroup().Where(x => x.Id == checkingId);
 
            Assert.True(expected != null); // Test if null
            Assert.That(expected.GroupNumber, Is.EqualTo(GetSampleGroup().FirstOrDefault(x => x.Id == checkingId).GroupNumber));
        }
        
        [Test]
        public void UpdateGrade_Successful()
        {
            Mock<IGroupRepository> mockGradeRepository = MockGroupRepository;
            mockGradeRepository.Setup(mr => mr.UpdateGroup(It.IsAny<GroupModel>()))
                .Returns((GroupModel s) => s);
            _mockGroupRepository = mockGradeRepository.Object;
            
            GroupModel expected = new GroupModel()
            {
                Id = 3,
                GroupNumber = "2"
            };

            
            var actual = _mockGroupRepository.UpdateGroup(expected);
            Assert.IsNotNull(expected); // Test if null
            Assert.AreEqual(expected.Id, actual.Id); // Verify the expected Number pre-insert
        }
        
        [Test]
        public void AddGrade_Successful()
        {
            Mock<IGroupRepository> mockGradeRepository = MockGroupRepository;
            mockGradeRepository.Setup(mr => mr.AddGroup(It.IsAny<GroupModel>()))
                .Returns(GetSampleGroup().Max(u => u.Id)+1);
            _mockGroupRepository = mockGradeRepository.Object;
            
            GroupModel expected = new GroupModel()
            {
                Id = 3,
                GroupNumber = "3"
            };

            
            int count = _mockGroupRepository.AddGroup(expected);
            Assert.IsNotNull(expected); // Test if null
            Assert.AreEqual(expected.Id+1, count); // Verify the expected Number pre-insert
        }
        
        private IEnumerable<GroupModel> GetSampleGroup(GroupModel group = null)
        {
            List<GroupModel> common = new List<GroupModel>()
            {
                new GroupModel() { Id = 1, GroupNumber = "1"},
                new GroupModel() { Id = 2, GroupNumber = "2"},
                new GroupModel() { Id = 3, GroupNumber = "3"},
            };
            if (group != null)
            {
                common[group.Id] = group;
            }
            //var result = from commonModel in common group commonModel by commonModel.St_Last_Name;

            return common;
        }
    }
}