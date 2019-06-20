using HelperLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelperLibrary;

namespace ConnectLibrary.SQLRepository.Interfaces
{
    public interface IGradeRepository : IRepository<GradeModel>
    {
         int AddGrade(GradeModel grade);
         GradeModel UpdateGrade(GradeModel grade);
         event EventHandler<StudentEventArgs> AddedGrade;
    }
}
