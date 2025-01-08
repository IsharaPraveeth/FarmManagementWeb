using FarmManagement.Application.Common.Mappings;
using FarmManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmManagement.Application.Features.Fields.Queries.GetAllFields
{
    public class GetAllFieldsDto : IMapFrom<Field>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Area { get; set; }
        public string CropName { get; set; }

    }
}
