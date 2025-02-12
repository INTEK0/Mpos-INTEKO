using System;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Bankttnminput : DevExpress.XtraEditors.XtraForm
    {
        private readonly POS_LAYOUT_NEW frm1;
        //private readonly POS_GAYTARMA_LAYOUT frmg1;
        public Bankttnminput(POS_LAYOUT_NEW frm/*, POS_GAYTARMA_LAYOUT frmg = null*/)
        {
            InitializeComponent();
            frm1 = frm;
            //frmg1 = frmg;
        }

        private void IsSucces()
        {
            //if (frm1 is null)
            //{
            //    frmg1.bankttnminputdata = textBox1.Text;
            //    this.Close();
            //}
            //else
            //{
            frm1.bankttnminputdata = textBox1.Text;
            DialogResult = DialogResult.OK;
            //}
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            IsSucces();
        }

        private void textBox1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode is Keys.Enter)
            {
                IsSucces();
            }
        }

        private void Bankttnminput_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (e.CloseReason)
            {
                case CloseReason.UserClosing:
                    DialogResult = DialogResult.Cancel;
                    break;
            }
        }
    }
}