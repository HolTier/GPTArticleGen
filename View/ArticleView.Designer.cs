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
            metaTitleNameTextBox = new TextBox();
            metaDescriptionNameTextBox = new TextBox();
            metaTagsNameTextBox = new TextBox();
            metaDesciptionNameLabel = new Label();
            metaTitleNameLabel = new Label();
            metaTagsNameLabel = new Label();
            contentNameTextBox = new TextBox();
            contentNameLabel = new Label();
            titleNameTextBox = new TextBox();
            titleNameLabel = new Label();
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
            maxRetriesNumeric = new NumericUpDown();
            maxRetriesLabel = new Label();
            tabConfiguration = new TabPage();
            cancelConfigurationButton = new Button();
            saveConfigurationButton = new Button();
            metaTitleConfigurationTextBox = new TextBox();
            metaDescriptionConfigurationTextBox = new TextBox();
            tagsConfigurationTextBox = new TextBox();
            metaDescriptionConfigurationLabel = new Label();
            metaTitleConfigurationLabel = new Label();
            tagsConfigurationLabel = new Label();
            contentConfigurationTextBox = new TextBox();
            contentConfigurationLabel = new Label();
            titleConfigurationTextBox = new TextBox();
            titleConfigurationLabel = new Label();
            defaultPromptTextBox = new TextBox();
            defaultLabel = new Label();
            tabDatabase = new TabPage();
            databaseComboBox = new ComboBox();
            databaseGridView = new DataGridView();
            titlesListBox = new ListBox();
            generateForSelectedButton = new Button();
            generateAllButton = new Button();
            importTitlesButton = new Button();
            addToPageButton = new Button();
            regenerateArticleButton = new Button();
            addImagesButton = new Button();
            runGenerationButton = new Button();
            addToPageSelectedButton = new Button();
            addToPageLabel = new Label();
            generateFromChatLabel = new Label();
            panel1 = new Panel();
            metaTitleLabel = new Label();
            metaTitleTextBox = new TextBox();
            metaDescriptionTextBox = new TextBox();
            metaDescriptionLabel = new Label();
            textAndPromptControl.SuspendLayout();
            tabContext.SuspendLayout();
            tabPrompt.SuspendLayout();
            tabWebView2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webView2).BeginInit();
            tabSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)maxRetriesNumeric).BeginInit();
            tabConfiguration.SuspendLayout();
            tabDatabase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)databaseGridView).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // textAndPromptControl
            // 
            textAndPromptControl.Controls.Add(tabContext);
            textAndPromptControl.Controls.Add(tabPrompt);
            textAndPromptControl.Controls.Add(tabWebView2);
            textAndPromptControl.Controls.Add(tabSettings);
            textAndPromptControl.Controls.Add(tabConfiguration);
            textAndPromptControl.Controls.Add(tabDatabase);
            textAndPromptControl.Location = new Point(0, 0);
            textAndPromptControl.Name = "textAndPromptControl";
            textAndPromptControl.RightToLeft = RightToLeft.No;
            textAndPromptControl.SelectedIndex = 0;
            textAndPromptControl.Size = new Size(854, 572);
            textAndPromptControl.TabIndex = 0;
            // 
            // tabContext
            // 
            tabContext.Controls.Add(panel1);
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
            tagsTextBox.Location = new Point(8, 440);
            tagsTextBox.Name = "tagsTextBox";
            tagsTextBox.Size = new Size(806, 27);
            tagsTextBox.TabIndex = 5;
            // 
            // tagsLabel
            // 
            tagsLabel.AutoSize = true;
            tagsLabel.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            tagsLabel.Location = new Point(3, 407);
            tagsLabel.Name = "tagsLabel";
            tagsLabel.Size = new Size(56, 30);
            tagsLabel.TabIndex = 4;
            tagsLabel.Text = "Tags";
            // 
            // contentTextBox
            // 
            contentTextBox.Location = new Point(8, 92);
            contentTextBox.Multiline = true;
            contentTextBox.Name = "contentTextBox";
            contentTextBox.ScrollBars = ScrollBars.Horizontal;
            contentTextBox.Size = new Size(806, 312);
            contentTextBox.TabIndex = 3;
            // 
            // contentLabel
            // 
            contentLabel.AutoSize = true;
            contentLabel.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            contentLabel.Location = new Point(3, 59);
            contentLabel.Name = "contentLabel";
            contentLabel.Size = new Size(90, 30);
            contentLabel.TabIndex = 2;
            contentLabel.Text = "Content";
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            titleLabel.Location = new Point(0, -7);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(54, 30);
            titleLabel.TabIndex = 1;
            titleLabel.Text = "Title";
            // 
            // titleTextBox
            // 
            titleTextBox.Location = new Point(8, 29);
            titleTextBox.Name = "titleTextBox";
            titleTextBox.Size = new Size(806, 27);
            titleTextBox.TabIndex = 0;
            // 
            // tabPrompt
            // 
            tabPrompt.Controls.Add(metaTitleNameTextBox);
            tabPrompt.Controls.Add(metaDescriptionNameTextBox);
            tabPrompt.Controls.Add(metaTagsNameTextBox);
            tabPrompt.Controls.Add(metaDesciptionNameLabel);
            tabPrompt.Controls.Add(metaTitleNameLabel);
            tabPrompt.Controls.Add(metaTagsNameLabel);
            tabPrompt.Controls.Add(contentNameTextBox);
            tabPrompt.Controls.Add(contentNameLabel);
            tabPrompt.Controls.Add(titleNameTextBox);
            tabPrompt.Controls.Add(titleNameLabel);
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
            // metaTitleNameTextBox
            // 
            metaTitleNameTextBox.Location = new Point(8, 251);
            metaTitleNameTextBox.Name = "metaTitleNameTextBox";
            metaTitleNameTextBox.Size = new Size(283, 27);
            metaTitleNameTextBox.TabIndex = 11;
            // 
            // metaDescriptionNameTextBox
            // 
            metaDescriptionNameTextBox.Location = new Point(8, 314);
            metaDescriptionNameTextBox.Name = "metaDescriptionNameTextBox";
            metaDescriptionNameTextBox.Size = new Size(283, 27);
            metaDescriptionNameTextBox.TabIndex = 10;
            // 
            // metaTagsNameTextBox
            // 
            metaTagsNameTextBox.Location = new Point(8, 188);
            metaTagsNameTextBox.Name = "metaTagsNameTextBox";
            metaTagsNameTextBox.Size = new Size(283, 27);
            metaTagsNameTextBox.TabIndex = 9;
            // 
            // metaDesciptionNameLabel
            // 
            metaDesciptionNameLabel.AutoSize = true;
            metaDesciptionNameLabel.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            metaDesciptionNameLabel.Location = new Point(3, 281);
            metaDesciptionNameLabel.Name = "metaDesciptionNameLabel";
            metaDesciptionNameLabel.Size = new Size(176, 30);
            metaDesciptionNameLabel.TabIndex = 8;
            metaDesciptionNameLabel.Text = "Meta description";
            // 
            // metaTitleNameLabel
            // 
            metaTitleNameLabel.AutoSize = true;
            metaTitleNameLabel.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            metaTitleNameLabel.Location = new Point(3, 218);
            metaTitleNameLabel.Name = "metaTitleNameLabel";
            metaTitleNameLabel.Size = new Size(105, 30);
            metaTitleNameLabel.TabIndex = 7;
            metaTitleNameLabel.Text = "Meta title";
            // 
            // metaTagsNameLabel
            // 
            metaTagsNameLabel.AutoSize = true;
            metaTagsNameLabel.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            metaTagsNameLabel.Location = new Point(3, 155);
            metaTagsNameLabel.Name = "metaTagsNameLabel";
            metaTagsNameLabel.Size = new Size(109, 30);
            metaTagsNameLabel.TabIndex = 6;
            metaTagsNameLabel.Text = "Meta tags";
            // 
            // contentNameTextBox
            // 
            contentNameTextBox.Location = new Point(8, 125);
            contentNameTextBox.Name = "contentNameTextBox";
            contentNameTextBox.Size = new Size(283, 27);
            contentNameTextBox.TabIndex = 5;
            // 
            // contentNameLabel
            // 
            contentNameLabel.AutoSize = true;
            contentNameLabel.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            contentNameLabel.Location = new Point(3, 92);
            contentNameLabel.Name = "contentNameLabel";
            contentNameLabel.Size = new Size(90, 30);
            contentNameLabel.TabIndex = 4;
            contentNameLabel.Text = "Content";
            // 
            // titleNameTextBox
            // 
            titleNameTextBox.Location = new Point(8, 62);
            titleNameTextBox.Name = "titleNameTextBox";
            titleNameTextBox.Size = new Size(283, 27);
            titleNameTextBox.TabIndex = 3;
            // 
            // titleNameLabel
            // 
            titleNameLabel.AutoSize = true;
            titleNameLabel.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            titleNameLabel.Location = new Point(3, 34);
            titleNameLabel.Name = "titleNameLabel";
            titleNameLabel.Size = new Size(54, 30);
            titleNameLabel.TabIndex = 2;
            titleNameLabel.Text = "Title";
            // 
            // promptFormatLabel
            // 
            promptFormatLabel.AutoSize = true;
            promptFormatLabel.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            promptFormatLabel.Location = new Point(297, -1);
            promptFormatLabel.Name = "promptFormatLabel";
            promptFormatLabel.Size = new Size(168, 30);
            promptFormatLabel.TabIndex = 1;
            promptFormatLabel.Text = "Prompt preview";
            // 
            // promptFormatTextBox
            // 
            promptFormatTextBox.Location = new Point(297, 34);
            promptFormatTextBox.Multiline = true;
            promptFormatTextBox.Name = "promptFormatTextBox";
            promptFormatTextBox.Size = new Size(546, 497);
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
            // tabConfiguration
            // 
            tabConfiguration.Controls.Add(cancelConfigurationButton);
            tabConfiguration.Controls.Add(saveConfigurationButton);
            tabConfiguration.Controls.Add(metaTitleConfigurationTextBox);
            tabConfiguration.Controls.Add(metaDescriptionConfigurationTextBox);
            tabConfiguration.Controls.Add(tagsConfigurationTextBox);
            tabConfiguration.Controls.Add(metaDescriptionConfigurationLabel);
            tabConfiguration.Controls.Add(metaTitleConfigurationLabel);
            tabConfiguration.Controls.Add(tagsConfigurationLabel);
            tabConfiguration.Controls.Add(contentConfigurationTextBox);
            tabConfiguration.Controls.Add(contentConfigurationLabel);
            tabConfiguration.Controls.Add(titleConfigurationTextBox);
            tabConfiguration.Controls.Add(titleConfigurationLabel);
            tabConfiguration.Controls.Add(defaultPromptTextBox);
            tabConfiguration.Controls.Add(defaultLabel);
            tabConfiguration.Location = new Point(4, 29);
            tabConfiguration.Name = "tabConfiguration";
            tabConfiguration.Padding = new Padding(3);
            tabConfiguration.Size = new Size(846, 539);
            tabConfiguration.TabIndex = 5;
            tabConfiguration.Text = "Configuration";
            tabConfiguration.UseVisualStyleBackColor = true;
            // 
            // cancelConfigurationButton
            // 
            cancelConfigurationButton.BackColor = Color.Transparent;
            cancelConfigurationButton.FlatAppearance.BorderColor = Color.Gray;
            cancelConfigurationButton.FlatAppearance.BorderSize = 0;
            cancelConfigurationButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            cancelConfigurationButton.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            cancelConfigurationButton.Location = new Point(721, 476);
            cancelConfigurationButton.Margin = new Padding(0);
            cancelConfigurationButton.Name = "cancelConfigurationButton";
            cancelConfigurationButton.Size = new Size(119, 57);
            cancelConfigurationButton.TabIndex = 23;
            cancelConfigurationButton.Text = "CANCEL";
            cancelConfigurationButton.UseVisualStyleBackColor = false;
            // 
            // saveConfigurationButton
            // 
            saveConfigurationButton.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            saveConfigurationButton.Location = new Point(599, 476);
            saveConfigurationButton.Name = "saveConfigurationButton";
            saveConfigurationButton.Size = new Size(119, 57);
            saveConfigurationButton.TabIndex = 22;
            saveConfigurationButton.Text = "SAVE";
            saveConfigurationButton.UseVisualStyleBackColor = true;
            // 
            // metaTitleConfigurationTextBox
            // 
            metaTitleConfigurationTextBox.Location = new Point(8, 344);
            metaTitleConfigurationTextBox.Name = "metaTitleConfigurationTextBox";
            metaTitleConfigurationTextBox.Size = new Size(283, 27);
            metaTitleConfigurationTextBox.TabIndex = 21;
            // 
            // metaDescriptionConfigurationTextBox
            // 
            metaDescriptionConfigurationTextBox.Location = new Point(8, 407);
            metaDescriptionConfigurationTextBox.Name = "metaDescriptionConfigurationTextBox";
            metaDescriptionConfigurationTextBox.Size = new Size(283, 27);
            metaDescriptionConfigurationTextBox.TabIndex = 20;
            // 
            // tagsConfigurationTextBox
            // 
            tagsConfigurationTextBox.Location = new Point(8, 281);
            tagsConfigurationTextBox.Name = "tagsConfigurationTextBox";
            tagsConfigurationTextBox.Size = new Size(283, 27);
            tagsConfigurationTextBox.TabIndex = 19;
            // 
            // metaDescriptionConfigurationLabel
            // 
            metaDescriptionConfigurationLabel.AutoSize = true;
            metaDescriptionConfigurationLabel.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            metaDescriptionConfigurationLabel.Location = new Point(3, 374);
            metaDescriptionConfigurationLabel.Name = "metaDescriptionConfigurationLabel";
            metaDescriptionConfigurationLabel.Size = new Size(176, 30);
            metaDescriptionConfigurationLabel.TabIndex = 18;
            metaDescriptionConfigurationLabel.Text = "Meta description";
            // 
            // metaTitleConfigurationLabel
            // 
            metaTitleConfigurationLabel.AutoSize = true;
            metaTitleConfigurationLabel.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            metaTitleConfigurationLabel.Location = new Point(3, 311);
            metaTitleConfigurationLabel.Name = "metaTitleConfigurationLabel";
            metaTitleConfigurationLabel.Size = new Size(105, 30);
            metaTitleConfigurationLabel.TabIndex = 17;
            metaTitleConfigurationLabel.Text = "Meta title";
            // 
            // tagsConfigurationLabel
            // 
            tagsConfigurationLabel.AutoSize = true;
            tagsConfigurationLabel.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            tagsConfigurationLabel.Location = new Point(3, 248);
            tagsConfigurationLabel.Name = "tagsConfigurationLabel";
            tagsConfigurationLabel.Size = new Size(109, 30);
            tagsConfigurationLabel.TabIndex = 16;
            tagsConfigurationLabel.Text = "Meta tags";
            // 
            // contentConfigurationTextBox
            // 
            contentConfigurationTextBox.Location = new Point(8, 218);
            contentConfigurationTextBox.Name = "contentConfigurationTextBox";
            contentConfigurationTextBox.Size = new Size(283, 27);
            contentConfigurationTextBox.TabIndex = 15;
            // 
            // contentConfigurationLabel
            // 
            contentConfigurationLabel.AutoSize = true;
            contentConfigurationLabel.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            contentConfigurationLabel.Location = new Point(3, 185);
            contentConfigurationLabel.Name = "contentConfigurationLabel";
            contentConfigurationLabel.Size = new Size(90, 30);
            contentConfigurationLabel.TabIndex = 14;
            contentConfigurationLabel.Text = "Content";
            // 
            // titleConfigurationTextBox
            // 
            titleConfigurationTextBox.Location = new Point(8, 155);
            titleConfigurationTextBox.Name = "titleConfigurationTextBox";
            titleConfigurationTextBox.Size = new Size(283, 27);
            titleConfigurationTextBox.TabIndex = 13;
            // 
            // titleConfigurationLabel
            // 
            titleConfigurationLabel.AutoSize = true;
            titleConfigurationLabel.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            titleConfigurationLabel.Location = new Point(3, 127);
            titleConfigurationLabel.Name = "titleConfigurationLabel";
            titleConfigurationLabel.Size = new Size(54, 30);
            titleConfigurationLabel.TabIndex = 12;
            titleConfigurationLabel.Text = "Title";
            // 
            // defaultPromptTextBox
            // 
            defaultPromptTextBox.Location = new Point(6, 36);
            defaultPromptTextBox.Multiline = true;
            defaultPromptTextBox.Name = "defaultPromptTextBox";
            defaultPromptTextBox.Size = new Size(834, 84);
            defaultPromptTextBox.TabIndex = 4;
            // 
            // defaultLabel
            // 
            defaultLabel.AutoSize = true;
            defaultLabel.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            defaultLabel.Location = new Point(6, 3);
            defaultLabel.Name = "defaultLabel";
            defaultLabel.Size = new Size(161, 30);
            defaultLabel.TabIndex = 3;
            defaultLabel.Text = "Default prompt";
            // 
            // tabDatabase
            // 
            tabDatabase.Controls.Add(databaseComboBox);
            tabDatabase.Controls.Add(databaseGridView);
            tabDatabase.Location = new Point(4, 29);
            tabDatabase.Name = "tabDatabase";
            tabDatabase.Padding = new Padding(3);
            tabDatabase.Size = new Size(846, 539);
            tabDatabase.TabIndex = 4;
            tabDatabase.Text = "Database";
            tabDatabase.UseVisualStyleBackColor = true;
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
            // generateForSelectedButton
            // 
            generateForSelectedButton.Location = new Point(1032, 54);
            generateForSelectedButton.Name = "generateForSelectedButton";
            generateForSelectedButton.Size = new Size(149, 29);
            generateForSelectedButton.TabIndex = 2;
            generateForSelectedButton.Text = "Generate selected";
            generateForSelectedButton.UseVisualStyleBackColor = true;
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
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.Controls.Add(metaDescriptionTextBox);
            panel1.Controls.Add(metaDescriptionLabel);
            panel1.Controls.Add(metaTitleTextBox);
            panel1.Controls.Add(metaTitleLabel);
            panel1.Controls.Add(titleLabel);
            panel1.Controls.Add(titleTextBox);
            panel1.Controls.Add(tagsTextBox);
            panel1.Controls.Add(contentLabel);
            panel1.Controls.Add(tagsLabel);
            panel1.Controls.Add(contentTextBox);
            panel1.Location = new Point(0, 1);
            panel1.Name = "panel1";
            panel1.Size = new Size(840, 530);
            panel1.TabIndex = 7;
            // 
            // metaTitleLabel
            // 
            metaTitleLabel.AutoSize = true;
            metaTitleLabel.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            metaTitleLabel.Location = new Point(3, 470);
            metaTitleLabel.Name = "metaTitleLabel";
            metaTitleLabel.Size = new Size(110, 30);
            metaTitleLabel.TabIndex = 6;
            metaTitleLabel.Text = "Meta Title";
            // 
            // metaTitleTextBox
            // 
            metaTitleTextBox.Location = new Point(8, 503);
            metaTitleTextBox.Name = "metaTitleTextBox";
            metaTitleTextBox.Size = new Size(806, 27);
            metaTitleTextBox.TabIndex = 7;
            // 
            // metaDescriptionTextBox
            // 
            metaDescriptionTextBox.Location = new Point(8, 569);
            metaDescriptionTextBox.Multiline = true;
            metaDescriptionTextBox.Name = "metaDescriptionTextBox";
            metaDescriptionTextBox.Size = new Size(806, 84);
            metaDescriptionTextBox.TabIndex = 9;
            // 
            // metaDescriptionLabel
            // 
            metaDescriptionLabel.AutoSize = true;
            metaDescriptionLabel.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            metaDescriptionLabel.Location = new Point(3, 536);
            metaDescriptionLabel.Name = "metaDescriptionLabel";
            metaDescriptionLabel.Size = new Size(178, 30);
            metaDescriptionLabel.TabIndex = 8;
            metaDescriptionLabel.Text = "Meta Description";
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
            Controls.Add(generateForSelectedButton);
            Controls.Add(titlesListBox);
            Controls.Add(textAndPromptControl);
            Name = "ArticleView";
            Text = "ArticleForm";
            textAndPromptControl.ResumeLayout(false);
            tabContext.ResumeLayout(false);
            tabPrompt.ResumeLayout(false);
            tabPrompt.PerformLayout();
            tabWebView2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)webView2).EndInit();
            tabSettings.ResumeLayout(false);
            tabSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)maxRetriesNumeric).EndInit();
            tabConfiguration.ResumeLayout(false);
            tabConfiguration.PerformLayout();
            tabDatabase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)databaseGridView).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
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
        private Button generateForSelectedButton;
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
        private TabPage tabSettings;
        private NumericUpDown maxRetriesNumeric;
        private Label maxRetriesLabel;
        private TextBox defaultPromptTextBox;
        private Label defaultLabel;
        private Button cancelSettingsButton;
        private Button saveSettingsButton;
        private Button addImagesButton;
        private Button runGenerationButton;
        private TabPage tabDatabase;
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
        private Label titleNameLabel;
        private TextBox metaTitleNameTextBox;
        private TextBox metaDescriptionNameTextBox;
        private TextBox metaTagsNameTextBox;
        private Label metaDesciptionNameLabel;
        private Label metaTitleNameLabel;
        private Label metaTagsNameLabel;
        private TextBox contentNameTextBox;
        private Label contentNameLabel;
        private TextBox titleNameTextBox;
        private TabPage tabConfiguration;
        private Button cancelConfigurationButton;
        private Button saveConfigurationButton;
        private TextBox metaTitleConfigurationTextBox;
        private TextBox metaDescriptionConfigurationTextBox;
        private TextBox tagsConfigurationTextBox;
        private Label metaDescriptionConfigurationLabel;
        private Label metaTitleConfigurationLabel;
        private Label tagsConfigurationLabel;
        private TextBox contentConfigurationTextBox;
        private Label contentConfigurationLabel;
        private TextBox titleConfigurationTextBox;
        private Label titleConfigurationLabel;
        private Panel panel1;
        private TextBox metaDescriptionTextBox;
        private Label metaDescriptionLabel;
        private TextBox metaTitleTextBox;
        private Label metaTitleLabel;
    }
}