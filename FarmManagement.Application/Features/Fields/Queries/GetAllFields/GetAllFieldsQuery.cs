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

namespace FarmManagement.Application.Features.Fields.Queries.GetAllFields
{
    public record GetAllFieldsQuery : IRequest<List<GetAllFieldsDto>>;

    internal class GetAllFieldsQueryHandler : IRequestHandler<GetAllFieldsQuery, List<GetAllFieldsDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllFieldsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetAllFieldsDto>> Handle(GetAllFieldsQuery query, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Repository<Field>().Entities
                   .ProjectTo<GetAllFieldsDto>(_mapper.ConfigurationProvider)
                   .ToListAsync(cancellationToken);
        }
    }
}
