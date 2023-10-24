using GPTArticleGen.Model;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Diagnostics;

public class SQLiteDB
{
    private SQLiteConnection connection;

    #region Basic Commands
    public SQLiteDB(string connectionString)
    {
        connection = new SQLiteConnection();

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
        connection = new SQLiteConnection("Data Source=WordpressArticlesDatabase.db;Version=3;");
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
        OpenConnection();
        SQLiteCommand command = CreateCommand();
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
            CloseConnection();
        }
    }

    public async Task<DataTable> GetAllArticleAsync()
    {
        OpenConnection();
        SQLiteCommand selectCommand = CreateCommand();
        selectCommand.CommandText = "SELECT * FROM Articles";
        SQLiteDataAdapter adapter = new SQLiteDataAdapter(selectCommand);

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
            CloseConnection();
        }
    }

    public async Task UpdateArticleWithoutImageIdAsync(ArticleModel articleModel)
    {
        OpenConnection();
        SQLiteCommand command = CreateCommand();
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
            CloseConnection();
        }
    }

    public async Task UpdateArticleTitleContentTags(ArticleModel articleModel)
    {
        OpenConnection();
        SQLiteCommand command = CreateCommand();
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
            CloseConnection();
        }
    }

    public async Task DeleteArticleAsync(ArticleModel articleModel)
    {
        OpenConnection();
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
        finally
        {
            CloseConnection();
        }
    }

    public async Task<int> GetLastArticleIdAsync()
    {
        OpenConnection();
        SQLiteCommand command = CreateCommand();
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
            CloseConnection();
        }
    }

    public async Task UpdateArticleImageId(int articleId, int imageId)
    {
        OpenConnection();
        SQLiteCommand command = CreateCommand();
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
            CloseConnection();
        }
    }

    public async Task<BindingList<ArticleDatabaseModel>> GetArticles()
    {
        var articles = new BindingList<ArticleDatabaseModel>();

        OpenConnection();
        SQLiteCommand command = CreateCommand();
        command.CommandText = "SELECT * FROM Articles";

        try
        {
            DbDataReader reader = await command.ExecuteReaderAsync();

            if (reader is SQLiteDataReader sqliteReader)
            {
                while (await sqliteReader.ReadAsync())
                {
                    if (sqliteReader.IsDBNull(5))
                    {
                        continue;
                    }
                    ArticleDatabaseModel article = new ArticleDatabaseModel
                    {
                        Id = sqliteReader.GetInt32(0),
                        Title = sqliteReader.IsDBNull(1) ? null : sqliteReader.GetString(1),
                        Content = sqliteReader.IsDBNull(2) ? null : sqliteReader.GetString(2),
                        Tags = sqliteReader.IsDBNull(3) ? null : sqliteReader.GetString(3),
                        PromptTitle = sqliteReader.IsDBNull(4) ? null : sqliteReader.GetString(4),
                        Prompt = sqliteReader.IsDBNull(5) ? null : sqliteReader.GetString(5),
                        IsPublished = sqliteReader.GetBoolean(6),
                        ImageId = sqliteReader.GetInt32(7),
                        PageId = sqliteReader.GetInt32(8)
                    };
                    articles.Add(article);
                }
            }


            return articles;
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

    public async Task<int> CheckIfArticleIsInDatabase(ArticleModel article)
    {
        OpenConnection();
        SQLiteCommand command = CreateCommand();
        command.CommandText = "SELECT Id FROM Articles Where PromptTitle = @PromptTitle AND Page_id = @Site_id;";
        command.Parameters.AddWithValue("@PromptTitle", article.PromptTitle);
        command.Parameters.AddWithValue("@Site_id", article.SiteId);

        try
        {
            DbDataReader reader = await command.ExecuteReaderAsync();
            if(reader is SQLiteDataReader sqliteReader)
            {
                while (await sqliteReader.ReadAsync())
                {
                    if (!sqliteReader.IsDBNull(0) || sqliteReader.GetInt32(0) > 0)
                    {
                        return sqliteReader.GetInt32(0);
                    }
                    else
                    {
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
            CloseConnection();
        }

        return -1;
    }

    #endregion

    #region CRUD Page
    public async Task InsertPageAsync(PageModel pageModel)
    {
        OpenConnection();
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
        finally
        {
            CloseConnection();
        }
    }

    public async Task<DataTable> GetAllPageAsync()
    {
        OpenConnection();
        SQLiteCommand selectCommand = CreateCommand();
        selectCommand.CommandText = "SELECT * FROM Pages";

        try
        {
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(selectCommand);
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
            CloseConnection();
        }

    }

    public async Task UpdatePageAsync(PageModel pageModel)
    {
        OpenConnection();
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
        finally
        {
            CloseConnection();
        }
    }

    public async Task DeletePageAsync(PageModel pageModel)
    {
        OpenConnection();
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
        finally
        {
            CloseConnection();
        }
    }

    public async Task<int> GetLastPageIdAsync()
    {
        OpenConnection();
        SQLiteCommand command = CreateCommand();
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
            CloseConnection();
        }
    }

    public async Task<int> GetPageIdByAttributes(PageModel pageModel)
    {
        OpenConnection();
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
        finally
        {
            CloseConnection();
        }
    }

    public async Task<int> AddPageAndReturnId(PageModel pageModel)
    {
        OpenConnection();
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
        finally
        {
            CloseConnection();
        }
    }

    public async Task<PageModel> GetPageById(int pageId)
    {
        OpenConnection();
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
        finally
        {
            CloseConnection();
        }
    }

    public async Task<BindingList<PageModel>> GetPages()
    {
        var pages = new BindingList<PageModel>();

        OpenConnection();
        SQLiteCommand command = CreateCommand();
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
                         Id =  sqliteReader.GetInt32(0),
                         Site = sqliteReader.IsDBNull(1) ? null : sqliteReader.GetString(1),
                         Username = sqliteReader.IsDBNull(2) ? null : sqliteReader.GetString(2),
                         Password = sqliteReader.IsDBNull(3) ? null : sqliteReader.GetString(3)
                    };
                    pages.Add(page);
                }
            }


            return pages;
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
}
