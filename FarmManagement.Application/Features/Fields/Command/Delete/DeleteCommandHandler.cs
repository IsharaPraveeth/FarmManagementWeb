using FarmManagement.Application.Interfaces.Repositories;
using FarmManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmManagement.Application.Features.Fields.Command.Delete
{
    public class DeleteCommandHandler : IRequestHandler<DeleteCommand, bool>
    {
        private readonly IGenericRepository<Field> _repository;

        public DeleteCommandHandler(IGenericRepository<Field> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return false; // Entity not found

            await _repository.DeleteAsync(entity);
            return true;
        }
    }
}
