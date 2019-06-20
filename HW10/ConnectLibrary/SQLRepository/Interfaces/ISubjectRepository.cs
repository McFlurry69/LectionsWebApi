using HelperLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectLibrary.SQLRepository.Interfaces
{
    public interface ISubjectRepository : IRepository<SubjectModel>
    {
        int AddSubject(SubjectModel subject);
        SubjectModel UpdateSubject(SubjectModel subject);
    }
}
