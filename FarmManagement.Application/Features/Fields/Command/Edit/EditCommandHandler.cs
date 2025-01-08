using FarmManagement.Application.Interfaces.Repositories;
using FarmManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmManagement.Application.Features.Fields.Command.Edit
{
    public class EditCommandHandler : IRequestHandler<EditCommand, bool>
    {
        private readonly IGenericRepository<Field> _repository;

        public EditCommandHandler(IGenericRepository<Field> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(EditCommand request, CancellationToken cancellationToken)
        {
  
            var obj = await _repository.GetByIdAsync(request.Id);
            if (obj == null)
                return false; // not found


            obj.Area = request.Area;
            obj.CropName = request.CropName;
            obj.UpdatedBy = 2;
            obj.UpdatedDate = DateTime.Now;

            await _repository.UpdateAsync(obj);

            return true;
        }

    }
}
