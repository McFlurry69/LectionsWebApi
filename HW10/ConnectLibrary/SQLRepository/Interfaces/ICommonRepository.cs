using System.Collections.Generic;
using HelperLibrary.Models;

namespace ConnectLibrary.SQLRepository.Interfaces
{
    public interface ICommonRepository
    {
        IEnumerable<CommonModel> FindAllStudentGrades();

        IEnumerable<CommonModel> GetStudentsOfLecturer(string subject, string lecturer);

        IEnumerable<CommonModel> GetLectionOfStudent(string name);

        IEnumerable<string> GetAmountOfVisitedLections(string name);
        
        IEnumerable<string> GetAmountOfVisitedLections(string subject, string lecturer);
    }
}