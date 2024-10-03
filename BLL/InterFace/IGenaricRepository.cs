using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.InterFace
{
    public interface IGenaricRepository<T> where T : BaseTable
    {
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? query=null);
        Task<T> GetByIdAsync(int id);
     
        Task<T> AddAsync(T entity);
        Task<T> EditAsync(T entity);

        Task<T> DeleteAsync(int id);

    }
}
