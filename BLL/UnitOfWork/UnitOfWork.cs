using BLL.InterFace;
using BLL.Respostery;
using DAL.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext db;

        public UnitOfWork(ApplicationDbContext db) 
        {
            this.db = db;
            Employee= new EmployeRep(db);
            Departments = new DepartmentRepository(db);
        }
        public IEmployeRep Employee {  get;private set; }

        public IDepartmentRepository Departments { get; private set; }


        public async Task <int> complete()
        {
            return await db.SaveChangesAsync();
        }

        public void Dispose()
        {
            db.Dispose();
        }
       
    }
}
