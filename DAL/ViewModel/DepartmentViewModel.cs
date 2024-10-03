using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModel
{
    public class DepartmentViewModel :BaseViewModel
    {
        public int? ManagerId { get; set; }
        public string? ManagerName { get; set; }
        public IEnumerable<Employees>? Managers { get; set; } = null;


    }
}
