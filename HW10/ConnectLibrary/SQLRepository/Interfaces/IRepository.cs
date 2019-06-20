using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using HelperLibrary.Models;
using HelperLibrary.Models.Interfaces;

namespace ConnectLibrary.SQLRepository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void DeleteById(int id);
        T FindByID(int id);
        IEnumerable<T> FindAll();
    }
}
