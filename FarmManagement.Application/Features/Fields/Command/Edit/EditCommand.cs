using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmManagement.Application.Features.Fields.Command.Edit
{
    public class EditCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public int Area { get; set; }
        public string CropName { get; set; }
    }
}
