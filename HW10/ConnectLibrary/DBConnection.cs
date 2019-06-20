using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using static HelperLibrary.Tools;
using static ConnectLibrary.LibrarySettings;

namespace ConnectLibrary
{
    public class DataAccess
    {
        public static List<string> InitializationCommands()
        {
            List<string> commands = new List<string>
            {
                @"CREATE TABLE Groups(Id int IDENTITY(1,1) PRIMARY KEY, GroupNumber nvarchar(50) not null);",
                
                @"CREATE TABLE Students(Id int IDENTITY(1,1) PRIMARY KEY,St_First_Name nvarchar(50) not null,
                                       St_Last_Name nvarchar(50) not null,Email nvarchar(50) not null,Phone varchar(20) not null, GroupNumberId int not null, CONSTRAINT fk_Groups
                                       FOREIGN KEY(GroupNumberId) REFERENCES Groups(Id) ON DELETE CASCADE);",

                @"CREATE TABLE Lecturers(Id int IDENTITY(1,1) PRIMARY KEY,Le_First_Name nvarchar(50) not null,
                                       Le_Last_Name nvarchar(50) not null,Email nvarchar(50) not null,Phone varchar(20));",

                @"CREATE TABLE Subjects(Id int IDENTITY(1,1) PRIMARY KEY,Title nvarchar(50) not null);",

                @"CREATE TABLE Lections(Id int IDENTITY(1,1) PRIMARY KEY,
                                        LecturersId int not null, CONSTRAINT fk_Lections FOREIGN KEY(LecturersId) 
                                        REFERENCES Lecturers(Id) ON DELETE CASCADE, SubjectsId int not null, CONSTRAINT fk_SubjectLecturerID
                                        FOREIGN KEY(SubjectsId) REFERENCES Subjects(Id) ON DELETE CASCADE);",

                @"CREATE TABLE Grades(Id int IDENTITY(1,1) PRIMARY KEY, LectionId int not null, CONSTRAINT fk_LectionId FOREIGN KEY(LectionId) 
                                        REFERENCES Lections(Id) ON DELETE CASCADE, StudentId int not null, CONSTRAINT fk_GradeStudentID FOREIGN KEY(StudentId) 
                                        REFERENCES Students(Id) ON DELETE CASCADE, Grade int not null, Date Date not null);"
            };
            return commands;
        }
        
        public static void AddTables()
        {
            using (IDbConnection connection = Connection)
            {
                connection.Open();
                foreach (var commands in InitializationCommands())
                {
                    using (IDbCommand command = Command(commands, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public static void Initialization()
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder(GetConnectionString());
            var databaseName = connectionStringBuilder.InitialCatalog;
            connectionStringBuilder.InitialCatalog = "master";

            using (var connection = new SqlConnection(connectionStringBuilder.ToString()))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"select * from dbo.sysdatabases where name='{databaseName}'";
                    // ExistCheck
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows) 
                            return;
                    }

                    command.CommandText = $"CREATE DATABASE {databaseName}";
                    command.ExecuteNonQuery();
                    
                    AddTables();
                }
            }
        }


    }
}
