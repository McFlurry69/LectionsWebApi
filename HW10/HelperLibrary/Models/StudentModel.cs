using HelperLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelperLibrary.Models.Interfaces;

namespace HelperLibrary.Models
{
    public class StudentModel : IPerson, ITable
    {
        public StudentModel(string firstname, string lastname, string email, string phone, int groupId)
        {
            St_First_Name = firstname;
            St_Last_Name = lastname;
            Email = email;
            Phone = phone;
            GroupNumberId = groupId;
        }

        public StudentModel() {}
        public int Id { get; set; }
        public string St_First_Name { get; set; }
        public string St_Last_Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int GroupNumberId { get; set; }
    }
}
