using System;
using MediatR;

namespace CsvManager.Commands
{
    public record ShowExceptionErrorCommand(Exception Exception):IRequest<Unit>
    {
    }
}
