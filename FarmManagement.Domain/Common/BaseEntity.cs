using FarmManagement.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmManagement.Domain.Common
{
    public abstract class BaseEntity :IEntity
    {
        public int Id { get; set; }
    }
}
