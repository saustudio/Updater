using CaptchaImageEx;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Updater
{
    public partial class FormCaptcha : Form
    {
        public Point downPoint = Point.Empty;
        public CaptchaImage captchaImg;
        public string textCaptcha = "";
        public FormCaptcha(string text)
        {
            InitializeComponent();
            captchaImg = new CaptchaImage(text, 350, 86, "Arial");
        }

        private void FormCaptcha_Paint(object sender, PaintEventArgs e)
        {
            Rectangle borderRectangle = this.ClientRectangle;
            ControlPaint.DrawBorder(e.Graphics, borderRectangle, Color.FromArgb(200, 0, 0, 0), ButtonBorderStyle.Solid);
        }

        private void FormCaptcha_Load(object sender, EventArgs e)
        {
            this.pictureBoxCaptcha.Image = this.captchaImg.Image;
        }

     

        private void rR_Button_OK_Click(object sender, EventArgs e)
        {
            if (this.textBoxCaptcha.Text.Length >= 5)
            {
                this.textCaptcha = this.textBoxCaptcha.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void rR_Button_CANCEL_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void textBoxCaptcha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                rR_Button_OK_Click(this, new EventArgs());
            }
        }
    }
}
