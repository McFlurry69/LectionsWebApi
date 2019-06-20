using HelperLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectLibrary.SQLRepository.Interfaces
{
    public interface ILectionsRepository : IRepository<LectionModel>
    {
         int AddLection(LectionModel grade);
         LectionModel UpdateLection(LectionModel grade);
    }
}
