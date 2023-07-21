namespace WinForms_Connect4
{
    partial class ArchiveGamesForm
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
            this.GamesCombo = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // GamesCombo
            // 
            this.GamesCombo.FormattingEnabled = true;
            this.GamesCombo.Location = new System.Drawing.Point(23, 105);
            this.GamesCombo.Name = "GamesCombo";
            this.GamesCombo.Size = new System.Drawing.Size(724, 21);
            this.GamesCombo.TabIndex = 0;
            // 
            // ArchiveGamesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 227);
            this.Controls.Add(this.GamesCombo);
            this.Name = "ArchiveGamesForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox GamesCombo;
    }
}