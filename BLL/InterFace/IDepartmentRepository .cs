using DAL.Entities;
using DAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.InterFace
{
    public interface IDepartmentRepository : IGenaricRepository<Department>
    {
        bool update(DepartmentViewModel depart);
    }
}
