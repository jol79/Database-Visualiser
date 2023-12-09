using Dapper;
using Npgsql;
using System.Data;
using System.Data.Common;

namespace DatabaseVisualiser.Data
{
    public class DbService: IDbService
    {
        private readonly string _connectionString;

        public DbService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<string>> GetTableNames()
        {
            using IDbConnection dbConnection = new NpgsqlConnection(_connectionString);
            dbConnection.Open();
            return await dbConnection.QueryAsync<string>("SELECT table_name FROM information_schema.tables WHERE table_schema='public'");
        }

        public async Task AddTable(string tableName)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(_connectionString);
            dbConnection.Open();
            await dbConnection.ExecuteAsync($"CREATE TABLE {tableName} (id serial PRIMARY KEY)");
        }

        public async Task EditTable(string oldTableName, string newTableName)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(_connectionString);
            dbConnection.Open();
            await dbConnection.ExecuteAsync($"ALTER TABLE {oldTableName} RENAME TO {newTableName}");
        }

        public async Task DeleteTable(string tableName)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(_connectionString);
            dbConnection.Open();
            await dbConnection.ExecuteAsync($"DROP TABLE {tableName}");
        }
    }
}
