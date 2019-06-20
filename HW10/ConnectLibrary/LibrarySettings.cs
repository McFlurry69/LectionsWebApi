using System.Data;
using System.Data.SqlClient;
using ConnectLibrary.Logger;
using ConnectLibrary.Service;
using ConnectLibrary.SQLRepository;
using ConnectLibrary.SQLRepository.Interfaces;
using static HelperLibrary.Tools;

namespace ConnectLibrary
{
    public class LibrarySettings
    {
        internal static IDbConnection Connection => new SqlConnection(GetConnectionString());
        
        internal static IDbCommand Command(string command, IDbConnection connection) => new SqlCommand(command, (SqlConnection) connection);

        public static ILogging Log => new Log4NetLogger();
        
        internal static IGroupRepository SetGroupRepository => new GroupRepository();
        internal static IGradeRepository SetGradeRepository => new GradeRepository();
        internal static IStudentRepository SetStudentRepository => new StudentRepository();
        internal static ILecturerRepository SetLecturerRepository => new LecturerRepository();
        internal static ILectionsRepository SetLectionsRepository => new LectionRepository();
        internal static ICommonRepository SetCommonRepository => new CommonRepository();
        internal static ISubjectRepository SetSubjectRepository => new SubjectRepository();
    }
}