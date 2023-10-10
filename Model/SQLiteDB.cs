using GPTArticleGen.Model;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;

public class SQLiteDB
{
    private SQLiteConnection connection;

    #region Basic Commands
    public SQLiteDB(string connectionString)
    {
        connection = new SQLiteConnection();
    }

    public SQLiteDB()
    {
        connection = new SQLiteConnection("Data Source=Articles.db;Version=3;");
    }

    public void OpenConnection()
    {
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }
    }

    public void CloseConnection()
    {
        if (connection.State != ConnectionState.Closed)
        {
            connection.Close();
        }
    }

    public SQLiteCommand CreateCommand()
    {
        return connection.CreateCommand();
    }
    #endregion

    #region CRUD
    public async Task InsertArticleAsync(ArticleModel articleModel)
    {
        SQLiteCommand command = CreateCommand();
        command.CommandText = "INSERT INTO Articles (Title, Content, Tags, Prompt) VALUES (@Title, @Content, @Tags, @Prompt)";
        command.Parameters.AddWithValue("@Title", articleModel.Title);
        command.Parameters.AddWithValue("@Content", articleModel.Content);
        command.Parameters.AddWithValue("@Tags", articleModel.Tags);
        command.Parameters.AddWithValue("@Prompt", articleModel.Prompt);
        try
        {
            await command.ExecuteNonQueryAsync();
        }
        catch (SQLiteException e)
        {
            throw e;
        }
    }

    public async Task<DataTable> GetAllArticleAsync()
    {
        SQLiteCommand selectCommand = CreateCommand();
        selectCommand.CommandText = "SELECT * FROM Articles";
        SQLiteDataAdapter adapter = new SQLiteDataAdapter(selectCommand);
        DataTable dataTable = new DataTable();
        await Task.Run(() => adapter.Fill(dataTable)); // Run the Fill operation asynchronously
        return dataTable;
    }

    public async Task UpdateArticleAsync(ArticleModel articleModel)
    {
        SQLiteCommand command = CreateCommand();
        command.CommandText = "UPDATE Articles SET Title = @Title, Content = @Content, Tags = @Tags, Prompt = @Prompt WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", articleModel.Id);
        command.Parameters.AddWithValue("@Title", articleModel.Title);
        command.Parameters.AddWithValue("@Content", articleModel.Content);
        command.Parameters.AddWithValue("@Tags", articleModel.Tags);
        command.Parameters.AddWithValue("@Prompt", articleModel.Prompt);
        try
        {
            await command.ExecuteNonQueryAsync();
        }
        catch (SQLiteException e)
        {
            throw e;
        }
    }

    public async Task DeleteArticleAsync(ArticleModel articleModel)
    {
        SQLiteCommand command = CreateCommand();
        command.CommandText = "DELETE FROM Articles WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", articleModel.Id);
        try
        {
            await command.ExecuteNonQueryAsync();
        }
        catch (SQLiteException e)
        {
            throw e;
        }
    }

    public async Task<int> GetLastArticleIdAsync()
    {
        SQLiteCommand command = CreateCommand();
        command.CommandText = "SELECT Id FROM Articles ORDER BY Id DESC LIMIT 1";
        DbDataReader reader = await command.ExecuteReaderAsync();
        int id = 0;

        if (reader is SQLiteDataReader sqliteReader)
        {
            while (await sqliteReader.ReadAsync())
            {
                id = sqliteReader.GetInt32(0);
            }
        }
        else
        {
            throw new InvalidOperationException("Unexpected database reader type.");
        }

        return id;
    }

    #endregion
}
