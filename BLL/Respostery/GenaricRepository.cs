using BLL.InterFace;
using DAL.DataBase;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Respostery
{
    public class GenaricRepository<T> : IGenaricRepository<T> where T : BaseTable
    {
      
        public  ApplicationDbContext db { get;}
        protected readonly DbSet<T> _table;

        public GenaricRepository(ApplicationDbContext context)
        {
            this.db = context;
            _table=context.Set<T>();
        }
        public async Task<T> AddAsync(T entity)
        {
           
            return (await _table.AddAsync(entity)).Entity;
        }

        public async Task<T> DeleteAsync(int id)
        {  // Fetch the entity by ID

            var entity = await db.Set<T>().FindAsync(id);
            try
            {
                db.Set<T>().Remove(entity);
                await db.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw;
            }
           

         

            return entity;



        }
       



        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? query = null)
        {
            
            if (query !=null)
            {
               return _table.Where(query);
            }
            


            return await _table.ToListAsync();
        }
        public virtual async Task<T> EditAsync(T entity)
        {
            try
            {
                await EnsureEntityExists(entity);
                var data = _table.Update(entity).Entity;
            }
            catch (Exception)
            {

                throw;
            }
           

            return _table.Update(entity).Entity;
        }

        public async Task<T> GetByIdAsync(int id)
        {
           return await _table.FirstOrDefaultAsync(p => p.Id == id);
        }
        protected async Task EnsureEntityExists(T entity)
        {
            if (!await IsExists(entity))
                throw new Exception("Entity dosn't exist in database");
        }
        protected async Task<bool> IsExists(T entity)
        {
            return await _table.AnyAsync(p => p.Id == entity.Id);
        }
    }
}
