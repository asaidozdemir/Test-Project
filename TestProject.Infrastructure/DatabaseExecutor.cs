using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace TestProject.Infrastructure
{
    public class DatabaseExecutor
    {
        private readonly string _connectionString;

        public DatabaseExecutor(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Executes a query or stored procedure and returns rows as dictionaries
        /// </summary>
        public async Task<List<Dictionary<string, object>>> ExecuteAsync(
            string spOrQuery,
            Dictionary<string, object>? parameters = null,
            bool isStoredProcedure = false,
            CancellationToken cancellationToken = default)
        {
            var result = new List<Dictionary<string, object>>();

            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync(cancellationToken);

            await using var cmd = new NpgsqlCommand(spOrQuery, conn);

            cmd.CommandTimeout = 30; // seconds

            if (isStoredProcedure)
                cmd.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
            {
                foreach (var kvp in parameters)
                    cmd.Parameters.AddWithValue(kvp.Key, kvp.Value ?? System.DBNull.Value);
            }

            await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);

            while (await reader.ReadAsync(cancellationToken))
            {
                var row = new Dictionary<string, object>();

                for (int i = 0; i < reader.FieldCount; i++)
                    row[reader.GetName(i)] = reader.GetValue(i);

                result.Add(row);
            }

            return result;
        }
    }
}
