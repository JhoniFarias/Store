using Store.Domain.Commands.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Handlers.Interfaces
{
    public interface IHandler<T> where T : Icommand
    {
        ICommandResult Handle(T command);
    }
}
