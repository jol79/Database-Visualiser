namespace DatabaseVisualiser.Data
{
    public interface IDbService
    {
        Task<IEnumerable<string>> GetTableNames();
        Task AddTable(string tableName);
        Task EditTable(string oldTableName, string newTableName);
        Task DeleteTable(string tableName);
    }
}
