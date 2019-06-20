using Dapper;
using HelperLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using static HelperLibrary.Tools;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectLibrary.Logger;
using ConnectLibrary.SQLRepository.Interfaces;
using HelperLibrary;
using HelperLibrary.Models.Interfaces;
using static ConnectLibrary.LibrarySettings;

namespace ConnectLibrary.SQLRepository.Prototypes
{
    public class PersonRepository<T> : GeneralRepository<T>, IPersonRepository<T> where T: class, IPerson
    {
        public PersonRepository(PeopleTable table) : base((Tables)table) { }

        public List<T> GetPersonByName(PeopleTable profession, string lastName, string firstname)
        {
            if (lastName == null)
            {
                Log.MakeLog(LoggerOperations.Error, "ArgumentNullException");
                throw new ArgumentNullException(nameof(lastName));
            }

            if (firstname == null)
            {
                Log.MakeLog(LoggerOperations.Error, "ArgumentNullException");
                throw new ArgumentNullException(nameof(firstname));
            }
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                string prefix = profession == PeopleTable.Students ? "St_" : "Le_";

                var p = new DynamicParameters();
                p.Add("@FirstName", firstname);
                p.Add("@LastName", lastName);

                string queryLastName = $"select * from dbo.{profession} where {prefix}Last_Name = @LastName";
                string queryNameAndLastName = $"select * from dbo.{profession} where {prefix}First_Name = @FirstName and {prefix}Last_Name = @LastName";
                
                string query = (lastName != null && firstname != null) ? queryNameAndLastName : queryLastName;

                var output = connection.Query<T>(query, p).ToList();
                return output;
            }
        }

        public static DynamicParameters PersonProperties(IPerson person, string lastName, string firstname) 
        {
            if (lastName == null)
            {
                Log.MakeLog(LoggerOperations.Error, "ArgumentNullException");
                throw new ArgumentNullException(nameof(lastName));
            }

            if (firstname == null)
            {
                Log.MakeLog(LoggerOperations.Error, "ArgumentNullException");
                throw new ArgumentNullException(nameof(firstname));
            }
            var p = new DynamicParameters();
            p.Add("@Id", 0, DbType.Int32, ParameterDirection.Output);
            p.Add("@FirstName", firstname);
            p.Add("@LastName", lastName);
            p.Add("@Email", person.Email);
            p.Add("@Phone", person.Phone);

            return p;
        }
    }
}
