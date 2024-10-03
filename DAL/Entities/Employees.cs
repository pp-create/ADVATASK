using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Employees:BaseTable
    {
        
           
            public decimal Salary { get; set; }
        [ForeignKey("DepartmentId")]
        public int DepartmentId { get; set; }
            public Department Departments { get; set; }
        [ForeignKey("ManagerId")]
        public int? ManagerId { get; set; }
            public Employees Manager { get; set; }
        
    }
}
