using FarmManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmManagement.Domain.Entities
{
    public class Field :BaseAuditEntity
    {
        public string Name { get; set; }
        public int Area { get; set; }
        public string CropName { get; set; }

    }
}
