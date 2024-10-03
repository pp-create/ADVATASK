using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModel
{
    public class EmployeeViewModel:BaseViewModel
    {
        public decimal Salary { get; set; }
      
        public int DepartmentId { get; set; }
       
        public int? ManagerId { get; set; }
        public string? DepartmentName { get; set; }
        public string? ManagerName { get; set; }
        public IEnumerable<Department>? Departments { get; set; } = null;
        public IEnumerable<Employees>? Managers { get; set; } = null;
    }
}
