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
        connection = new SQLiteConnection("Data Source=WordpressArticles.db;Version=3;");
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

    #region CRUD Article
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

    public async Task UpdateArticleImageId(int articleId, int imageId)
    {
        OpenConnection();
        SQLiteCommand command = CreateCommand();
        command.CommandText = "UPDATE Articles SET ImageId = @ImageId WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", articleId);
        command.Parameters.AddWithValue("@ImageId", imageId);
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
            CloseConnection();
        }
    }

    #endregion

    #region CRUD Page
    public async Task InsertPageAsync(PageModel pageModel)
    {
        SQLiteCommand command = CreateCommand();
        command.CommandText = "INSERT INTO Pages (Site, Username, Password) VALUES (@Site, @Username, @Password)";
        command.Parameters.AddWithValue("@Site", pageModel.Site);
        command.Parameters.AddWithValue("@Username", pageModel.Username);
        command.Parameters.AddWithValue("@Password", pageModel.Password);
        try
        {
            await command.ExecuteNonQueryAsync();
        }
        catch (SQLiteException e)
        {
            throw e;
        }
    }

    public async Task<DataTable> GetAllPageAsync()
    {
        SQLiteCommand selectCommand = CreateCommand();
        selectCommand.CommandText = "SELECT * FROM Pages";
        SQLiteDataAdapter adapter = new SQLiteDataAdapter(selectCommand);
        DataTable dataTable = new DataTable();
        await Task.Run(() => adapter.Fill(dataTable)); // Run the Fill operation asynchronously
        return dataTable;
    }

    public async Task UpdatePageAsync(PageModel pageModel)
    {
        SQLiteCommand command = CreateCommand();
        command.CommandText = "UPDATE Pages SET Site = @Site, Username = @Username, Password = @Password WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", pageModel.Id);
        command.Parameters.AddWithValue("@Site", pageModel.Site);
        command.Parameters.AddWithValue("@Username", pageModel.Username);
        command.Parameters.AddWithValue("@Password", pageModel.Password);
        try
        {
            await command.ExecuteNonQueryAsync();
        }
        catch (SQLiteException e)
        {
            throw e;
        }
    }

    public async Task DeletePageAsync(PageModel pageModel)
    {
        SQLiteCommand command = CreateCommand();
        command.CommandText = "DELETE FROM Pages WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", pageModel.Id);
        try
        {
            await command.ExecuteNonQueryAsync();
        }
        catch (SQLiteException e)
        {
            throw e;
        }
    }

    public async Task<int> GetLastPageIdAsync()
    {
        SQLiteCommand command = CreateCommand();
        command.CommandText = "SELECT Id FROM Pages ORDER BY Id DESC LIMIT 1";
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

    public async Task<int> GetPageIdByAttributes(PageModel pageModel)
    {
        try
        {
            using (SQLiteCommand command = CreateCommand())
            {
                command.CommandText = "SELECT Id FROM Pages WHERE site = @Site AND username = @Username AND password = @Password";

                // Add parameters for the command
                command.Parameters.Add(new SQLiteParameter("@Site", pageModel.Site));
                command.Parameters.Add(new SQLiteParameter("@Username", pageModel.Username));
                command.Parameters.Add(new SQLiteParameter("@Password", pageModel.Password));

                using (DbDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader is SQLiteDataReader sqliteReader)
                    {
                        while (await sqliteReader.ReadAsync())
                        {
                            return sqliteReader.GetInt32(0);
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException("Unexpected database reader type.");
                    }
                }
            }

            // No matching record found, return -1
            return -1;
        }
        catch (Exception ex)
        {
            // Handle exceptions, log, and possibly re-throw or return an error code as needed.
            // Example: Console.WriteLine("Error: " + ex.Message);
            // You can re-throw the exception or return an error code here.
            return -1;
        }
    }

    public async Task<int> AddPageAndReturnId(PageModel pageModel)
    {
        try
        {
            using (SQLiteCommand command = CreateCommand())
            {
                // Insert a new page record
                command.CommandText = "INSERT INTO Pages (site, username, password) VALUES (@Site, @Username, @Password); SELECT last_insert_rowid();";

                // Add parameters for the insert command
                command.Parameters.Add(new SQLiteParameter("@Site", pageModel.Site));
                command.Parameters.Add(new SQLiteParameter("@Username", pageModel.Username));
                command.Parameters.Add(new SQLiteParameter("@Password", pageModel.Password));

                // Execute the insert command and retrieve the newly created ID
                int id = Convert.ToInt32(await command.ExecuteScalarAsync());

                return id;
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions, log, and possibly re-throw or return an error code as needed.
            // Example: Console.WriteLine("Error: " + ex.Message);
            // You can re-throw the exception or return an error code here.
            return -1;
        }
    }

    public async Task<PageModel> GetPageById(int pageId)
    {
        try
        {
            using (SQLiteCommand command = CreateCommand())
            {
                command.CommandText = "SELECT * FROM Pages WHERE Id = @Id";
                command.Parameters.Add(new SQLiteParameter("@Id", pageId));

                using (DbDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader is SQLiteDataReader sqliteReader)
                    {
                        if (await sqliteReader.ReadAsync())
                        {
                            // Create a PageModel and populate it with data from the database
                            PageModel pageModel = new PageModel
                            {
                                Id = sqliteReader.GetInt32(0),
                                Site = sqliteReader.GetString(1),
                                Username = sqliteReader.GetString(2),
                                Password = sqliteReader.GetString(3)
                                // You may need to adapt these field indices based on your table structure
                            };

                            return pageModel;
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException("Unexpected database reader type.");
                    }
                }
            }

            // If no record with the specified ID is found, return null or handle it as needed.
            return null;
        }
        catch (Exception ex)
        {
            // Handle exceptions, log, and possibly re-throw or return an error code as needed.
            // Example: Console.WriteLine("Error: " + ex.Message);
            // You can re-throw the exception or return null or handle it differently here.
            return null;
        }
    }

    #endregion
}
