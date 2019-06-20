using HelperLibrary.Models;

namespace ConnectLibrary.SQLRepository.Interfaces
{
    public interface IGroupRepository : IRepository<GroupModel>
    {
        int AddGroup(GroupModel group);
        GroupModel UpdateGroup(GroupModel group);
    }
}