using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Domain
{
    public abstract class BaseAuditEntity : BaseEntity<long>
    {
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? LastModifiedAt { get; set; }
    }
}
