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
using static ConnectLibrary.LibrarySettings;

namespace ConnectLibrary.SQLRepository
{
    public class GroupRepository : GeneralRepository<GroupModel>, IGroupRepository
    {
        public GroupRepository() : base(Tables.Groups) { }
        
        readonly IDbConnection _connection = Connection;

        private static DynamicParameters GroupProperties(GroupModel group)
        {
            if (group == null)
            {
                Log.MakeLog(LoggerOperations.Error, "ArgumentNullException");
                throw new ArgumentNullException(nameof(group));
            }
            var p = new DynamicParameters();
            p.Add("@Id", 0, DbType.Int32, ParameterDirection.Output);
            p.Add("@GroupNumber", group.GroupNumber);

            return p;
        }
        
        public int AddGroup(GroupModel group)
        {
            if (group == null)
            {
                Log.MakeLog(LoggerOperations.Error, "ArgumentNullException");
                throw new ArgumentNullException(nameof(group));
            }
            
            using (IDbConnection connection = _connection)
            {
                var p = GroupProperties(group);

                string sql = $@"insert into dbo.{Tables.Groups} (GroupNumber)
                                values (@GroupNumber); select @Id = @@IDENTITY ";

                connection.Execute(sql, p);
                return p.Get<int>("@Id");
            }
        }

        public GroupModel UpdateGroup(GroupModel group)
        {
            if (@group == null)
            {
                Log.MakeLog(LoggerOperations.Error, "ArgumentNullException");
                throw new ArgumentNullException(nameof(@group));
            }
            
            using (IDbConnection connection = _connection)
            {
                var p = GroupProperties(group);

                string sql = $@"update dbo.{Tables.Groups} set 
                            GroupNumber = @GroupNumber where Id = {group.Id}; select @Id = @@IDENTITY";
                connection.Execute(sql, p);

                return FindByID(group.Id);
            }
        }
    }
}
