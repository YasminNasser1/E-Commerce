using BusinessLogicLayer.Repo;
using DataAccessLayer.Context;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BusinessLogicLayer
{

    public class GenaricRepository<T> : IGenaricrepository<T> where T : BaseEntity
    {
        private readonly E_CommerceDbContext _dbContext;

        public GenaricRepository(E_CommerceDbContext dbContext ) 
        { 
            _dbContext= dbContext;
        }
        public async Task CreateAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _dbContext.Set<T>().Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
          => await _dbContext.Set<T>().ToListAsync();


        public async Task<T> GetByIdAsync(int id)
          => await _dbContext.Set<T>().Where(x => x.Id == id).FirstOrDefaultAsync();


        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }

}
