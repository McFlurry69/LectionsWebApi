using HelperLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelperLibrary.Models.Interfaces;

namespace HelperLibrary.Models
{
    public class LecturerModel : IPerson, ITable
    {
        public LecturerModel(string firstname, string lastname, string email, string phone = null)
        {
            Le_First_Name = firstname;
            Le_Last_Name = lastname;
            Email = email;
            Phone = phone;
        }

        public LecturerModel() { }

        public int Id { get; set; }
        public string Le_First_Name { get; set; }
        public string Le_Last_Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
