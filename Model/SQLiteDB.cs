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

    #region CRUD Article
    public async Task InsertArticleAsync(ArticleModel articleModel)
    {
        DbMutex.WaitOne();
        try
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO Articles (Title, Content, Tags, PromptTitle ,Prompt, isPublished, Image_id, Page_id) VALUES (@Title, @Content, @Tags, @PromptTitle ,@Prompt, @IsPublished, @Image_id, @Page_id)";
                    command.Parameters.AddWithValue("@Title", articleModel.Title);
                    command.Parameters.AddWithValue("@Content", articleModel.Content);
                    command.Parameters.AddWithValue("@Tags", articleModel.Tags);
                    command.Parameters.AddWithValue("@PromptTitle", articleModel.PromptTitle);
                    command.Parameters.AddWithValue("@Prompt", articleModel.Prompt);
                    command.Parameters.AddWithValue("@IsPublished", articleModel.IsPublished);
                    command.Parameters.AddWithValue("@Page_id", articleModel.SiteId);
                    command.Parameters.AddWithValue("@Image_id", articleModel.ImageId);
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
        finally
        {
            DbMutex.ReleaseMutex();
        }
    }

    public async Task<DataTable> GetAllArticleAsync()
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Articles";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);

                try
                {
                    DataTable dataTable = new DataTable();
                    await Task.Run(() => adapter.Fill(dataTable)); // Run the Fill operation asynchronously
                    return dataTable;
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

    public async Task UpdateArticleWithoutImageIdAsync(ArticleModel articleModel)
    {
        
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE Articles SET Title = @Title, Content = @Content, Tags = @Tags, PromptTitle = @PromptTitle ,Prompt = @Prompt, IsPublished = @IsPublished, Page_id = @Page_id WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", articleModel.Id);
                    command.Parameters.AddWithValue("@Title", articleModel.Title);
                    command.Parameters.AddWithValue("@Content", articleModel.Content);
                    command.Parameters.AddWithValue("@Tags", articleModel.Tags);
                    command.Parameters.AddWithValue("@PromptTitle", articleModel.PromptTitle);
                    command.Parameters.AddWithValue("@Prompt", articleModel.Prompt);
                    command.Parameters.AddWithValue("@IsPublished", articleModel.IsPublished);
                    //command.Parameters.AddWithValue("@Image_id", articleModel.ImageId);
                    command.Parameters.AddWithValue("@Page_id", articleModel.SiteId);

                    Debug.WriteLine("UpdateArticleAsync: " + command.CommandText);

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

    public async Task UpdateArticleTitleContentTags(ArticleModel articleModel)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE Articles SET Title = @Title, Content = @Content, Tags = @Tags WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", articleModel.Id);
                command.Parameters.AddWithValue("@Title", articleModel.Title);
                command.Parameters.AddWithValue("@Content", articleModel.Content);
                command.Parameters.AddWithValue("@Tags", articleModel.Tags);

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

    public async Task DeleteArticleAsync(ArticleModel articleModel)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (SQLiteCommand command = connection.CreateCommand())
            {
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
                finally
                {
                    connection.Close();
                }
            }
        }
    }

    public async Task<int> GetLastArticleIdAsync()
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT Id FROM Articles ORDER BY Id DESC LIMIT 1";

                try
                {
                    DbDataReader reader = await command.ExecuteReaderAsync();
                    int id = 0;

                    if (reader is SQLiteDataReader sqliteReader)
                    {
                        while (await sqliteReader.ReadAsync())
                        {
                            id = sqliteReader.GetInt32(0);
                        }

                        sqliteReader.Close();
                    }
                    else
                    {
                        throw new InvalidOperationException("Unexpected database reader type.");
                    }

                    await reader.DisposeAsync();
                    return id;
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

    public async Task UpdateArticleImageId(int articleId, int imageId)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE Articles SET image_id = @Image_id WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", articleId);
                command.Parameters.AddWithValue("@Image_id", imageId);
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

    public async Task<BindingList<ArticleDatabaseModel>> GetArticles()
    {
        var articles = new BindingList<ArticleDatabaseModel>();

        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open(); // Open the SQLite connection

            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Articles";

                try
                {
                    SQLiteDataReader reader = (SQLiteDataReader)await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        ArticleDatabaseModel article = new ArticleDatabaseModel
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.IsDBNull(1) ? null : reader.GetString(1),
                            Content = reader.IsDBNull(2) ? null : reader.GetString(2),
                            Tags = reader.IsDBNull(3) ? null : reader.GetString(3),
                            PromptTitle = reader.IsDBNull(4) ? null : reader.GetString(4),
                            Prompt = reader.IsDBNull(5) ? null : reader.GetString(5),
                            IsPublished = reader.GetBoolean(6),
                            ImageId = reader.GetInt32(7),
                            PageId = reader.GetInt32(8)
                        };

                        articles.Add(article);
                    }

                    await reader.DisposeAsync();
                    return articles;
                }
                catch (SQLiteException e)
                {
                    throw e;
                }
                finally
                {
                    connection.Close(); // Close the SQLite connection in the finally block
                }
            }
        }
    }

    public async Task<List<ArticleModel>> GetArticlesAsArticleModelAsync()
    {
        var articles = new List<ArticleModel>();

        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open(); // Open the SQLite connection

            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Articles";

                try
                {
                    SQLiteDataReader reader = (SQLiteDataReader)await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        ArticleModel article = new ArticleModel
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.IsDBNull(1) ? null : reader.GetString(1),
                            Content = reader.IsDBNull(2) ? null : reader.GetString(2),
                            Tags = reader.IsDBNull(3) ? null : reader.GetString(3),
                            PromptTitle = reader.IsDBNull(4) ? null : reader.GetString(4),
                            Prompt = reader.IsDBNull(5) ? null : reader.GetString(5),
                            IsPublished = reader.GetBoolean(6),
                            ImageId = reader.GetInt32(7),
                            PageId = reader.GetInt32(8)
                        };

                        articles.Add(article);
                    }

                    await reader.DisposeAsync();
                    return articles;
                }
                catch (SQLiteException e)
                {
                    throw e;
                }
                finally
                {
                    connection.Close(); // Close the SQLite connection in the finally block
                }
            }
        }
    }

    public async Task<int> CheckIfArticleIsInDatabase(ArticleModel article)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT Id FROM Articles Where PromptTitle = @PromptTitle AND Page_id = @Site_id;";
                command.Parameters.AddWithValue("@PromptTitle", article.PromptTitle);
                command.Parameters.AddWithValue("@Site_id", article.SiteId);

                try
                {
                    DbDataReader reader = await command.ExecuteReaderAsync();
                    if (reader is SQLiteDataReader sqliteReader)
                    {
                        while (await sqliteReader.ReadAsync())
                        {
                            if (!sqliteReader.IsDBNull(0) || sqliteReader.GetInt32(0) > 0)
                            {
                                try
                                {
                                    return sqliteReader.GetInt32(0);
                                }
                                finally
                                {
                                    sqliteReader.Close();
                                    await reader.DisposeAsync();
                                }
                            }
                            else
                            {
                                sqliteReader.Close();
                                await reader.DisposeAsync();
                                return -1;
                            }
                        }
                    }
                }
                catch (SQLiteException e)
                {
                    throw e;
                }
                finally
                {
                    connection.Close();
                }

                return -1;
            }
        }
    }

    #endregion

    #region CRUD Page
    public async Task InsertPageAsync(PageModel pageModel)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (SQLiteCommand command = connection.CreateCommand())
            {
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
                finally
                {
                    connection.Close();
                }
            }
        }
    }

    public async Task<DataTable> GetAllPageAsync()
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Pages";

                try
                {
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    await Task.Run(() => adapter.Fill(dataTable)); // Run the Fill operation asynchronously
                    return dataTable;
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

    public async Task UpdatePageAsync(PageModel pageModel)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (SQLiteCommand command = connection.CreateCommand())
            {
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
                finally
                {
                    connection.Close();
                }
            }
        }
    }

    public async Task DeletePageAsync(PageModel pageModel)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (SQLiteCommand command = connection.CreateCommand())
            {
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
                finally
                {
                    connection.Close();
                }
            }
        }
    }

    public async Task<int> GetLastPageIdAsync()
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT Id FROM Pages ORDER BY Id DESC LIMIT 1";

                try
                {
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

    public async Task<int> GetPageIdByAttributes(PageModel pageModel)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
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
                                try
                                {
                                    return sqliteReader.GetInt32(0);
                                }
                                finally
                                {
                                    sqliteReader.Close();
                                    await reader.DisposeAsync();
                                }
                            }

                            sqliteReader.Close();
                        }
                        else
                        {
                            throw new InvalidOperationException("Unexpected database reader type.");
                        }

                        await reader.DisposeAsync();
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
            finally
            {
                connection.Close();
            }
        }
    }

    public async Task<int> AddPageAndReturnId(PageModel pageModel)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
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
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
    }

    public async Task<PageModel> GetPageByIdAsync(int pageId)
    {
        SQLiteCommand command = CreateCommand();    
        command.CommandText = "SELECT * FROM Pages WHERE Id = @Id";
        command.Parameters.Add(new SQLiteParameter("@Id", pageId));
        SQLiteDataReader reader = (SQLiteDataReader)await command.ExecuteReaderAsync();
        PageModel pageModel = new PageModel
        {
            Id = reader.GetInt32(0),
            Site = reader.GetString(1),
            Username = reader.GetString(2),
            Password = reader.GetString(3)
            // You may need to adapt these field indices based on your table structure
        };

        return pageModel;
    }

    public async Task<PageModel> GetPageById(int pageId)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            //connection.Open();
            try
            {
                using (SQLiteCommand command = CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Pages WHERE Id = @Id";
                    command.Parameters.Add(new SQLiteParameter("@Id", pageId));
                    SQLiteDataReader reader = (SQLiteDataReader)await command.ExecuteReaderAsync();
                    
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

                                sqliteReader.Close();
                                await reader.DisposeAsync();
                                return pageModel;
                            }
                        }
                        else
                        {
                            throw new InvalidOperationException("Unexpected database reader type.");
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
            finally
            {
                connection.Close();
            }
        }
    }

    public async Task<BindingList<PageModel>> GetPages()
    {
        var pages = new BindingList<PageModel>();

        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Pages";

                try
                {
                    DbDataReader reader = await command.ExecuteReaderAsync();

                    if (reader is SQLiteDataReader sqliteReader)
                    {
                        while (await sqliteReader.ReadAsync())
                        {
                            PageModel page = new PageModel
                            {
                                Id = sqliteReader.GetInt32(0),
                                Site = sqliteReader.IsDBNull(1) ? null : sqliteReader.GetString(1),
                                Username = sqliteReader.IsDBNull(2) ? null : sqliteReader.GetString(2),
                                Password = sqliteReader.IsDBNull(3) ? null : sqliteReader.GetString(3)
                            };
                            pages.Add(page);
                        }
                        sqliteReader.Close();
                    }

                    await reader.DisposeAsync();
                    return pages;
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
