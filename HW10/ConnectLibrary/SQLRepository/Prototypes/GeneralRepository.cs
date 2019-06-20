using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using static HelperLibrary.Tools;
using System.Text;
using System.Threading.Tasks;
using ConnectLibrary.Logger;
using ConnectLibrary.SQLRepository.Interfaces;
using HelperLibrary.Models;
using HelperLibrary.Models.Interfaces;
using static ConnectLibrary.LibrarySettings;

namespace ConnectLibrary.SQLRepository.Prototypes
{
    public abstract class GeneralRepository<T> : IRepository<T> where T: class, ITable
    {
        private readonly string _tableName;

        public GeneralRepository(Tables tableName)
        {
            _tableName = tableName.ToString();
        }


        public void DeleteById(int id)
        {
            if (id <= 0)
            {
                Log.MakeLog(LoggerOperations.Error, "ArgumentOutOfRangeException");
                throw new ArgumentOutOfRangeException(nameof(id));
            }
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                cn.Execute("DELETE FROM dbo." + _tableName + " WHERE Id=@ID", new { ID = id });
            }
        }

        public T FindByID(int id)
        {
            if (id <= 0) 
            {
                Log.MakeLog(LoggerOperations.Error, "ArgumentOutOfRangeException");
                throw new ArgumentOutOfRangeException(nameof(id));
            }
            
            T item = default(T);

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var p = new DynamicParameters();
                p.Add("@ID", id);
                item = cn.Query<T>($"SELECT * FROM dbo.{_tableName} WHERE Id=@ID", p).SingleOrDefault();
            }

            return item;
        }

        public IEnumerable<T> FindAll()
        {
            IEnumerable<T> items = null;

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                items = cn.Query<T>("SELECT * FROM dbo." + _tableName);
            }

            return items;
        }
    }
}
