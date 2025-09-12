using SharedKernel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSACommerce.UserService.Core.Entites
{
    public class TbUser : BaseAuditEntity, IAggregateRoot
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Phone { get; set; }
        public string Salt { get; set; } = null!;
    }

}
