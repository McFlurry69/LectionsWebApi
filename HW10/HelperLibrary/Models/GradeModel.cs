using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelperLibrary.Models.Interfaces;

namespace HelperLibrary.Models
{
    public class GradeModel : ITable
    {
        public GradeModel(int stdentId, int lectionId, int grade, DateTime date)
        {
            StudentId = stdentId;
            LectionId = lectionId;
            Grade = grade;
            Date = date;
        }

        public GradeModel(){}
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int LectionId { get; set; }
        
        public int Grade { get; set; }
        public DateTime Date { get; set; }
    }
}
