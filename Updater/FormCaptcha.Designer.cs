namespace Updater
{
    partial class FormCaptcha
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
            this.pictureBoxCaptcha = new System.Windows.Forms.PictureBox();
            this.rR_Button_CANCEL = new RR_DesignUI.RR_Button();
            this.rR_Button_OK = new RR_DesignUI.RR_Button();
            this.colorPanel1 = new Updater.ColorPanel();
            this.textBoxCaptcha = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCaptcha)).BeginInit();
            this.colorPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxCaptcha
            // 
            this.pictureBoxCaptcha.ErrorImage = null;
            this.pictureBoxCaptcha.InitialImage = null;
            this.pictureBoxCaptcha.Location = new System.Drawing.Point(18, 20);
            this.pictureBoxCaptcha.Name = "pictureBoxCaptcha";
            this.pictureBoxCaptcha.Size = new System.Drawing.Size(350, 86);
            this.pictureBoxCaptcha.TabIndex = 0;
            this.pictureBoxCaptcha.TabStop = false;
            // 
            // rR_Button_CANCEL
            // 
            this.rR_Button_CANCEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(133)))), ((int)(((byte)(4)))));
            this.rR_Button_CANCEL.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rR_Button_CANCEL.ForeColor = System.Drawing.Color.Black;
            this.rR_Button_CANCEL.Location = new System.Drawing.Point(166, 186);
            this.rR_Button_CANCEL.Name = "rR_Button_CANCEL";
            this.rR_Button_CANCEL.Rounding = 25;
            this.rR_Button_CANCEL.RoundingEnable = true;
            this.rR_Button_CANCEL.Size = new System.Drawing.Size(204, 28);
            this.rR_Button_CANCEL.TabIndex = 8;
            this.rR_Button_CANCEL.Text = "CANCEL";
            this.rR_Button_CANCEL.TextHover = null;
            this.rR_Button_CANCEL.Click += new System.EventHandler(this.rR_Button_CANCEL_Click);
            // 
            // rR_Button_OK
            // 
            this.rR_Button_OK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(133)))), ((int)(((byte)(4)))));
            this.rR_Button_OK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rR_Button_OK.ForeColor = System.Drawing.Color.Black;
            this.rR_Button_OK.Location = new System.Drawing.Point(18, 186);
            this.rR_Button_OK.Name = "rR_Button_OK";
            this.rR_Button_OK.Rounding = 25;
            this.rR_Button_OK.RoundingEnable = true;
            this.rR_Button_OK.Size = new System.Drawing.Size(142, 28);
            this.rR_Button_OK.TabIndex = 1;
            this.rR_Button_OK.Text = "OK";
            this.rR_Button_OK.TextHover = null;
            this.rR_Button_OK.Click += new System.EventHandler(this.rR_Button_OK_Click);
            // 
            // colorPanel1
            // 
            this.colorPanel1.ColorDraw = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(23)))), ((int)(((byte)(33)))));
            this.colorPanel1.ColorDrawBorder = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(48)))), ((int)(((byte)(79)))));
            this.colorPanel1.ColorDrawEnable = true;
            this.colorPanel1.Controls.Add(this.textBoxCaptcha);
            this.colorPanel1.Location = new System.Drawing.Point(18, 128);
            this.colorPanel1.Name = "colorPanel1";
            this.colorPanel1.Size = new System.Drawing.Size(350, 38);
            this.colorPanel1.TabIndex = 6;
            // 
            // textBoxCaptcha
            // 
            this.textBoxCaptcha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(23)))), ((int)(((byte)(33)))));
            this.textBoxCaptcha.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxCaptcha.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxCaptcha.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCaptcha.ForeColor = System.Drawing.Color.White;
            this.textBoxCaptcha.Location = new System.Drawing.Point(8, 7);
            this.textBoxCaptcha.MaxLength = 5;
            this.textBoxCaptcha.Name = "textBoxCaptcha";
            this.textBoxCaptcha.Size = new System.Drawing.Size(335, 25);
            this.textBoxCaptcha.TabIndex = 7;
            this.textBoxCaptcha.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxCaptcha.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxCaptcha_KeyDown);
            // 
            // FormCaptcha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(9)))), ((int)(((byte)(15)))));
            this.ClientSize = new System.Drawing.Size(389, 240);
            this.Controls.Add(this.rR_Button_CANCEL);
            this.Controls.Add(this.rR_Button_OK);
            this.Controls.Add(this.colorPanel1);
            this.Controls.Add(this.pictureBoxCaptcha);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormCaptcha";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormCaptcha";
            this.Load += new System.EventHandler(this.FormCaptcha_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormCaptcha_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCaptcha)).EndInit();
            this.colorPanel1.ResumeLayout(false);
            this.colorPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxCaptcha;
        private ColorPanel colorPanel1;
        private System.Windows.Forms.TextBox textBoxCaptcha;
        private RR_DesignUI.RR_Button rR_Button_OK;
        private RR_DesignUI.RR_Button rR_Button_CANCEL;
    }
}