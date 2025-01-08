using FarmManagement.Application.Interfaces.Repositories;
using FarmManagement.Domain.Common;
using FarmManagement.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FarmManagement.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseAuditEntity
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> Entities => _dbContext.Set<T>();

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
             T exist = _dbContext.Set<T>()
                           .Find(entity.Id);

            if (exist != null)
            {
                _dbContext.Entry(exist).CurrentValues
                    .SetValues(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbContext
                .Set<T>()
                .ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id) => await _dbContext.Set<T>().FindAsync(id);
    }
}
