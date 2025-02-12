using DevExpress.XtraEditors;
using System;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class BankTTNM : DevExpress.XtraEditors.XtraForm
    {
        public BankTTNM()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //yadda saxla
            var val = checkBox1.Checked ? 1 : 0;
            if (val > 0)
            {
                checkBox1.Text = "AÇIQDIR";


                string fileName = (Application.StartupPath + @"\BankTTNM.txt");
                string writeText = "1";
                FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);

                sw.WriteLine(writeText);
                sw.Close();
                fs.Close();


                XtraMessageBox.Show("ƏMƏLİYYAT UĞURLA BAŞA ÇATDI");

                GETSTATUS();
            }
            else
            {
                checkBox1.Text = "BAĞLIDIR";

                string fileName = (Application.StartupPath + @"\BankTTNM.txt");
                string writeText = "0";
                FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);

                StreamWriter sw = new StreamWriter(fs);

                sw.WriteLine(writeText);
                sw.Close();
                fs.Close();




                XtraMessageBox.Show("ƏMƏLİYYAT UĞURLA BAŞA ÇATDI");

                GETSTATUS();
            }
        }

        private void BankTTNM_Load(object sender, EventArgs e)
        {
            GETSTATUS();
        }

        public void GETSTATUS()
        {
            try
            {
                string fileName = (Application.StartupPath + @"\BankTTNM.txt");

                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                StreamReader sw = new StreamReader(fs);
                string data_ = sw.ReadLine();


                sw.Close();
                fs.Close();

                int number = Int32.Parse(data_);
                //XtraMessageBox.Show(number.ToString());
                if (number > 0)
                {
                    checkBox1.Checked = true;
                    checkBox1.Text = "AÇIQDIR";
                    labelControl1.Text = "HAL HAZIRDA BANK TTNM AÇIQDIR";
                }
                else
                {
                    checkBox1.Checked = false;
                    checkBox1.Text = "BAĞLIDIR";
                    labelControl1.Text = "HAL HAZIRDA BANK TTNM BAĞLIDIR";
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
                Console.WriteLine("Xəta!\n" + e);
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            var val = checkBox1.Checked ? 1 : 0;
            if (val > 0)
            {
                checkBox1.Text = "AÇIQDIR";
            }
            else
            {
                checkBox1.Text = "BAĞLIDIR";
            }
        }
    }
}