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
            tabContent = new TabPage();
            content = new TextBox();
            tabPrompt = new TabPage();
            tagsTextBox = new TextBox();
            tagsLabel = new Label();
            contentTextBox = new TextBox();
            contentLabel = new Label();
            titleLabel = new Label();
            titleTextBox = new TextBox();
            tabPage1 = new TabPage();
            webView2 = new Microsoft.Web.WebView2.WinForms.WebView2();
            titlesListBox = new ListBox();
            generateButton = new Button();
            generateAllButton = new Button();
            changePromptButton = new Button();
            importTitlesButton = new Button();
            addToPageButton = new Button();
            regenerateArticleButton = new Button();
            textAndPromptControl.SuspendLayout();
            tabContent.SuspendLayout();
            tabPrompt.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webView2).BeginInit();
            SuspendLayout();
            // 
            // textAndPromptControl
            // 
            textAndPromptControl.Controls.Add(tabContent);
            textAndPromptControl.Controls.Add(tabPrompt);
            textAndPromptControl.Controls.Add(tabPage1);
            textAndPromptControl.Location = new Point(0, 0);
            textAndPromptControl.Name = "textAndPromptControl";
            textAndPromptControl.SelectedIndex = 0;
            textAndPromptControl.Size = new Size(812, 572);
            textAndPromptControl.TabIndex = 0;
            // 
            // tabContent
            // 
            tabContent.Controls.Add(content);
            tabContent.Location = new Point(4, 29);
            tabContent.Name = "tabContent";
            tabContent.Padding = new Padding(3);
            tabContent.Size = new Size(804, 539);
            tabContent.TabIndex = 0;
            tabContent.Text = "Content";
            tabContent.UseVisualStyleBackColor = true;
            // 
            // content
            // 
            content.Location = new Point(0, 0);
            content.Multiline = true;
            content.Name = "content";
            content.Size = new Size(808, 540);
            content.TabIndex = 0;
            // 
            // tabPrompt
            // 
            tabPrompt.Controls.Add(tagsTextBox);
            tabPrompt.Controls.Add(tagsLabel);
            tabPrompt.Controls.Add(contentTextBox);
            tabPrompt.Controls.Add(contentLabel);
            tabPrompt.Controls.Add(titleLabel);
            tabPrompt.Controls.Add(titleTextBox);
            tabPrompt.Location = new Point(4, 29);
            tabPrompt.Name = "tabPrompt";
            tabPrompt.Padding = new Padding(3);
            tabPrompt.Size = new Size(804, 539);
            tabPrompt.TabIndex = 1;
            tabPrompt.Text = "Prompt";
            tabPrompt.UseVisualStyleBackColor = true;
            // 
            // tagsTextBox
            // 
            tagsTextBox.Location = new Point(8, 504);
            tagsTextBox.Name = "tagsTextBox";
            tagsTextBox.Size = new Size(789, 27);
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
            contentTextBox.Size = new Size(790, 366);
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
            titleTextBox.Size = new Size(790, 27);
            titleTextBox.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(webView2);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(804, 539);
            tabPage1.TabIndex = 2;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // webView2
            // 
            webView2.AllowExternalDrop = true;
            webView2.CreationProperties = null;
            webView2.DefaultBackgroundColor = Color.White;
            webView2.Location = new Point(0, 0);
            webView2.Name = "webView2";
            webView2.Size = new Size(808, 543);
            webView2.TabIndex = 0;
            webView2.ZoomFactor = 1D;
            // 
            // titlesListBox
            // 
            titlesListBox.FormattingEnabled = true;
            titlesListBox.ItemHeight = 20;
            titlesListBox.Location = new Point(818, 28);
            titlesListBox.Name = "titlesListBox";
            titlesListBox.Size = new Size(208, 544);
            titlesListBox.TabIndex = 1;
            // 
            // generateButton
            // 
            generateButton.Location = new Point(1032, 28);
            generateButton.Name = "generateButton";
            generateButton.Size = new Size(149, 29);
            generateButton.TabIndex = 2;
            generateButton.Text = "Generate";
            generateButton.UseVisualStyleBackColor = true;
            // 
            // generateAllButton
            // 
            generateAllButton.Location = new Point(1032, 63);
            generateAllButton.Name = "generateAllButton";
            generateAllButton.Size = new Size(149, 29);
            generateAllButton.TabIndex = 3;
            generateAllButton.Text = "Generate for all";
            generateAllButton.UseVisualStyleBackColor = true;
            // 
            // changePromptButton
            // 
            changePromptButton.Location = new Point(1032, 98);
            changePromptButton.Name = "changePromptButton";
            changePromptButton.Size = new Size(149, 29);
            changePromptButton.TabIndex = 4;
            changePromptButton.Text = "Change Prompt";
            changePromptButton.UseVisualStyleBackColor = true;
            // 
            // importTitlesButton
            // 
            importTitlesButton.Location = new Point(1032, 133);
            importTitlesButton.Name = "importTitlesButton";
            importTitlesButton.Size = new Size(149, 29);
            importTitlesButton.TabIndex = 5;
            importTitlesButton.Text = "Import Titles";
            importTitlesButton.UseVisualStyleBackColor = true;
            // 
            // addToPageButton
            // 
            addToPageButton.Location = new Point(1032, 168);
            addToPageButton.Name = "addToPageButton";
            addToPageButton.Size = new Size(149, 29);
            addToPageButton.TabIndex = 6;
            addToPageButton.Text = "Add to page";
            addToPageButton.UseVisualStyleBackColor = true;
            // 
            // regenerateArticleButton
            // 
            regenerateArticleButton.Location = new Point(1032, 203);
            regenerateArticleButton.Name = "regenerateArticleButton";
            regenerateArticleButton.Size = new Size(149, 29);
            regenerateArticleButton.TabIndex = 7;
            regenerateArticleButton.Text = "Regenerate";
            regenerateArticleButton.UseVisualStyleBackColor = true;
            // 
            // ArticleView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1193, 572);
            Controls.Add(regenerateArticleButton);
            Controls.Add(addToPageButton);
            Controls.Add(importTitlesButton);
            Controls.Add(changePromptButton);
            Controls.Add(generateAllButton);
            Controls.Add(generateButton);
            Controls.Add(titlesListBox);
            Controls.Add(textAndPromptControl);
            Name = "ArticleView";
            Text = "ArticleForm";
            textAndPromptControl.ResumeLayout(false);
            tabContent.ResumeLayout(false);
            tabContent.PerformLayout();
            tabPrompt.ResumeLayout(false);
            tabPrompt.PerformLayout();
            tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)webView2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl textAndPromptControl;
        private TabPage tabContent;
        private TextBox content;
        private TabPage tabPrompt;
        private TextBox titleTextBox;
        private ListBox titlesListBox;
        private Button generateButton;
        private Button generateAllButton;
        private Button changePromptButton;
        private Button importTitlesButton;
        private Button addToPageButton;
        private TabPage tabPage1;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView2;
        private Button regenerateArticleButton;
        private Label titleLabel;
        private TextBox contentTextBox;
        private Label contentLabel;
        private Label tagsLabel;
        private TextBox tagsTextBox;
    }
}