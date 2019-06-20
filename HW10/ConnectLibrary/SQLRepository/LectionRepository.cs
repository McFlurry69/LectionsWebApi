using Dapper;
using HelperLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
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
    public class LectionRepository : GeneralRepository<LectionModel>, ILectionsRepository
    {
        public LectionRepository() : base(Tables.Lections) { }

        readonly IDbConnection _connection = Connection;
        
        private static DynamicParameters LectionProperties(LectionModel lection)
        {
            if (lection == null)
            {
                Log.MakeLog(LoggerOperations.Error, "ArgumentNullException");
                throw new ArgumentNullException(nameof(lection));
            }
            var p = new DynamicParameters();
            p.Add("@Id", 0, DbType.Int32, ParameterDirection.Output);
            p.Add("@LecturersId", lection.LecturersId);
            p.Add("@SubjectsId", lection.SubjectsId);

            return p;
        }
        
        public int AddLection(LectionModel lection)
        {
            if (lection == null)
            {
                Log.MakeLog(LoggerOperations.Error, "ArgumentNullException");
                throw new ArgumentNullException(nameof(lection));
            }
            
            using (IDbConnection connection = _connection)
            {
                var p = LectionProperties(lection);

                string sql = $@"insert into dbo.{Tables.Lections} (SubjectsId, LecturesId)
                                values (@SubjectsId, @LecturersId); select @Id = @@IDENTITY ";

                connection.Execute(sql, p);
                return p.Get<int>("@Id");
            }
        }
        
        public LectionModel UpdateLection(LectionModel lection)
        {
            if (lection == null)
            {
                Log.MakeLog(LoggerOperations.Error, "ArgumentNullException");
                throw new ArgumentNullException(nameof(lection));
            }
            
            using (IDbConnection connection = _connection)
            {
                var p = LectionProperties(lection);

                string sql = $@"update dbo.{Tables.Lections} set 
                            SubjectId = @SubjectId, LecturerId = @LecturerId where Id = {lection.Id}; select @Id = @@IDENTITY";
                connection.Execute(sql, p);

                return FindByID(lection.Id);
            }
        }
    }
}
