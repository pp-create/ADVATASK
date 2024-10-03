using BLL.InterFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        IEmployeRep Employee { get; }
        IDepartmentRepository Departments { get; }

        Task<int> complete();
    }
}
