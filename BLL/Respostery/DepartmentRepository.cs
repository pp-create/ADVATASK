using BLL.InterFace;
using DAL.DataBase;
using DAL.Entities;
using DAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Respostery
{
    public class DepartmentRepository : GenaricRepository<Department>, IDepartmentRepository
    {
        private readonly ApplicationDbContext db;

        public DepartmentRepository(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }
        public bool update(DepartmentViewModel depart)

        {
            Department department = new Department();
            var data = db.Department_TBL.Where(x => x.Id == depart.Id).FirstOrDefault();
            if (data!=null)
            {
                data.Name = depart.Name;
                data.ManagerId = depart.ManagerId;


                return true;
            }
            return false;
        }
    }
}
