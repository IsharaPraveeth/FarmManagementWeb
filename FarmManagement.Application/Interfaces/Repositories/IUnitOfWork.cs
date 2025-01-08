using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmManagement.Domain.Common;
namespace FarmManagement.Application.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<T> Repository<T>() where T : BaseAuditEntity;
    Task<int> Save(CancellationToken cancellationToken);
    Task Rollback();
}
