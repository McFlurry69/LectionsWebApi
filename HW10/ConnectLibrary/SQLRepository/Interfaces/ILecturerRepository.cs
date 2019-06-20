using HelperLibrary.Models;

namespace ConnectLibrary.SQLRepository.Interfaces
{
    public interface ILecturerRepository : IRepository<LecturerModel>, IPersonRepository<LecturerModel>
    {
        int AddLecturer(LecturerModel lecturer);
        LecturerModel UpdateLecturer(LecturerModel lecturer);
    }
}