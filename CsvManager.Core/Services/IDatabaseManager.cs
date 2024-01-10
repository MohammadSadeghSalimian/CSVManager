namespace CsvManager.Core.Services;

public interface IDatabaseManager
{
    void SetIgnoredColumns(params string[] ignoredColumns);
    void CreateDatabase(string tableName);
    void BulkInsert(DateTime referenceDate);
}