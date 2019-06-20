using Dapper;
using HelperLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using static ConnectLibrary.DataAccess;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectLibrary.Logger;
using ConnectLibrary.SQLRepository.Interfaces;
using ConnectLibrary.SQLRepository.Prototypes;
using static ConnectLibrary.LibrarySettings;

namespace ConnectLibrary.SQLRepository
{
    public class LecturerRepository : PersonRepository<LecturerModel>, ILecturerRepository
    {
        public LecturerRepository() : base(PeopleTable.Lecturers) { }
        
        readonly IDbConnection _connection = Connection;

        public virtual int AddLecturer(LecturerModel lecturer)
        {
            if (lecturer == null)
            {
                Log.MakeLog(LoggerOperations.Error, "ArgumentNullException");
                throw new ArgumentNullException(nameof(lecturer));
            }
            
            using (IDbConnection connection = _connection)
            {
                var p = PersonProperties(lecturer, lecturer.Le_Last_Name, lecturer.Le_First_Name);

                string sql = $@"insert into dbo.{PeopleTable.Lecturers} (First_Name, Last_Name, Email, Phone) 
                                values (@FirstName, @LastName, @Email, @Phone); select @Id = @@IDENTITY";

                connection.Execute(sql, p);
                return p.Get<int>("@Id");
            }
        }

        public virtual LecturerModel UpdateLecturer(LecturerModel lecturer)
        {
            if (lecturer == null)
            {
                Log.MakeLog(LoggerOperations.Error, "ArgumentNullException");
                throw new ArgumentNullException(nameof(lecturer));
            }
            
            using (IDbConnection connection = _connection)
            {
                var p = PersonProperties(lecturer, lecturer.Le_Last_Name, lecturer.Le_First_Name);
                string sql = $@"update dbo.{PeopleTable.Lecturers} set 
                            First_Name = @FirstName, Last_Name =  @LastName, 
                            Email = @Email, Phone = @Phone where Id = {lecturer.Id}; select @Id = @@IDENTITY";
                connection.Execute(sql, p);

                return FindByID(lecturer.Id);
            }
        }
    }
}
