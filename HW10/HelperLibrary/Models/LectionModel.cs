using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelperLibrary.Models.Interfaces;

namespace HelperLibrary.Models
{
    public class LectionModel : ITable
    {
        public LectionModel(int lecturerId, int subjectId)
        {
            LecturersId = lecturerId;
            SubjectsId = subjectId;
        }

        public LectionModel(){}
        public int Id { get; set; }
        public int LecturersId { get; set; }
        public int SubjectsId { get; set; }
    }
}
