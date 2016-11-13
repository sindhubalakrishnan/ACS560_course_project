namespace ChatApplication
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.refreshContactsbtn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.convHistorytxb = new System.Windows.Forms.RichTextBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.sndMsgbtn = new System.Windows.Forms.Button();
            this.sendMsgtbx = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.refreshContactsbtn);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(366, 693);
            this.panel1.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Location = new System.Drawing.Point(22, 71);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(304, 516);
            this.panel4.TabIndex = 14;
            // 
            // refreshContactsbtn
            // 
            this.refreshContactsbtn.Location = new System.Drawing.Point(22, 21);
            this.refreshContactsbtn.Name = "refreshContactsbtn";
            this.refreshContactsbtn.Size = new System.Drawing.Size(194, 30);
            this.refreshContactsbtn.TabIndex = 6;
            this.refreshContactsbtn.Text = "Refresh Contacts";
            this.refreshContactsbtn.UseVisualStyleBackColor = true;
            this.refreshContactsbtn.Click += new System.EventHandler(this.refreshContactsbtn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(366, 593);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(240)))));
            this.panel2.Controls.Add(this.button6);
            this.panel2.Controls.Add(this.button5);
            this.panel2.Controls.Add(this.button4);
            this.panel2.Controls.Add(this.button3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 593);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(366, 100);
            this.panel2.TabIndex = 0;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(258, 41);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(82, 32);
            this.button6.TabIndex = 3;
            this.button6.Text = "newuser";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(176, 42);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(76, 31);
            this.button5.TabIndex = 2;
            this.button5.Text = "users";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(95, 42);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 31);
            this.button4.TabIndex = 1;
            this.button4.Text = "keypad";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 41);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(76, 37);
            this.button3.TabIndex = 0;
            this.button3.Text = "home";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.convHistorytxb);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(366, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(766, 496);
            this.panel3.TabIndex = 1;
            // 
            // convHistorytxb
            // 
            this.convHistorytxb.AcceptsTab = true;
            this.convHistorytxb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.convHistorytxb.Location = new System.Drawing.Point(0, 0);
            this.convHistorytxb.Name = "convHistorytxb";
            this.convHistorytxb.Size = new System.Drawing.Size(766, 496);
            this.convHistorytxb.TabIndex = 0;
            this.convHistorytxb.Text = "";
            // 
            // sndMsgbtn
            // 
            this.sndMsgbtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.sndMsgbtn.Location = new System.Drawing.Point(685, 3);
            this.sndMsgbtn.Name = "sndMsgbtn";
            this.sndMsgbtn.Size = new System.Drawing.Size(78, 197);
            this.sndMsgbtn.TabIndex = 3;
            this.sndMsgbtn.Text = "Send";
            this.sndMsgbtn.UseVisualStyleBackColor = true;
            this.sndMsgbtn.Click += new System.EventHandler(this.sndMsgbtn_Click);
            // 
            // sendMsgtbx
            // 
            this.sendMsgtbx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sendMsgtbx.Location = new System.Drawing.Point(3, 3);
            this.sendMsgtbx.Name = "sendMsgtbx";
            this.sendMsgtbx.Size = new System.Drawing.Size(676, 197);
            this.sendMsgtbx.TabIndex = 4;
            this.sendMsgtbx.Text = "";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 89.16449F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.83551F));
            this.tableLayoutPanel1.Controls.Add(this.sndMsgbtn, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.sendMsgtbx, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(366, 490);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(766, 203);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1132, 693);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button refreshContactsbtn;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button sndMsgbtn;
        private System.Windows.Forms.RichTextBox sendMsgtbx;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RichTextBox convHistorytxb;
    }
}

