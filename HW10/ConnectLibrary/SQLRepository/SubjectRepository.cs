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
using static HelperLibrary.Tools;
using static ConnectLibrary.LibrarySettings;

namespace ConnectLibrary.SQLRepository
{
    public class SubjectRepository : GeneralRepository<SubjectModel>, ISubjectRepository
    {
        public SubjectRepository() : base(Tables.Subjects) { }

        readonly IDbConnection _connection = Connection;
        
        private static DynamicParameters SubjectProperties(SubjectModel subject)
        {
            var p = new DynamicParameters();
            p.Add("@Id", 0, DbType.Int32, ParameterDirection.Output);
            p.Add("@Title", subject.Title);

            return p;
        }
        
        public int AddSubject(SubjectModel subject)
        {
            if (subject == null)
            {
                Log.MakeLog(LoggerOperations.Error, "ArgumentNullException");
                throw new ArgumentNullException(nameof(subject));
            }
            
            using (IDbConnection connection = _connection)
            {
                var p = SubjectProperties(subject);

                string sql = $@"insert into dbo.Subjects (Title)
                                values (@Title); select @Id = @@IDENTITY ";

                connection.Execute(sql, p);
                return p.Get<int>("@Id");
            }
        }

        public SubjectModel UpdateSubject(SubjectModel subject)
        {
            if (subject == null)
            {
                Log.MakeLog(LoggerOperations.Error, "ArgumentNullException");
                throw new ArgumentNullException(nameof(subject));
            }
            
            using (IDbConnection connection = _connection)
            {
                var p = SubjectProperties(subject);

                string sql = $@"update dbo.{Tables.Subjects} set 
                            Title = @Title where Id = {subject.Id}; select @Id = @@IDENTITY";
                connection.Execute(sql, p);

                return FindByID(subject.Id);
            }
        }
    }
}
