using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmManagement.Application.Features.Fields.Command.Delete
{
    public class DeleteCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
