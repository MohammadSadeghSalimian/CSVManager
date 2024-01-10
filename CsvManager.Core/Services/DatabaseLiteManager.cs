using System.Globalization;
using System.Text;
using Humanizer;
using Microsoft.Data.Sqlite;

namespace CsvManager.Core.Services;

public class DatabaseLiteManager : IDatabaseManager, IDisposable
{


    private StreamReader? _stream;
    private readonly FileInfo _databaseFile;
    private readonly FileInfo _csvFile;
    private SqliteConnectionStringBuilder? _connectionBuilder;
    private string[]? _ignoredColumns;
    private string? _tableName;
    private List<(string Name, int Index)>? _tableColumns;
    private SqliteConnection? _connection;

    public DatabaseLiteManager(FileInfo csvFile, FileInfo databaseFile, params string[] ignoredColumns)
    {
        _csvFile = csvFile;
        _databaseFile = databaseFile;
        _ignoredColumns = ignoredColumns;
    }

    public void SetIgnoredColumns(params string[] ignoredColumns)
    {
        _ignoredColumns = ignoredColumns;
    }

    private void OpenCsv()
    {
        _stream = new StreamReader(_csvFile.FullName);
    }

    public void CreateDatabase(string tableName)
    {
        OpenCsv();
        CreateDatabaseFile();
        
        CreateTable(tableName, _ignoredColumns);

    }

    private void CloseCsv()
    {
        _stream?.Close();
    }

    private void CreateDatabaseFile()
    {
        _connectionBuilder = new SqliteConnectionStringBuilder
        {
            DataSource = _databaseFile.FullName
        };
    }

    private void CreateTable(string table, IEnumerable<string>? ignoredColumns)
    {

        _tableName = table;
        var hasSet = ignoredColumns != null ? ignoredColumns.Distinct().ToHashSet() : new HashSet<string>();
         
        _stream?.BaseStream.Seek(0, SeekOrigin.Begin);
        _stream?.DiscardBufferedData();
        var header = _stream?.ReadLine();
        if (header == null)
        {
            return;
        }

        var columns = header.Split(',', StringSplitOptions.RemoveEmptyEntries);
        if (_connectionBuilder==null)
        {
            return;
        }
        _connection = new SqliteConnection(_connectionBuilder.ConnectionString);
        _connection.Open();
        var sb = new StringBuilder();
        sb.Append($"CREATE TABLE IF NOT EXISTS [{_tableName}] (");
        sb.AppendLine("[ID] INTEGER NOT NULL UNIQUE PRIMARY KEY,");
        sb.AppendLine($"[{columns[0]}] TEXT NOT NULL UNIQUE,");
        var n = columns.Length;
        var l = new List<string>(n - 1);
        _tableColumns = new List<(string, int)>(n) { ("ID", 0), (columns[0], 0) };

        for (var i = 1; i < n; i++)
        {
            var column = columns[i];
            if (hasSet.Contains(column))
            {
                continue;
            }

            l.Add($"[{column}] REAL NOT NULL");
            _tableColumns.Add((column, i));
        }

        sb.AppendLine(string.Join(',', l));
        sb.AppendLine(");");

        using var command = _connection.CreateCommand();
        command.CommandText = sb.ToString();
        command.ExecuteNonQuery();
        _connection.Close();
    }


    public void BulkInsert(DateTime referenceDate)
    {
        if (_tableColumns==null)
        {
            throw new ApplicationException("Table is not created");
        }
        if ( _connection == null )
        {
            throw new ApplicationException("Connection is not established");
        }

        if (_stream == null)
        {
            throw new ApplicationException("Csv file problem");
        }

        var sb = new StringBuilder();
        sb.Append($"INSERT INTO {_tableName} (");
        sb.Append(string.Join(", ", _tableColumns.Select(x => $"[{x.Name}]")));
        sb.Append(") VALUES (");
        sb.Append(string.Join(", ", _tableColumns.Select(x => $"@{x.Name.Pascalize().Replace("-", "")}")));
        sb.Append(")");
        _connection.Open();
        using var tr = _connection.BeginTransaction();
        using var cmd = _connection.CreateCommand();
        cmd.Transaction = tr;
        cmd.CommandText = sb.ToString();
        var pp = _tableColumns.Select(x => "@" + x.Name.Pascalize().Replace("-", "")).ToArray();
        var ind = _tableColumns.Select(x => x.Index).ToArray();
        while (!_stream.EndOfStream)
        {
            var text = _stream.ReadLine();
            if (text == null)
            {
                continue;
            }

            var textData = text.Split(',', StringSplitOptions.RemoveEmptyEntries);
            var date = DateTime.ParseExact(textData[0], "dd/MM/yyyy HH:mm:ss.ffffff", CultureInfo.InvariantCulture);
            var xn = (long)(date - referenceDate).TotalMilliseconds;
            cmd.Parameters.AddWithValue(pp[0], xn);
            cmd.Parameters.AddWithValue(pp[1], date.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
            for (int i = 2; i < _tableColumns.Count; i++)
            {
                cmd.Parameters.AddWithValue(pp[i], double.Parse(textData[ind[i]]));
            }

            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
        }

        tr.Commit();
        _connection.Close();
    }

    public void Dispose()
    {
        _stream?.Dispose();
        _connection?.Close();
        _connection?.Dispose();
    }
}