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
using ConnectLibrary.SQLRepository.Prototypes;
using HelperLibrary;
using static ConnectLibrary.DataAccess;
using static ConnectLibrary.LibrarySettings;

namespace ConnectLibrary.SQLRepository
{
    public class GradeRepository : GeneralRepository<GradeModel>, IGradeRepository
    {
        public GradeRepository() : base(Tables.Grades){}
        public event EventHandler<StudentEventArgs> AddedGrade;
        
        readonly IDbConnection _connection = Connection;

        private static DynamicParameters GradeProperties(GradeModel grade)
        {
            var p = new DynamicParameters();
            p.Add("@Id", 0, DbType.Int32, ParameterDirection.Output);
            p.Add("@StudentId", grade.StudentId);
            p.Add("@LectionId", grade.LectionId);
            p.Add("@Grade", grade.Grade);
            p.Add("@Date", grade.Date);

            return p;
        }

        public int AddGrade(GradeModel grade)
        {
            if (grade == null)
            {
                Log.MakeLog(LoggerOperations.Error, "ArgumentNullException");
                throw new ArgumentNullException(nameof(grade));
            }
            
            using (IDbConnection connection = _connection)
            {
                var p = GradeProperties(grade);
                string sql = $@"insert into dbo.{Tables.Grades} (StudentId, LectionId, Grade, Date) 
                                values (@StudentId, @LectionId, @Grade, @Date); select @Id = @@IDENTITY";
                connection.Execute(sql, p);
                OnGradeAdded(new StudentEventArgs() {grade = grade});
                return p.Get<int>("@Id");
            }
        }
        public GradeModel UpdateGrade(GradeModel grade)
        {
            if (grade == null)
            {
                Log.MakeLog(LoggerOperations.Error, "ArgumentNullException");
                throw new ArgumentNullException(nameof(grade));
            }
            
            using (IDbConnection connection = _connection)
            {
                var p = GradeProperties(grade);
                string sql = $@"update dbo.{Tables.Grades} set StudentId = @StudentId, LectionId = @LectionId, 
                                Grade = @Grade, Date = @Date 
                                where Id = {grade.Id}; select @Id = @@IDENTITY";
                connection.Execute(sql, p);

                return FindByID(grade.Id);
            }
        }

        protected virtual void OnGradeAdded(StudentEventArgs args)
        {
            AddedGrade?.Invoke(this, args);
        }
    } 
}
