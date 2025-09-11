using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityFrameworkCore.Interceptors
{
    public class AuditEntityInterceptor(IUser currentUser) : SaveChangesInterceptor
    {
    }
}
