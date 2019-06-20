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
    public class LectionsService
    {
        private readonly ILectionsRepository _lectionRepository = null;

        public LectionsService()
        {
            _lectionRepository = SetLectionsRepository;
        }

        public int AddLection(LectionModel grade)
        {
            return _lectionRepository.AddLection(grade);
        }

        public LectionModel UpdateLection(LectionModel grade)
        {
            return _lectionRepository.UpdateLection(grade);
        }

        public void DeleteLectionById(int id)
        {
            _lectionRepository.DeleteById(id);
        }

        public LectionModel FindLectionById(int id)
        {
            return _lectionRepository.FindByID(id);
        }

        public IEnumerable<LectionModel> FindAllLections()
        {
            return _lectionRepository.FindAll();
        }
    }
}
