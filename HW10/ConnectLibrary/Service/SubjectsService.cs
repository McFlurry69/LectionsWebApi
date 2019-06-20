using HelperLibrary.Models;
using ConnectLibrary.SQLRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectLibrary.SQLRepository.Interfaces;
using static ConnectLibrary.LibrarySettings;

namespace ConnectLibrary.Service
{
    public class SubjectsService
    {
        private readonly ISubjectRepository _subjectRepository = null;

        public SubjectsService()
        {
            _subjectRepository = SetSubjectRepository;
        }

        public int AddSubject (SubjectModel subject)
        {
            return _subjectRepository.AddSubject(subject);
        }

        public SubjectModel UpdateSubject (SubjectModel subject)
        {
            return _subjectRepository.UpdateSubject(subject);
        }

        public void DeleteSubjectById (int id)
        {
            _subjectRepository.DeleteById(id);
        }

        public SubjectModel FindSubjectById(int id)
        {
            return _subjectRepository.FindByID(id);
        }

        public IEnumerable<SubjectModel> FindAllSubjects()
        {
            return _subjectRepository.FindAll();
        }

    }
}
