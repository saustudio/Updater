using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Updater
{
    class ColorPanel : Panel
    {
        private Color _color;
        private Color _border;
        private bool _borderenbl = false;

        [Category("SettingColor")]
        public Color ColorDraw
        {
            get { return _color; }
            set { _color = value; Invalidate(); }
        }

        [Category("SettingColor")]
        public Color ColorDrawBorder
        {
            get { return _border; }
            set { _border = value; Invalidate(); }
        }

        [Category("SettingColor")]
        public bool ColorDrawEnable
        {
            get { return _borderenbl; }
            set { _borderenbl = value; Invalidate(); }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush blueBrush = new SolidBrush(_color);
            g.FillRectangle(blueBrush, this.ClientRectangle);

            if (_borderenbl) {
                Rectangle borderRectangle = this.ClientRectangle;
                ControlPaint.DrawBorder(e.Graphics, borderRectangle, _border, ButtonBorderStyle.Solid);
            }
           

            base.OnPaint(e);
        }
    }
}
