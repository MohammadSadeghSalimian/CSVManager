using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CsvManager.Commands
{
    public record ShowExceptionErrorCommand(Exception Exception):IRequest<Unit>
    {
    }
}
