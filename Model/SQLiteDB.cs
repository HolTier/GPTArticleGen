using GPTArticleGen.Model;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Diagnostics;

public class SQLiteDB
{
    private SQLiteConnection _connection;
    private string _connectionString = "Data Source=database.db;";
    private static readonly Mutex DbMutex = new Mutex();

    #region Basic Commands
    public SQLiteDB(string connectionString)
    {
        _connection = new SQLiteConnection();

        OpenConnection();
        // Create table of Pages if not exists
        string createPagesTableQuery = @"CREATE TABLE IF NOT EXISTS Pages (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                site TEXT,
                username TEXT,
                password TEXT
            )";
        SQLiteCommand createPagesTableCommand = CreateCommand();
        createPagesTableCommand.CommandText = createPagesTableQuery;
        createPagesTableCommand.ExecuteNonQuery();

        // Create table if not exists
        string createTableQuery = @"CREATE TABLE IF NOT EXISTS Articles (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                title TEXT,
                content TEXT,
                tags TEXT,
                promptTitle TEXT,
                prompt TEXT,
                isPublished BOOLEAN,
                image_id INTEGER,
                page_id INTEGER REFERENCES Pages(id)
                
            )";
        SQLiteCommand createTableCommand = CreateCommand();
        createTableCommand.CommandText = createTableQuery;
        createTableCommand.ExecuteNonQuery();

        string createLogsTableQuery = @"CREATE TABLE IF NOT EXISTS Logs (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                promptTitle TEXT,
                site TEXT,
                postUrl TEXT,
                postId INTEGER,
                username TEXT,
                date TEXT
            )";
        SQLiteCommand createLogsTableCommand = CreateCommand();
        createLogsTableCommand.CommandText = createLogsTableQuery;
        createLogsTableCommand.ExecuteNonQuery();
        CloseConnection();
    }

    public SQLiteDB()
    {
        _connection = new SQLiteConnection(_connectionString);
        OpenConnection();
        // Create table of Pages if not exists
        string createPagesTableQuery = @"CREATE TABLE IF NOT EXISTS Pages (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                site TEXT,
                username TEXT,
                password TEXT
            )";
        SQLiteCommand createPagesTableCommand = CreateCommand();
        createPagesTableCommand.CommandText = createPagesTableQuery;
        createPagesTableCommand.ExecuteNonQuery();

        // Create table if not exists
        string createTableQuery = @"CREATE TABLE IF NOT EXISTS Articles (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                title TEXT,
                content TEXT,
                tags TEXT,
                promptTitle TEXT,
                prompt TEXT,
                isPublished BOOLEAN,
                image_id INTEGER,
                page_id INTEGER REFERENCES Pages(id)
                
            )";
        SQLiteCommand createTableCommand = CreateCommand();
        createTableCommand.CommandText = createTableQuery;
        createTableCommand.ExecuteNonQuery();
        string createLogsTableQuery = @"CREATE TABLE IF NOT EXISTS Logs (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                promptTitle TEXT,
                site TEXT,
                postUrl TEXT,
                postId INTEGER,
                username TEXT,
                date TEXT
            )";
        SQLiteCommand createLogsTableCommand = CreateCommand();
        createLogsTableCommand.CommandText = createLogsTableQuery;
        createLogsTableCommand.ExecuteNonQuery();
        CloseConnection();
    }

    public void OpenConnection()
    {
        if (_connection.State != ConnectionState.Open)
        {
            _connection.Open();
        }
    }

    public void CloseConnection()
    {
        if (_connection.State != ConnectionState.Closed)
        {
            _connection.Close();
        }
    }

    public SQLiteCommand CreateCommand()
    {
        return _connection.CreateCommand();
    }
    #endregion

    #region CRUD Logs
    public async Task InsertLogAsync(LogsModel logsModel)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO Logs (PromptTitle, Site, PostUrl, PostId, Username, Date) VALUES (@PromptTitle, @Site, @PostUrl, @PostId, @Username, @Date)";
                command.Parameters.AddWithValue("@PromptTitle", logsModel.PromptTitle);
                command.Parameters.AddWithValue("@Site", logsModel.Site);
                command.Parameters.AddWithValue("@PostUrl", logsModel.PostUrl);
                command.Parameters.AddWithValue("@PostId", logsModel.PostId);
                command.Parameters.AddWithValue("@Username", logsModel.Username);
                command.Parameters.AddWithValue("@Date", logsModel.Date);

                try
                {
                    await command.ExecuteNonQueryAsync();
                }
                catch (SQLiteException e)
                {
                    throw e;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }

    public async Task<BindingList<LogsModel>> GetAllLogs()
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Logs";

                try
                {
                    DbDataReader reader = await command.ExecuteReaderAsync();
                    var logs = new BindingList<LogsModel>();

                    if (reader is SQLiteDataReader sqliteReader)
                    {
                        while (await sqliteReader.ReadAsync())
                        {
                            LogsModel log = new LogsModel
                            {
                                Id = sqliteReader.GetInt32(0),
                                PromptTitle = sqliteReader.IsDBNull(1) ? null : sqliteReader.GetString(1),
                                Site = sqliteReader.IsDBNull(2) ? null : sqliteReader.GetString(2),
                                PostUrl = sqliteReader.IsDBNull(3) ? null : sqliteReader.GetString(3),
                                PostId = sqliteReader.GetInt32(4),
                                Username = sqliteReader.IsDBNull(5) ? null : sqliteReader.GetString(5),
                                Date = sqliteReader.IsDBNull(6) ? default(DateTime) : DateTime.Parse(sqliteReader.GetString(6))
                            };
                            logs.Add(log);
                        }
                        sqliteReader.Close();
                    }

                    await reader.DisposeAsync();
                    return logs;
                }
                catch (SQLiteException e)
                {
                    throw e;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }

    #endregion
}
