using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameTexturePackManager
{
    public partial class ProgressBarForm : Form
    {
        public ProgressBarForm(string text)
        {
            InitializeComponent();
            Text = text;
            progressLabel.Text = "";
            progressBar.Value = 0;
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressLabel.Text = e.UserState != null ? e.UserState.ToString() : "Working on task...";
            progressLabel.Update();
            progressBar.Value = e.ProgressPercentage;
            progressBar.Update();
            Update();
        }
    }
}
