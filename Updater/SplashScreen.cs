using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Updater
{
    public partial class SplashScreen : Form
    {
        public bool _isload = false;
        public bool _iserror = false;
        public string _url;
        public string _path;

        public SplashScreen()
        {
            InitializeComponent();
        }

        public void ThreadUpdater()
        {
            try
            {
                new WebClient().DownloadFile(_url, _path);
            }
            catch{
                _iserror = true;
            }

            _isload = false;
        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            _isload = true;
            timer1.Enabled = true;
            new Thread(ThreadUpdater).Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!_isload) {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
