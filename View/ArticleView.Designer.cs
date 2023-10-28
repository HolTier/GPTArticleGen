namespace GPTArticleGen.View
{
    partial class ArticleView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textAndPromptControl = new TabControl();
            tabContext = new TabPage();
            tagsTextBox = new TextBox();
            tagsLabel = new Label();
            contentTextBox = new TextBox();
            contentLabel = new Label();
            titleLabel = new Label();
            titleTextBox = new TextBox();
            tabPrompt = new TabPage();
            promptTextBox = new TextBox();
            promptLabel = new Label();
            promptFormatLabel = new Label();
            promptFormatTextBox = new TextBox();
            tabWebView2 = new TabPage();
            webView2 = new Microsoft.Web.WebView2.WinForms.WebView2();
            tabSettings = new TabPage();
            browseExportFilePathButton = new Button();
            browseImagesPathButton = new Button();
            addTimeCheckBox = new CheckBox();
            createNewFileCheckBox = new CheckBox();
            exportFileNameTextBox = new TextBox();
            exportFileNameLabel = new Label();
            exportFilePathTextBox = new TextBox();
            exportFilePathLabel = new Label();
            imagesPathLabel = new Label();
            imagesPathTextBox = new TextBox();
            cancelSettingsButton = new Button();
            saveSettingsButton = new Button();
            defaultPromptTextBox = new TextBox();
            defaultLabel = new Label();
            maxRetriesNumeric = new NumericUpDown();
            maxRetriesLabel = new Label();
            tabPage1 = new TabPage();
            databaseComboBox = new ComboBox();
            databaseGridView = new DataGridView();
            titlesListBox = new ListBox();
            generateButton = new Button();
            generateAllButton = new Button();
            importTitlesButton = new Button();
            addToPageButton = new Button();
            regenerateArticleButton = new Button();
            addImagesButton = new Button();
            runGenerationButton = new Button();
            addToPageSelectedButton = new Button();
            addToPageLabel = new Label();
            generateFromChatLabel = new Label();
            textAndPromptControl.SuspendLayout();
            tabContext.SuspendLayout();
            tabPrompt.SuspendLayout();
            tabWebView2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webView2).BeginInit();
            tabSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)maxRetriesNumeric).BeginInit();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)databaseGridView).BeginInit();
            SuspendLayout();
            // 
            // textAndPromptControl
            // 
            textAndPromptControl.Controls.Add(tabContext);
            textAndPromptControl.Controls.Add(tabPrompt);
            textAndPromptControl.Controls.Add(tabWebView2);
            textAndPromptControl.Controls.Add(tabSettings);
            textAndPromptControl.Controls.Add(tabPage1);
            textAndPromptControl.Location = new Point(0, 0);
            textAndPromptControl.Name = "textAndPromptControl";
            textAndPromptControl.SelectedIndex = 0;
            textAndPromptControl.Size = new Size(854, 572);
            textAndPromptControl.TabIndex = 0;
            // 
            // tabContext
            // 
            tabContext.Controls.Add(tagsTextBox);
            tabContext.Controls.Add(tagsLabel);
            tabContext.Controls.Add(contentTextBox);
            tabContext.Controls.Add(contentLabel);
            tabContext.Controls.Add(titleLabel);
            tabContext.Controls.Add(titleTextBox);
            tabContext.Location = new Point(4, 29);
            tabContext.Name = "tabContext";
            tabContext.Padding = new Padding(3);
            tabContext.Size = new Size(846, 539);
            tabContext.TabIndex = 1;
            tabContext.Text = "Context";
            tabContext.UseVisualStyleBackColor = true;
            // 
            // tagsTextBox
            // 
            tagsTextBox.Location = new Point(8, 504);
            tagsTextBox.Name = "tagsTextBox";
            tagsTextBox.Size = new Size(832, 27);
            tagsTextBox.TabIndex = 5;
            // 
            // tagsLabel
            // 
            tagsLabel.AutoSize = true;
            tagsLabel.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            tagsLabel.Location = new Point(8, 471);
            tagsLabel.Name = "tagsLabel";
            tagsLabel.Size = new Size(56, 30);
            tagsLabel.TabIndex = 4;
            tagsLabel.Text = "Tags";
            // 
            // contentTextBox
            // 
            contentTextBox.Location = new Point(8, 102);
            contentTextBox.Multiline = true;
            contentTextBox.Name = "contentTextBox";
            contentTextBox.ScrollBars = ScrollBars.Horizontal;
            contentTextBox.Size = new Size(832, 366);
            contentTextBox.TabIndex = 3;
            // 
            // contentLabel
            // 
            contentLabel.AutoSize = true;
            contentLabel.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            contentLabel.Location = new Point(8, 69);
            contentLabel.Name = "contentLabel";
            contentLabel.Size = new Size(90, 30);
            contentLabel.TabIndex = 2;
            contentLabel.Text = "Content";
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            titleLabel.Location = new Point(3, 3);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(54, 30);
            titleLabel.TabIndex = 1;
            titleLabel.Text = "Title";
            // 
            // titleTextBox
            // 
            titleTextBox.Location = new Point(8, 35);
            titleTextBox.Name = "titleTextBox";
            titleTextBox.Size = new Size(832, 27);
            titleTextBox.TabIndex = 0;
            // 
            // tabPrompt
            // 
            tabPrompt.Controls.Add(promptTextBox);
            tabPrompt.Controls.Add(promptLabel);
            tabPrompt.Controls.Add(promptFormatLabel);
            tabPrompt.Controls.Add(promptFormatTextBox);
            tabPrompt.Location = new Point(4, 29);
            tabPrompt.Name = "tabPrompt";
            tabPrompt.Padding = new Padding(3);
            tabPrompt.Size = new Size(846, 539);
            tabPrompt.TabIndex = 0;
            tabPrompt.Text = "Prompt";
            tabPrompt.UseVisualStyleBackColor = true;
            // 
            // promptTextBox
            // 
            promptTextBox.Location = new Point(8, 239);
            promptTextBox.Multiline = true;
            promptTextBox.Name = "promptTextBox";
            promptTextBox.Size = new Size(832, 292);
            promptTextBox.TabIndex = 3;
            // 
            // promptLabel
            // 
            promptLabel.AutoSize = true;
            promptLabel.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            promptLabel.Location = new Point(8, 206);
            promptLabel.Name = "promptLabel";
            promptLabel.Size = new Size(85, 30);
            promptLabel.TabIndex = 2;
            promptLabel.Text = "Prompt";
            // 
            // promptFormatLabel
            // 
            promptFormatLabel.AutoSize = true;
            promptFormatLabel.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            promptFormatLabel.Location = new Point(8, 3);
            promptFormatLabel.Name = "promptFormatLabel";
            promptFormatLabel.Size = new Size(160, 30);
            promptFormatLabel.TabIndex = 1;
            promptFormatLabel.Text = "Prompt Format";
            // 
            // promptFormatTextBox
            // 
            promptFormatTextBox.Location = new Point(8, 34);
            promptFormatTextBox.Multiline = true;
            promptFormatTextBox.Name = "promptFormatTextBox";
            promptFormatTextBox.Size = new Size(835, 169);
            promptFormatTextBox.TabIndex = 0;
            // 
            // tabWebView2
            // 
            tabWebView2.Controls.Add(webView2);
            tabWebView2.Location = new Point(4, 29);
            tabWebView2.Name = "tabWebView2";
            tabWebView2.Padding = new Padding(3);
            tabWebView2.Size = new Size(846, 539);
            tabWebView2.TabIndex = 2;
            tabWebView2.Text = "WebView";
            tabWebView2.UseVisualStyleBackColor = true;
            // 
            // webView2
            // 
            webView2.AllowExternalDrop = true;
            webView2.CreationProperties = null;
            webView2.DefaultBackgroundColor = Color.White;
            webView2.Location = new Point(0, 0);
            webView2.Name = "webView2";
            webView2.Size = new Size(846, 543);
            webView2.TabIndex = 0;
            webView2.ZoomFactor = 1D;
            // 
            // tabSettings
            // 
            tabSettings.Controls.Add(browseExportFilePathButton);
            tabSettings.Controls.Add(browseImagesPathButton);
            tabSettings.Controls.Add(addTimeCheckBox);
            tabSettings.Controls.Add(createNewFileCheckBox);
            tabSettings.Controls.Add(exportFileNameTextBox);
            tabSettings.Controls.Add(exportFileNameLabel);
            tabSettings.Controls.Add(exportFilePathTextBox);
            tabSettings.Controls.Add(exportFilePathLabel);
            tabSettings.Controls.Add(imagesPathLabel);
            tabSettings.Controls.Add(imagesPathTextBox);
            tabSettings.Controls.Add(cancelSettingsButton);
            tabSettings.Controls.Add(saveSettingsButton);
            tabSettings.Controls.Add(defaultPromptTextBox);
            tabSettings.Controls.Add(defaultLabel);
            tabSettings.Controls.Add(maxRetriesNumeric);
            tabSettings.Controls.Add(maxRetriesLabel);
            tabSettings.Location = new Point(4, 29);
            tabSettings.Name = "tabSettings";
            tabSettings.Padding = new Padding(3);
            tabSettings.Size = new Size(846, 539);
            tabSettings.TabIndex = 3;
            tabSettings.Text = "Settings";
            tabSettings.UseVisualStyleBackColor = true;
            // 
            // browseExportFilePathButton
            // 
            browseExportFilePathButton.Location = new Point(772, 74);
            browseExportFilePathButton.Name = "browseExportFilePathButton";
            browseExportFilePathButton.Size = new Size(68, 27);
            browseExportFilePathButton.TabIndex = 16;
            browseExportFilePathButton.Text = "Browse";
            browseExportFilePathButton.UseVisualStyleBackColor = true;
            // 
            // browseImagesPathButton
            // 
            browseImagesPathButton.Location = new Point(772, 39);
            browseImagesPathButton.Name = "browseImagesPathButton";
            browseImagesPathButton.Size = new Size(68, 27);
            browseImagesPathButton.TabIndex = 15;
            browseImagesPathButton.Text = "Browse";
            browseImagesPathButton.UseVisualStyleBackColor = true;
            // 
            // addTimeCheckBox
            // 
            addTimeCheckBox.AutoSize = true;
            addTimeCheckBox.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            addTimeCheckBox.Location = new Point(195, 148);
            addTimeCheckBox.Name = "addTimeCheckBox";
            addTimeCheckBox.Size = new Size(130, 34);
            addTimeCheckBox.TabIndex = 14;
            addTimeCheckBox.Text = "Add time ";
            addTimeCheckBox.UseVisualStyleBackColor = true;
            // 
            // createNewFileCheckBox
            // 
            createNewFileCheckBox.AutoSize = true;
            createNewFileCheckBox.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            createNewFileCheckBox.Location = new Point(9, 148);
            createNewFileCheckBox.Name = "createNewFileCheckBox";
            createNewFileCheckBox.Size = new Size(180, 34);
            createNewFileCheckBox.TabIndex = 13;
            createNewFileCheckBox.Text = "Create new file";
            createNewFileCheckBox.UseVisualStyleBackColor = true;
            // 
            // exportFileNameTextBox
            // 
            exportFileNameTextBox.Location = new Point(176, 107);
            exportFileNameTextBox.Name = "exportFileNameTextBox";
            exportFileNameTextBox.Size = new Size(596, 27);
            exportFileNameTextBox.TabIndex = 12;
            // 
            // exportFileNameLabel
            // 
            exportFileNameLabel.AutoSize = true;
            exportFileNameLabel.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            exportFileNameLabel.Location = new Point(0, 107);
            exportFileNameLabel.Name = "exportFileNameLabel";
            exportFileNameLabel.Size = new Size(170, 30);
            exportFileNameLabel.TabIndex = 11;
            exportFileNameLabel.Text = "Export file name";
            // 
            // exportFilePathTextBox
            // 
            exportFilePathTextBox.Location = new Point(160, 74);
            exportFilePathTextBox.Name = "exportFilePathTextBox";
            exportFilePathTextBox.Size = new Size(612, 27);
            exportFilePathTextBox.TabIndex = 10;
            // 
            // exportFilePathLabel
            // 
            exportFilePathLabel.AutoSize = true;
            exportFilePathLabel.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            exportFilePathLabel.Location = new Point(0, 74);
            exportFilePathLabel.Name = "exportFilePathLabel";
            exportFilePathLabel.Size = new Size(159, 30);
            exportFilePathLabel.TabIndex = 9;
            exportFilePathLabel.Text = "Export file path";
            // 
            // imagesPathLabel
            // 
            imagesPathLabel.AutoSize = true;
            imagesPathLabel.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            imagesPathLabel.Location = new Point(0, 36);
            imagesPathLabel.Name = "imagesPathLabel";
            imagesPathLabel.Size = new Size(132, 30);
            imagesPathLabel.TabIndex = 8;
            imagesPathLabel.Text = "Images path";
            // 
            // imagesPathTextBox
            // 
            imagesPathTextBox.Location = new Point(130, 39);
            imagesPathTextBox.Name = "imagesPathTextBox";
            imagesPathTextBox.Size = new Size(642, 27);
            imagesPathTextBox.TabIndex = 7;
            // 
            // cancelSettingsButton
            // 
            cancelSettingsButton.BackColor = Color.Transparent;
            cancelSettingsButton.FlatAppearance.BorderColor = Color.Gray;
            cancelSettingsButton.FlatAppearance.BorderSize = 0;
            cancelSettingsButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            cancelSettingsButton.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            cancelSettingsButton.Location = new Point(721, 474);
            cancelSettingsButton.Margin = new Padding(0);
            cancelSettingsButton.Name = "cancelSettingsButton";
            cancelSettingsButton.Size = new Size(119, 57);
            cancelSettingsButton.TabIndex = 6;
            cancelSettingsButton.Text = "CANCEL";
            cancelSettingsButton.UseVisualStyleBackColor = false;
            // 
            // saveSettingsButton
            // 
            saveSettingsButton.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            saveSettingsButton.Location = new Point(599, 474);
            saveSettingsButton.Name = "saveSettingsButton";
            saveSettingsButton.Size = new Size(119, 57);
            saveSettingsButton.TabIndex = 5;
            saveSettingsButton.Text = "SAVE";
            saveSettingsButton.UseVisualStyleBackColor = true;
            // 
            // defaultPromptTextBox
            // 
            defaultPromptTextBox.Location = new Point(9, 218);
            defaultPromptTextBox.Multiline = true;
            defaultPromptTextBox.Name = "defaultPromptTextBox";
            defaultPromptTextBox.Size = new Size(834, 84);
            defaultPromptTextBox.TabIndex = 4;
            // 
            // defaultLabel
            // 
            defaultLabel.AutoSize = true;
            defaultLabel.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            defaultLabel.Location = new Point(3, 185);
            defaultLabel.Name = "defaultLabel";
            defaultLabel.Size = new Size(161, 30);
            defaultLabel.TabIndex = 3;
            defaultLabel.Text = "Default prompt";
            // 
            // maxRetriesNumeric
            // 
            maxRetriesNumeric.Location = new Point(130, 6);
            maxRetriesNumeric.Name = "maxRetriesNumeric";
            maxRetriesNumeric.Size = new Size(150, 27);
            maxRetriesNumeric.TabIndex = 2;
            // 
            // maxRetriesLabel
            // 
            maxRetriesLabel.AutoSize = true;
            maxRetriesLabel.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            maxRetriesLabel.Location = new Point(0, -1);
            maxRetriesLabel.Name = "maxRetriesLabel";
            maxRetriesLabel.Size = new Size(121, 30);
            maxRetriesLabel.TabIndex = 0;
            maxRetriesLabel.Text = "Max retries";
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(databaseComboBox);
            tabPage1.Controls.Add(databaseGridView);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(846, 539);
            tabPage1.TabIndex = 4;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // databaseComboBox
            // 
            databaseComboBox.FormattingEnabled = true;
            databaseComboBox.Items.AddRange(new object[] { "Articles", "Pages" });
            databaseComboBox.Location = new Point(3, 3);
            databaseComboBox.Name = "databaseComboBox";
            databaseComboBox.Size = new Size(151, 28);
            databaseComboBox.TabIndex = 1;
            // 
            // databaseGridView
            // 
            databaseGridView.AllowUserToAddRows = false;
            databaseGridView.AllowUserToDeleteRows = false;
            databaseGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            databaseGridView.Location = new Point(3, 34);
            databaseGridView.Name = "databaseGridView";
            databaseGridView.ReadOnly = true;
            databaseGridView.RowHeadersWidth = 51;
            databaseGridView.RowTemplate.Height = 29;
            databaseGridView.Size = new Size(840, 502);
            databaseGridView.TabIndex = 0;
            // 
            // titlesListBox
            // 
            titlesListBox.FormattingEnabled = true;
            titlesListBox.ItemHeight = 20;
            titlesListBox.Location = new Point(853, 28);
            titlesListBox.Name = "titlesListBox";
            titlesListBox.Size = new Size(166, 544);
            titlesListBox.TabIndex = 1;
            // 
            // generateButton
            // 
            generateButton.Location = new Point(1032, 54);
            generateButton.Name = "generateButton";
            generateButton.Size = new Size(149, 29);
            generateButton.TabIndex = 2;
            generateButton.Text = "Generate selected";
            generateButton.UseVisualStyleBackColor = true;
            // 
            // generateAllButton
            // 
            generateAllButton.Location = new Point(1032, 89);
            generateAllButton.Name = "generateAllButton";
            generateAllButton.Size = new Size(149, 29);
            generateAllButton.TabIndex = 3;
            generateAllButton.Text = "Generate all";
            generateAllButton.UseVisualStyleBackColor = true;
            // 
            // importTitlesButton
            // 
            importTitlesButton.Location = new Point(1032, 398);
            importTitlesButton.Name = "importTitlesButton";
            importTitlesButton.Size = new Size(149, 29);
            importTitlesButton.TabIndex = 5;
            importTitlesButton.Text = "Import Titles";
            importTitlesButton.UseVisualStyleBackColor = true;
            // 
            // addToPageButton
            // 
            addToPageButton.Location = new Point(1032, 182);
            addToPageButton.Name = "addToPageButton";
            addToPageButton.Size = new Size(149, 29);
            addToPageButton.TabIndex = 6;
            addToPageButton.Text = "Add all";
            addToPageButton.UseVisualStyleBackColor = true;
            // 
            // regenerateArticleButton
            // 
            regenerateArticleButton.Location = new Point(1032, 468);
            regenerateArticleButton.Name = "regenerateArticleButton";
            regenerateArticleButton.Size = new Size(149, 29);
            regenerateArticleButton.TabIndex = 7;
            regenerateArticleButton.Text = "Regenerate";
            regenerateArticleButton.UseVisualStyleBackColor = true;
            // 
            // addImagesButton
            // 
            addImagesButton.Location = new Point(1032, 433);
            addImagesButton.Name = "addImagesButton";
            addImagesButton.Size = new Size(149, 29);
            addImagesButton.TabIndex = 8;
            addImagesButton.Text = "Add Images";
            addImagesButton.UseVisualStyleBackColor = true;
            // 
            // runGenerationButton
            // 
            runGenerationButton.Location = new Point(1032, 503);
            runGenerationButton.Name = "runGenerationButton";
            runGenerationButton.Size = new Size(149, 59);
            runGenerationButton.TabIndex = 10;
            runGenerationButton.Text = "Run Generation";
            runGenerationButton.UseVisualStyleBackColor = true;
            // 
            // addToPageSelectedButton
            // 
            addToPageSelectedButton.Location = new Point(1032, 147);
            addToPageSelectedButton.Name = "addToPageSelectedButton";
            addToPageSelectedButton.Size = new Size(149, 29);
            addToPageSelectedButton.TabIndex = 11;
            addToPageSelectedButton.Text = "Add selected";
            addToPageSelectedButton.UseVisualStyleBackColor = true;
            // 
            // addToPageLabel
            // 
            addToPageLabel.AutoSize = true;
            addToPageLabel.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            addToPageLabel.Location = new Point(1037, 121);
            addToPageLabel.Name = "addToPageLabel";
            addToPageLabel.Size = new Size(144, 23);
            addToPageLabel.TabIndex = 12;
            addToPageLabel.Text = "Add to wordpress";
            // 
            // generateFromChatLabel
            // 
            generateFromChatLabel.AutoSize = true;
            generateFromChatLabel.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            generateFromChatLabel.Location = new Point(1032, 28);
            generateFromChatLabel.Name = "generateFromChatLabel";
            generateFromChatLabel.Size = new Size(159, 23);
            generateFromChatLabel.TabIndex = 13;
            generateFromChatLabel.Text = "Generate from chat";
            // 
            // ArticleView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1193, 572);
            Controls.Add(generateFromChatLabel);
            Controls.Add(addToPageLabel);
            Controls.Add(addToPageSelectedButton);
            Controls.Add(runGenerationButton);
            Controls.Add(addImagesButton);
            Controls.Add(regenerateArticleButton);
            Controls.Add(addToPageButton);
            Controls.Add(importTitlesButton);
            Controls.Add(generateAllButton);
            Controls.Add(generateButton);
            Controls.Add(titlesListBox);
            Controls.Add(textAndPromptControl);
            Name = "ArticleView";
            Text = "ArticleForm";
            textAndPromptControl.ResumeLayout(false);
            tabContext.ResumeLayout(false);
            tabContext.PerformLayout();
            tabPrompt.ResumeLayout(false);
            tabPrompt.PerformLayout();
            tabWebView2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)webView2).EndInit();
            tabSettings.ResumeLayout(false);
            tabSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)maxRetriesNumeric).EndInit();
            tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)databaseGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TabControl textAndPromptControl;
        private TabPage tabPrompt;
        private TextBox promptFormatTextBox;
        private TabPage tabContext;
        private TextBox titleTextBox;
        private ListBox titlesListBox;
        private Button generateButton;
        private Button generateAllButton;
        private Button importTitlesButton;
        private Button addToPageButton;
        private TabPage tabWebView2;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView2;
        private Button regenerateArticleButton;
        private Label titleLabel;
        private TextBox contentTextBox;
        private Label contentLabel;
        private Label tagsLabel;
        private TextBox tagsTextBox;
        private Label promptFormatLabel;
        private Label promptLabel;
        private TextBox promptTextBox;
        private TabPage tabSettings;
        private NumericUpDown maxRetriesNumeric;
        private Label maxRetriesLabel;
        private TextBox defaultPromptTextBox;
        private Label defaultLabel;
        private Button cancelSettingsButton;
        private Button saveSettingsButton;
        private Button addImagesButton;
        private Button runGenerationButton;
        private TabPage tabPage1;
        private DataGridView databaseGridView;
        private ComboBox databaseComboBox;
        private Button addToPageSelectedButton;
        private Label addToPageLabel;
        private Label generateFromChatLabel;
        private Label imagesPathLabel;
        private TextBox imagesPathTextBox;
        private TextBox exportFileNameTextBox;
        private Label exportFileNameLabel;
        private TextBox exportFilePathTextBox;
        private Label exportFilePathLabel;
        private CheckBox addTimeCheckBox;
        private CheckBox createNewFileCheckBox;
        private Button browseImagesPathButton;
        private Button button3;
        private Button browseExportFilePathButton;
    }
}