using FarmManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmManagement.Application.Interfaces.Repositories
{
    public interface IFieldRepository
    {
        Task<List<Field>> GetAllFieldAsync();
        Task<Field> GetFieldByNameAsync(string name);
    }
}
