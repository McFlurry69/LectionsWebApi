using System.Collections.Generic;
using HelperLibrary.Models;

namespace ConnectLibrary.SQLRepository.Interfaces
{
    public interface IPersonRepository<T> where T : class
    {
        List<T> GetPersonByName(PeopleTable profession, string lastName, string firstname);
    }
}