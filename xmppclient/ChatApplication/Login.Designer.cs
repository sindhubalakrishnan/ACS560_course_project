namespace ChatApplication
{
    partial class Login
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
            this.loginBtn = new System.Windows.Forms.Button();
            this.usrnamelbl = new System.Windows.Forms.Label();
            this.passwordlbl = new System.Windows.Forms.Label();
            this.usrnametxt1 = new System.Windows.Forms.TextBox();
            this.passwordtxt = new System.Windows.Forms.TextBox();
            this.cancelbtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // loginBtn
            // 
            this.loginBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginBtn.Location = new System.Drawing.Point(254, 229);
            this.loginBtn.Name = "loginBtn";
            this.loginBtn.Size = new System.Drawing.Size(95, 54);
            this.loginBtn.TabIndex = 0;
            this.loginBtn.Text = "Login";
            this.loginBtn.UseVisualStyleBackColor = true;
            this.loginBtn.Click += new System.EventHandler(this.loginBtn_Click);
            // 
            // usrnamelbl
            // 
            this.usrnamelbl.AutoSize = true;
            this.usrnamelbl.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usrnamelbl.Location = new System.Drawing.Point(56, 82);
            this.usrnamelbl.Name = "usrnamelbl";
            this.usrnamelbl.Size = new System.Drawing.Size(115, 28);
            this.usrnamelbl.TabIndex = 1;
            this.usrnamelbl.Text = "USERNAME";
            // 
            // passwordlbl
            // 
            this.passwordlbl.AutoSize = true;
            this.passwordlbl.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordlbl.Location = new System.Drawing.Point(56, 158);
            this.passwordlbl.Name = "passwordlbl";
            this.passwordlbl.Size = new System.Drawing.Size(117, 28);
            this.passwordlbl.TabIndex = 2;
            this.passwordlbl.Text = "PASSWORD";
            // 
            // usrnametxt1
            // 
            this.usrnametxt1.Location = new System.Drawing.Point(254, 82);
            this.usrnametxt1.Name = "usrnametxt1";
            this.usrnametxt1.Size = new System.Drawing.Size(176, 26);
            this.usrnametxt1.TabIndex = 3;
            // 
            // passwordtxt
            // 
            this.passwordtxt.Location = new System.Drawing.Point(254, 151);
            this.passwordtxt.Name = "passwordtxt";
            this.passwordtxt.Size = new System.Drawing.Size(176, 26);
            this.passwordtxt.TabIndex = 4;
            // 
            // cancelbtn
            // 
            this.cancelbtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelbtn.Location = new System.Drawing.Point(388, 229);
            this.cancelbtn.Name = "cancelbtn";
            this.cancelbtn.Size = new System.Drawing.Size(95, 54);
            this.cancelbtn.TabIndex = 5;
            this.cancelbtn.Text = "Cancel";
            this.cancelbtn.UseVisualStyleBackColor = true;
            // 
            // Login
            // 
            this.ClientSize = new System.Drawing.Size(567, 399);
            this.Controls.Add(this.cancelbtn);
            this.Controls.Add(this.passwordtxt);
            this.Controls.Add(this.usrnametxt1);
            this.Controls.Add(this.passwordlbl);
            this.Controls.Add(this.usrnamelbl);
            this.Controls.Add(this.loginBtn);
            this.Name = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button loginBtn;
        private System.Windows.Forms.Label usrnamelbl;
        private System.Windows.Forms.Label passwordlbl;
        private System.Windows.Forms.TextBox usrnametxt1;
        private System.Windows.Forms.TextBox passwordtxt;
        private System.Windows.Forms.Button cancelbtn;
    }
}