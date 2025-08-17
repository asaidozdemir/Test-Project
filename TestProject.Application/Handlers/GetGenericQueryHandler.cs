using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TestProject.Core.Commands;
using TestProject.Core.Constants;
using TestProject.Infrastructure;

namespace TestProject.Application.Handlers
{
    public class GetGenericQueryHandler : IRequestHandler<GetGenericQuery, List<Dictionary<string, object>>>
    {
        private readonly DatabaseExecutor _db;

        public GetGenericQueryHandler(DatabaseExecutor db)
        {
            _db = db;
        }

        public async Task<List<Dictionary<string, object>>> Handle(GetGenericQuery request, CancellationToken cancellationToken)
        {
            var rows = await _db.ExecuteAsync(DbConstants.GetUsers, parameters: null, isStoredProcedure: true, cancellationToken: cancellationToken);
            return rows;
        }
    }
}
