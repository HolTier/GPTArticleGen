namespace GPTArticleGen
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            TextBoxTest = new TextBox();
            ButtonTesst = new Button();
            SuspendLayout();
            // 
            // TextBoxTest
            // 
            TextBoxTest.Location = new Point(89, 22);
            TextBoxTest.Multiline = true;
            TextBoxTest.Name = "TextBoxTest";
            TextBoxTest.Size = new Size(355, 350);
            TextBoxTest.TabIndex = 0;
            // 
            // ButtonTesst
            // 
            ButtonTesst.Location = new Point(470, 22);
            ButtonTesst.Name = "ButtonTesst";
            ButtonTesst.Size = new Size(94, 29);
            ButtonTesst.TabIndex = 1;
            ButtonTesst.Text = "Test";
            ButtonTesst.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(ButtonTesst);
            Controls.Add(TextBoxTest);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox TextBoxTest;
        private Button ButtonTesst;
    }
}