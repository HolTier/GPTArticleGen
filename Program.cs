using GPTArticleGen.Model;
using GPTArticleGen.Presenter;
using GPTArticleGen.View;

namespace GPTArticleGen
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            // ApplicationConfiguration.Initialize();
            // Application.Run(new ArticleView());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Create an instance of the Model
            var model = new ArticleModel();

            // Create an instance of the View (Form)
            var view = new ArticleView();

            // Create an instance of the Presenter, passing the View and Model
            var presenter = new ArticlePresenter(view, model);

            // Initialize the Presenter to set the View to the initial state
            presenter.Initialize();

            // Star the Application Loop with the main Form
            Application.Run(view);
        }
    }
}