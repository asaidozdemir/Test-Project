using System.Collections.Generic;
using MediatR;

namespace TestProject.Core.Commands
{
    public class GetGenericQuery : IRequest<List<Dictionary<string, object>>>
    {
    }
}
