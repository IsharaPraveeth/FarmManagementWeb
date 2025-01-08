using FarmManagement.Application.Interfaces.Repositories;
using FarmManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmManagement.Infrastructure.Repositories
{
    public class FieldRepository : IFieldRepository
    {
        private readonly IGenericRepository<Field> _repository;

        public FieldRepository(IGenericRepository<Field> repository)
        {
            _repository = repository;
        }

        public async Task<List<Field>> GetAllFieldAsync()
        {
            return await _repository.Entities.OrderByDescending(x=>x.Name).ToListAsync();
        }

        public async Task<Field> GetFieldByNameAsync(string name)
        {
            return await _repository.Entities.Where(x => x.Name == name).FirstOrDefaultAsync();
        }
    }
}
