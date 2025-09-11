using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Messaging
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
