using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelperLibrary.Models.Interfaces;

namespace HelperLibrary.Models
{
    public class GroupModel: ITable
    {
        public GroupModel(string groupNumber)
        {
            GroupNumber = groupNumber;
        }

        public GroupModel(){}
        public int Id { get; set; }
        public string GroupNumber { get; set; }
    }
}
