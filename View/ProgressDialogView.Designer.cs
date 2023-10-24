namespace GPTArticleGen.View
{
    partial class ProgressDialogView
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
            progressLabel = new Label();
            SuspendLayout();
            // 
            // progressLabel
            // 
            progressLabel.Location = new Point(0, 0);
            progressLabel.Name = "progressLabel";
            progressLabel.Size = new Size(329, 207);
            progressLabel.TabIndex = 0;
            progressLabel.Text = "";
            
            // 
            // ProgressDialogView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(328, 204);
            Controls.Add(progressLabel);
            Name = "ProgressDialogView";
            Text = "ProgressDialogView";
            ResumeLayout(false);
        }

        #endregion

        private Label progressLabel;
    }
}