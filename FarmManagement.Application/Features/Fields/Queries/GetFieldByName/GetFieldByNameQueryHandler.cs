using AutoMapper;
using AutoMapper.QueryableExtensions;
using FarmManagement.Application.Interfaces.Repositories;
using FarmManagement.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmManagement.Application.Features.Fields.Queries.GetFieldByName
{
    public class GetFieldByNameQueryHandler : IRequestHandler<GetFieldByNameQuery, GetFieldByNameDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetFieldByNameQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetFieldByNameDto> Handle(GetFieldByNameQuery request, CancellationToken cancellationToken)
        {
            var field = await _unitOfWork.Repository<Field>()
                .Entities
                .Where(f => f.Name == request.Name)
                .ProjectTo<GetFieldByNameDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            return field;
        }
    }
}
