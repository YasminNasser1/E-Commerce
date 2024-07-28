using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repo
{
    public interface IGenaricrepository <T> where T : BaseEntity
    {

        Task<IEnumerable<T>> GetAllAsync();


        Task<T> GetByIdAsync(int id);

        Task UpdateAsync(T entity);

        Task DeleteAsync(int id);

        Task CreateAsync(T entity);

    }
}
