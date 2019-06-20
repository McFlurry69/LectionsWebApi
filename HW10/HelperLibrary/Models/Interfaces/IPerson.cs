using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperLibrary.Models.Interfaces
{
    public interface IPerson : ITable
    {
        string Email { get; }
        string Phone { get; }
    }
}
