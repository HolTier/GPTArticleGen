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
            prompt = new TextBox();
            titlesBox = new ListBox();
            generateButton = new Button();
            generateAllButton = new Button();
            changePromptButton = new Button();
            importTitlesButton = new Button();
            addToPageButton = new Button();
            textAndPromptControl.SuspendLayout();
            tabContent.SuspendLayout();
            tabPrompt.SuspendLayout();
            SuspendLayout();
            // 
            // textAndPromptControl
            // 
            textAndPromptControl.Controls.Add(tabContent);
            textAndPromptControl.Controls.Add(tabPrompt);
            textAndPromptControl.Location = new Point(0, 0);
            textAndPromptControl.Name = "textAndPromptControl";
            textAndPromptControl.SelectedIndex = 0;
            textAndPromptControl.Size = new Size(406, 452);
            textAndPromptControl.TabIndex = 0;
            // 
            // tabContent
            // 
            tabContent.Controls.Add(content);
            tabContent.Location = new Point(4, 29);
            tabContent.Name = "tabContent";
            tabContent.Padding = new Padding(3);
            tabContent.Size = new Size(398, 419);
            tabContent.TabIndex = 0;
            tabContent.Text = "Content";
            tabContent.UseVisualStyleBackColor = true;
            // 
            // content
            // 
            content.Location = new Point(0, 0);
            content.Multiline = true;
            content.Name = "content";
            content.Size = new Size(398, 424);
            content.TabIndex = 0;
            // 
            // tabPrompt
            // 
            tabPrompt.Controls.Add(prompt);
            tabPrompt.Location = new Point(4, 29);
            tabPrompt.Name = "tabPrompt";
            tabPrompt.Padding = new Padding(3);
            tabPrompt.Size = new Size(398, 419);
            tabPrompt.TabIndex = 1;
            tabPrompt.Text = "Prompt";
            tabPrompt.UseVisualStyleBackColor = true;
            // 
            // prompt
            // 
            prompt.Location = new Point(0, 0);
            prompt.Multiline = true;
            prompt.Name = "prompt";
            prompt.Size = new Size(402, 419);
            prompt.TabIndex = 0;
            // 
            // titlesBox
            // 
            titlesBox.FormattingEnabled = true;
            titlesBox.ItemHeight = 20;
            titlesBox.Location = new Point(412, 29);
            titlesBox.Name = "titlesBox";
            titlesBox.Size = new Size(208, 424);
            titlesBox.TabIndex = 1;
            // 
            // generateButton
            // 
            generateButton.Location = new Point(626, 29);
            generateButton.Name = "generateButton";
            generateButton.Size = new Size(149, 29);
            generateButton.TabIndex = 2;
            generateButton.Text = "Generate";
            generateButton.UseVisualStyleBackColor = true;
            // 
            // generateAllButton
            // 
            generateAllButton.Location = new Point(626, 64);
            generateAllButton.Name = "generateAllButton";
            generateAllButton.Size = new Size(149, 29);
            generateAllButton.TabIndex = 3;
            generateAllButton.Text = "Generate for all";
            generateAllButton.UseVisualStyleBackColor = true;
            // 
            // changePromptButton
            // 
            changePromptButton.Location = new Point(626, 99);
            changePromptButton.Name = "changePromptButton";
            changePromptButton.Size = new Size(149, 29);
            changePromptButton.TabIndex = 4;
            changePromptButton.Text = "Change Prompt";
            changePromptButton.UseVisualStyleBackColor = true;
            // 
            // importTitlesButton
            // 
            importTitlesButton.Location = new Point(626, 134);
            importTitlesButton.Name = "importTitlesButton";
            importTitlesButton.Size = new Size(149, 29);
            importTitlesButton.TabIndex = 5;
            importTitlesButton.Text = "Import Titles";
            importTitlesButton.UseVisualStyleBackColor = true;
            // 
            // addToPageButton
            // 
            addToPageButton.Location = new Point(626, 169);
            addToPageButton.Name = "addToPageButton";
            addToPageButton.Size = new Size(149, 29);
            addToPageButton.TabIndex = 6;
            addToPageButton.Text = "Add to page";
            addToPageButton.UseVisualStyleBackColor = true;
            // 
            // ArticleView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 465);
            Controls.Add(addToPageButton);
            Controls.Add(importTitlesButton);
            Controls.Add(changePromptButton);
            Controls.Add(generateAllButton);
            Controls.Add(generateButton);
            Controls.Add(titlesBox);
            Controls.Add(textAndPromptControl);
            Name = "ArticleView";
            Text = "ArticleForm";
            textAndPromptControl.ResumeLayout(false);
            tabContent.ResumeLayout(false);
            tabContent.PerformLayout();
            tabPrompt.ResumeLayout(false);
            tabPrompt.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl textAndPromptControl;
        private TabPage tabContent;
        private TextBox content;
        private TabPage tabPrompt;
        private TextBox prompt;
        private ListBox titlesBox;
        private Button generateButton;
        private Button generateAllButton;
        private Button changePromptButton;
        private Button importTitlesButton;
        private Button addToPageButton;
    }
}