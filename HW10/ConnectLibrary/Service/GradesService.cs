using HelperLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectLibrary.SQLRepository.Interfaces;
using ConnectLibrary.SQLRepository;
using static ConnectLibrary.LibrarySettings;
using static ConnectLibrary.ValidationCheckClass;

namespace ConnectLibrary.Service
{
    public class GradesService
    {
        public IGradeRepository _gradeRepository;
        Notification _note = new Notification();

        public GradesService()
        {
            _gradeRepository = SetGradeRepository;
        }

        public int AddGrade(GradeModel grade)
        {
            ValidateGrade(grade);
            _gradeRepository.AddedGrade += _note.OnGradeAdded;
            
            return _gradeRepository.AddGrade(grade);
        }

        public GradeModel UpdateGrade(GradeModel grade)
        {
            ValidateGrade(grade);
            return _gradeRepository.UpdateGrade(grade);
        }

        public void DeleteGradeById(int id)
        {
            _gradeRepository.DeleteById(id);
        }

        public GradeModel FindGradeById(int id)
        {
            return _gradeRepository.FindByID(id);
        }

        public IEnumerable<GradeModel> FindAllGrades()
        {
            return _gradeRepository.FindAll();
        }
    }
}
