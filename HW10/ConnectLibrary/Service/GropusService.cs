using HelperLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectLibrary.SQLRepository.Interfaces;
using ConnectLibrary.SQLRepository;
using static ConnectLibrary.LibrarySettings;

namespace ConnectLibrary.Service
{
    public class GroupsService
    {
        private readonly IGroupRepository _groupsRepository = null;

        public GroupsService()
        {
            _groupsRepository = SetGroupRepository;
        }

        public int AddGroup(GroupModel grade)
        {
            return _groupsRepository.AddGroup(grade);
        }

        public GroupModel UpdatGroup(GroupModel grade)
        {
            return _groupsRepository.UpdateGroup(grade);
        }

        public void DeleteGroupById(int id)
        {
            _groupsRepository.DeleteById(id);
        }

        public GroupModel FinGroupById(int id)
        {
            return _groupsRepository.FindByID(id);
        }

        public IEnumerable<GroupModel> FindAllGroups()
        {
            return _groupsRepository.FindAll();
        }
    }
}
