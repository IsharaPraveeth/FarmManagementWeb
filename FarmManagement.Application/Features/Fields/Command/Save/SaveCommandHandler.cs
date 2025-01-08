using AutoMapper;
using FarmManagement.Application.Interfaces.Repositories;
using FarmManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmManagement.Application.Features.Fields.Command.Save
{
    internal class SaveCommandHandler : IRequestHandler<SaveCommand, int>
    {
        private readonly IGenericRepository<Field> _repository;

        public SaveCommandHandler(IGenericRepository<Field> repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(SaveCommand request, CancellationToken cancellationToken)
        {
            
            var obj = new Field
            {
                Name = request.Name,
                Area = request.Area,
                CropName = request.CropName,
                CreatedBy = 1, // hardcoded user id & date since the user login not implemented
                CreatedDate = DateTime.Now

            };

            await _repository.AddAsync(obj);

            return obj.Id;
        }
    }
}
