using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelperLibrary.Models.Interfaces;

namespace HelperLibrary.Models
{
    public class SubjectModel : ITable
    {
        public SubjectModel(string title)
        {
            Title = title;
        }

        public SubjectModel(){}
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
