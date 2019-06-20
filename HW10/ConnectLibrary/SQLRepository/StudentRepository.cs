using Dapper;
using HelperLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectLibrary.Logger;
using ConnectLibrary.SQLRepository.Interfaces;
using ConnectLibrary.SQLRepository.Prototypes;
using static ConnectLibrary.DataAccess;
using static ConnectLibrary.LibrarySettings;

namespace ConnectLibrary.SQLRepository
{
    public class StudentRepository : PersonRepository<StudentModel>, IStudentRepository
    {
        public StudentRepository() : base(PeopleTable.Students) { }

        readonly IDbConnection _connection = Connection;
        
        public virtual int AddStudent(StudentModel student)
        {
            if (student == null)
            {
                Log.MakeLog(LoggerOperations.Error, "ArgumentNullException");
                throw new ArgumentNullException(nameof(student));
            }
            
            using (IDbConnection connection = _connection)
            {
                var p = PersonProperties(student, student.St_Last_Name, student.St_First_Name);
                p.Add("@GroupId", student.GroupNumberId);

                string sql = $@"insert into dbo.{PeopleTable.Students} (St_First_Name, St_Last_Name, Email, Phone, GroupNumberId) 
                                values (@FirstName, @LastName, @Email, @Phone, @GroupId); select @Id = @@IDENTITY";

                connection.Execute(sql, p);
                return p.Get<int>("@Id");
            }
        }

        public virtual StudentModel UpdateStudent(StudentModel student)
        {
            if (student == null)
            {
                Log.MakeLog(LoggerOperations.Error, "ArgumentNullException");
                throw new ArgumentNullException(nameof(student));
            }
            
            using (IDbConnection connection = _connection)
            {
                var p = PersonProperties(student, student.St_Last_Name, student.St_First_Name);

                string sql = $@"update dbo.{PeopleTable.Students} set 
                            First_Name = @FirstName, Last_Name =  @LastName, 
                            Email = @Email, Phone = @Phone, GroupNumberId = @GroupId where Id = {student.Id}; select @Id = @@IDENTITY";
                connection.Execute(sql, p);

                return FindByID(student.Id);
            }
        }
    }
}
