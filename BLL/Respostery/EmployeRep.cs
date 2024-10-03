using BLL.InterFace;
using DAL.DataBase;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Respostery
{
    public class EmployeRep : GenaricRepository<Employees>, IEmployeRep
    {
        private readonly ApplicationDbContext db;
        public EmployeRep(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }
        public async Task<IEnumerable<Employees>> GetAllWithDetailsAsync()
        {
            return await db.Employees_TBL
                                 .Include(e => e.Departments)
                                 .Include(e => e.Manager)
                                 .ToListAsync();
        }



    }
}
