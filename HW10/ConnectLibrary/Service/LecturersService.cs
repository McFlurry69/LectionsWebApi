using HelperLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectLibrary.SQLRepository.Interfaces;
using HelperLibrary.Models.Interfaces;
using ConnectLibrary.SQLRepository;
using static ConnectLibrary.LibrarySettings;
using static ConnectLibrary.ValidationCheckClass;

namespace ConnectLibrary.Service
{
    public class LecturersService
    {
        private readonly ILecturerRepository _lecturerRepository = null;

        public LecturersService()
        {
            _lecturerRepository = SetLecturerRepository;
        }

        public List<LecturerModel> GetLecturersByName(string lastName, string firstname)
        {
            return _lecturerRepository.GetPersonByName(PeopleTable.Lecturers, lastName, firstname);
        }

        public int AddLecturer(LecturerModel lecturer)
        {
            ValidateLecturer(lecturer);
            return _lecturerRepository.AddLecturer(lecturer);
        }

        public void FireLecturer(int lecturerId)
        {
            _lecturerRepository.DeleteById(lecturerId);
        }

        public IPerson FindLecturerById(int lecturerId)
        {
            return _lecturerRepository.FindByID(lecturerId);
        }

        public IEnumerable<LecturerModel> FindAllLecturers()
        {
            return _lecturerRepository.FindAll();
        }

        public LecturerModel UpdateLecturerInfo(LecturerModel lecturer)
        {
            ValidateLecturer(lecturer);
            return _lecturerRepository.UpdateLecturer(lecturer);
        }
    }
}
