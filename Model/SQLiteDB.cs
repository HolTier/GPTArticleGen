using GPTArticleGen.Model;
using System.Data;
using System.Data.SQLite;

public class SQLiteDB
{
    private SQLiteConnection connection;

    #region Basic Commands
    public SQLiteDB(string connectionString)
    {
        connection = new SQLiteConnection(connectionString);
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
    public void InsertArticle(ArticleModel articleModel)
    {
        SQLiteCommand command = CreateCommand();
        command.CommandText = "INSERT INTO Article (Title, Content, Tags, Prompt) VALUES (@Title, @Content, @Tags, @Prompt)";
        command.Parameters.AddWithValue("@Title", articleModel.Title);
        command.Parameters.AddWithValue("@Content", articleModel.Content);
        command.Parameters.AddWithValue("@Author", articleModel.Tags);
        command.Parameters.AddWithValue("@Date", articleModel.Prompt);
        try
        {
            command.ExecuteNonQuery();
        }
        catch (SQLiteException e)
        {
            throw e;
        }
    }

    public DataTable GetAllArticle()
    {
        SQLiteCommand selectCommand = CreateCommand();
        selectCommand.CommandText = "SELECT * FROM Article";
        SQLiteDataAdapter adapter = new SQLiteDataAdapter(selectCommand);
        DataTable dataTable = new DataTable();
        adapter.Fill(dataTable);
        return dataTable;
    }

    public void UpdateArticle(ArticleModel articleModel)
    {
        SQLiteCommand command = CreateCommand();
        command.CommandText = "UPDATE Article SET Title = @Title, Content = @Content, Tags = @Tags, Prompt = @Prompt WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", articleModel.Id);
        command.Parameters.AddWithValue("@Title", articleModel.Title);
        command.Parameters.AddWithValue("@Content", articleModel.Content);
        command.Parameters.AddWithValue("@Tags", articleModel.Tags);
        command.Parameters.AddWithValue("@Prompt", articleModel.Prompt);
        try
        {
            command.ExecuteNonQuery();
        }
        catch (SQLiteException e)
        {
            throw e;
        }
    }

    public void DeleteArticle(ArticleModel articleModel)
    {
        SQLiteCommand command = CreateCommand();
        command.CommandText = "DELETE FROM Article WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", articleModel.Id);
        try
        {
            command.ExecuteNonQuery();
        }
        catch (SQLiteException e)
        {
            throw e;
        }
    }

    public int GetastArticleId()
    {
        SQLiteCommand command = CreateCommand();
        command.CommandText = "SELECT Id FROM Article ORDER BY Id DESC LIMIT 1";
        SQLiteDataReader reader = command.ExecuteReader();
        int id = 0;
        while (reader.Read())
        {
            id = reader.GetInt32(0);
        }
        return id;
    }

    #endregion
}
