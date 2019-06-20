using HelperLibrary.Models;

namespace ConnectLibrary.SQLRepository.Interfaces
{
    public interface IStudentRepository : IPersonRepository<StudentModel>, IRepository<StudentModel>
    {
        int AddStudent(StudentModel student);
        StudentModel UpdateStudent(StudentModel student);
    }
}