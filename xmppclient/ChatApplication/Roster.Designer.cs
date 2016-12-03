namespace ChatApplication
{
    partial class Roster
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
            this.SuspendLayout();
            // 
            // Roster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1083, 687);
            this.Hostname = "desktop-gm279k1";
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "Roster";
            this.Password = "admin";
            this.Port = 5222;
            this.Text = "Roster";
            this.Tls = true;
            this.Username = "admin";
            this.Validate = null;
            this.Load += new System.EventHandler(this.Roster_Load);
            this.ResumeLayout(false);

        }

        #endregion
    }
}