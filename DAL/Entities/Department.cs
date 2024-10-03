using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Department:BaseTable
    {

        [ForeignKey("ManagerId")]
        public int? ManagerId { get; set; }
            public Employees Manager { get; set; }

            public List<Employees> Employee { get; set; }
      
    }
}
