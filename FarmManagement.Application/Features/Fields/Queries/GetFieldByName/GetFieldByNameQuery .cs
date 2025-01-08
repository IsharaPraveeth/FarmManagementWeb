using MediatR;

namespace FarmManagement.Application.Features.Fields.Queries.GetFieldByName
{
    public class GetFieldByNameQuery : IRequest<GetFieldByNameDto>
    {
        public string Name { get; set; }
        public GetFieldByNameQuery(string name)
        {
            Name = name;
        }
    }
}
