using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class MÜŞTƏRİLƏR : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public MÜŞTƏRİLƏR()
        {
            InitializeComponent();
        }

        private void buttonEdit1_EditValueChanged(object sender, EventArgs e)
        {
          
        }

        private void buttonEdit1_Click(object sender, EventArgs e)
        {
         
        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "txt",
                Filter = "Pdf files (*.pdf)|*.pdf",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                buttonEdit1.Text = openFileDialog1.FileName;

                //daxili emr 
                byte[] daxili_emrfile = SaveFile(buttonEdit1.Text.ToString());
                daxili_emr_file_update1 = daxili_emrfile;

                daxili_emr_file_name1 = buttonEdit1.Text.ToString();
            }
        }
        public static byte[] daxili_emr_file_update1 = null;
        public static string  daxili_emr_file_name1= null;

        public static byte[] daxili_emr_file_update2 = null;
        public static string daxili_emr_file_name2 = null;

        public static byte[] daxili_emr_file_update3 = null;
        public static string daxili_emr_file_name3 = null;

        public static byte[] picture_edit_value = null;
        private byte[] SaveFile(string filepath)
        {
            using (Stream stream = File.OpenRead(filepath))
            {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                string extn = new FileInfo(filepath).Extension;
                return buffer;
            }

        }

        private void buttonEdit2_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void buttonEdit2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "txt",
                Filter = "Pdf files (*.pdf)|*.pdf",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                buttonEdit2.Text = openFileDialog1.FileName;

                //daxili emr 
                byte[] daxili_emrfile = SaveFile(buttonEdit2.Text.ToString());
                daxili_emr_file_update2 = daxili_emrfile;

                daxili_emr_file_name2 = buttonEdit2.Text.ToString();
            }
        }

        private void buttonEdit3_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "txt",
                Filter = "Pdf files (*.pdf)|*.pdf",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                buttonEdit3.Text = openFileDialog1.FileName;

                //daxili emr 
                byte[] daxili_emrfile = SaveFile(buttonEdit3.Text.ToString());
                daxili_emr_file_update3 = daxili_emrfile;

                daxili_emr_file_name3 = buttonEdit3.Text.ToString();
            }
        }

        private void pictureEdit1_EditValueChanged(object sender, EventArgs e)
        {
            //picture 
            if (pictureEdit1.Image == null)
            {

            }
            else
            {
                MemoryStream stream = new MemoryStream();
                pictureEdit1.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] pic = stream.ToArray();
                picture_edit_value = pic;
            }
        }
        private string qeryString = "  EXEC dbo.MUSTERI_EMELIYYAT_NOMRE";
        public void GETKOD()
        {

            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon))
            {

                SqlCommand command = new SqlCommand(qeryString, connection);


                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        //XtraMessageBox.Show(reader[0].ToString());

                        textEdit17.Text = reader[0].ToString();
                        textEdit17.Enabled = false;

                    }
                    reader.Close();


                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.Message);
                    XtraMessageBox.Show(ex.Message);
                }
            }

        }
        private void MUSTERILER_Load(object sender, EventArgs e)
        {
            //
            GETKOD();
            GETCINS();
            GETVETENDASLIQ();
            DateTime dateTime = DateTime.UtcNow.Date;
            dateEdit4.Text = dateTime.ToString();
        }
        private void GETVETENDASLIQ()
        {
            //int id = Convert.ToInt32(lookUpEdit1.EditValue.ToString());

            //if (id > 0)
            //{
            string strQuery = "  select vetendaslig_id,vetendaslig as N'VƏTƏNDAŞLIQ' from vetendaslig ";
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = GetData(cmd);
            lookUpEdit2.Properties.DisplayMember = "VƏTƏNDAŞLIQ";
            lookUpEdit2.Properties.ValueMember = "vetendaslig_id";
            lookUpEdit2.Properties.DataSource = dt;
            lookUpEdit2.Properties.NullText = "--Seçin--";
            lookUpEdit2.Properties.PopulateColumns();
            lookUpEdit2.Properties.Columns[0].Visible = false;
            //}
        }
        private void GETCINS()
        {
            //int id = Convert.ToInt32(lookUpEdit1.EditValue.ToString());

            //if (id > 0)
            //{
            string strQuery = "    select cins_id,cins AS N'CİNSİ' from cins";
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = GetData(cmd);
            lookUpEdit1.Properties.DisplayMember = "CİNSİ";
            lookUpEdit1.Properties.ValueMember = "cins_id";
            lookUpEdit1.Properties.DataSource = dt;
            lookUpEdit1.Properties.NullText = "--Seçin--";
            lookUpEdit1.Properties.PopulateColumns();
            lookUpEdit1.Properties.Columns[0].Visible = false;
            //}
        }

        private DataTable GetData(SqlCommand cmd)

        {

            DataTable dt = new DataTable();


            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);

            SqlDataAdapter sda = new SqlDataAdapter();

            cmd.CommandType = CommandType.Text;

            cmd.Connection = con;

            try

            {

                con.Open();

                sda.SelectCommand = cmd;

                sda.Fill(dt);

                return dt;

            }

            catch (Exception)

            {



                return null;

            }

            finally

            {

                con.Close();

                sda.Dispose();

                con.Dispose();

            }

        }
        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                int a = 0;
                //= mc.INSERT_MUSTERI_b(Convert.ToDateTime(dateEdit4.Text),textEdit4.Text.ToString(),
                //    textEdit5.Text.ToString(),textEdit3.Text.ToString(),textEdit1.Text.ToString(),
                //    Convert.ToDateTime (dateEdit1.Text) ,Convert.ToDateTime(dateEdit2.Text),Convert.ToDateTime(dateEdit3.Text)
                //    ,textEdit16.Text.ToString(),textEdit2.Text.ToString(),textEdit6.Text.ToString(),
                //    //textEdit19.Text.ToString()
                //   //,
                //    lookUpEdit1.Text.ToString(),lookUpEdit2.Text.ToString(),
                //    textEdit10.Text.ToString(),textEdit11.Text.ToString(),memoEdit1.Text.ToString(),
                //    textEdit17.Text.ToString(), textEdit7.Text.ToString(),
                //    textEdit20.Text.ToString(),
                //    textEdit22.Text.ToString(),
                //    textEdit18.Text.ToString(),
                //    textEdit21.Text.ToString(),
                //    textEdit23.Text.ToString()

                    //)         ;
                if (a > 0)
                {
                    XtraMessageBox.Show("UĞURLA TAMAMLANDI");
                    CLEAR();
                    GETKOD();
                }
                else
                {
                    XtraMessageBox.Show("BELƏ BİR İSTİFADƏÇİ ARTIQ MÖVCUDDUR");
                }
                //GETKOD();
            }
            catch
            {
                //
                XtraMessageBox.Show("UĞURSUZ TAMAMLANDI");
            }
           
        }

        private void CLEAR()
        {
            textEdit4.Text = "";
            textEdit5.Text = "";
            textEdit3.Text = "";
            textEdit1.Text = "";
            dateEdit1.Text = "";
            dateEdit2.Text = "";
            dateEdit3.Text = "";
            textEdit16.Text = "";
            textEdit2.Text = "";
            textEdit6.Text = "";
            textEdit19.Text = "";
            buttonEdit1.Text = "";
            buttonEdit2.Text = "";
            buttonEdit3.Text = "";
            textEdit9.Text = "";
            textEdit10.Text = "";
            textEdit11.Text = "";
            memoEdit1.Text = "";
            lookUpEdit1.Text = "--Seçin--";
            lookUpEdit2.Text = "--Seçin--";

            //lookUpEdit1.Properties.DisplayMember = "CİNSİ";
            //lookUpEdit1.Properties.ValueMember = "cins_id";
            //lookUpEdit1.Properties.DataSource = dt;
            lookUpEdit1.Properties.NullText = "--Seçin--";
            //lookUpEdit1.Properties.PopulateColumns();
            //lookUpEdit1.Properties.Columns[0].Visible = false;
        }

        private void labelControl3_Click(object sender, EventArgs e)
        {

        }

        private void lookUpEdit1_TextChanged(object sender, EventArgs e)
        {
            GETCINS();
        }

        private void lookUpEdit2_TextChanged(object sender, EventArgs e)
        {
            GETVETENDASLIQ();
        }

     

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ribbon_Click(object sender, EventArgs e)
        {

        }
    }
}