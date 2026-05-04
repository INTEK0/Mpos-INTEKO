using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace WindowsFormsApp2.Forms
{
    public partial class fInfo : DevExpress.XtraEditors.XtraForm
    {
        public fInfo()
        {
            InitializeComponent();
        }

        private void fInfo_Load(object sender, EventArgs e)
        {
            labelControl1.Text = labelControl1.Text.Replace("{year}", DateTime.Now.Year.ToString());
            lVersion.Text = $"Versiya: {Application.ProductVersion}";
        }

        private void hyperlinkLabelControl1_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = lWebLink.Text,
                UseShellExecute = true
            });
        }
    }
}