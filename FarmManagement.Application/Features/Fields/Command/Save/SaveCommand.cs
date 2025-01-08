using FarmManagement.Application.Common.Mappings;
using FarmManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmManagement.Application.Features.Fields.Command.Save
{
    public class SaveCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Area { get; set; }
        public string CropName { get; set; }

    }
}
