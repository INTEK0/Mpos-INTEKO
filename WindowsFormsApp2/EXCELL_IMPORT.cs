using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;



using System.Configuration;
using ExcelDataReader;
using DevExpress.DataAccess.Native.Data;
using System.Data.SqlClient;
using WindowsFormsApp2.Helpers;
using static WindowsFormsApp2.Helpers.DB.DatabaseClasses;
using static WindowsFormsApp2.Helpers.Enums;
using static WindowsFormsApp2.Helpers.FormHelpers;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using DevExpress.XtraCharts.Designer.Native;



namespace WindowsFormsApp2
{
    public partial class EXCELL_IMPORT : DevExpress.XtraEditors.XtraForm
    {
        DataTableCollection tableCollection;
        public EXCELL_IMPORT()
        {
            InitializeComponent();
        }


        private void simpleButton1_Click(object sender, EventArgs e)
        {

            using (OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Excell 97-2003 Workbook|.xls|Excell Workbook|*.xlsx",
                FilterIndex = 2,
            })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textEdit1.Text = openFileDialog.FileName;
                    using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                            });
                            tableCollection = result.Tables;
                            comboBox1.Items.Clear();
                            foreach (System.Data.DataTable table in tableCollection)
                                comboBox1.Items.Add(table.TableName);
                        }

                    }
                }

            }
        }

        private void selectedindexchanged_Click(object sender, EventArgs e)
        {
            System.Data.DataTable dt = tableCollection[comboBox1.SelectedItem.ToString()];
            //gridControl1.DataSource = dt;
            dataGridView1.DataSource = dt;
        }

        private System.Data.DataTable GetDTfromDGV(DataGridView dgv)
        {
            // Macking our DataTable
            System.Data.DataTable dt = new System.Data.DataTable();
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                dt.Columns.Add(column.Name, typeof(string));
            }
            // Getting data
            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                DataRow dr = dt.NewRow();
                for (int col = 0; col < dgv.Columns.Count; col++)
                {
                    dr[col] = dgvRow.Cells[col].Value;
                }
                dt.Rows.Add(dr);
            }
            // removing empty rows
            for (int row = dt.Rows.Count - 1; row >= 0; row--)
            {
                bool flag = true;
                for (int col = 0; col < dt.Columns.Count; col++)
                {
                    if (dt.Rows[row][col] != DBNull.Value)
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag == true)
                {
                    dt.Rows.RemoveAt(row);
                }
            }
            return dt;
        }

        private void WriteToSQL(System.Data.DataTable dt)
        {
            //  string connection = new SqlConnection(Properties.Settings.Default.SqlCon);
            //   string connectionStringSQL = "Your connection string";
            using (SqlConnection sqlConn = new SqlConnection(Properties.Settings.Default.SqlCon))
            {
                SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlConn);
                // Setting the database table name
                sqlBulkCopy.DestinationTableName = "EXCELL_IMPORT_DATA_NEW";
                // Mapping the DataTable columns with that of the database table
                sqlBulkCopy.ColumnMappings.Add(dt.Columns[0].ColumnName, "ALIS_TARIHI");
                sqlBulkCopy.ColumnMappings.Add(dt.Columns[1].ColumnName, "FAKTURA_NO");
                sqlBulkCopy.ColumnMappings.Add(dt.Columns[2].ColumnName, "TECHIZATCI_ADI");
                sqlBulkCopy.ColumnMappings.Add(dt.Columns[3].ColumnName, "MEHSUL_KODU");
                sqlBulkCopy.ColumnMappings.Add(dt.Columns[4].ColumnName, "KATEGORIYA");
                sqlBulkCopy.ColumnMappings.Add(dt.Columns[5].ColumnName, "MEHSUL_ADI");
                sqlBulkCopy.ColumnMappings.Add(dt.Columns[6].ColumnName, "MEHSULUN_MIGDARI");
                sqlBulkCopy.ColumnMappings.Add(dt.Columns[7].ColumnName, "VAHIDI");
                sqlBulkCopy.ColumnMappings.Add(dt.Columns[8].ColumnName, "SATIS_GIYMETI");
                sqlBulkCopy.ColumnMappings.Add(dt.Columns[9].ColumnName, "SATINALMA_GIYMETI");
                sqlBulkCopy.ColumnMappings.Add(dt.Columns[10].ColumnName, "EDV");
                sqlBulkCopy.ColumnMappings.Add(dt.Columns[11].ColumnName, "BARKOD");
                sqlBulkCopy.ColumnMappings.Add(dt.Columns[12].ColumnName, "ISTEHSAL_TARIHI");
                sqlBulkCopy.ColumnMappings.Add(dt.Columns[13].ColumnName, "SONISTIFADE_TARIHI");
                sqlBulkCopy.ColumnMappings.Add(dt.Columns[14].ColumnName, "TESVIR");
                //sqlBulkCopy.ColumnMappings.Add(dt.Columns[10].ColumnName, "terazi");
                sqlConn.Open();
                sqlBulkCopy.WriteToServer(dt);
            }
        }

        techizatci_odenis tr = new techizatci_odenis();

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            var importType = this.Controls.OfType<CheckEdit>().FirstOrDefault(x => x.Checked);
            if (importType.Tag.ToString() == Enums.OperationType.ExcelImport_Control.ToString())
            {
                kontrollu();
            }
            else
            {
                kontrolsuz();
            }
        }

        public void kontrolsuz()
        {
        Excelimport:
            string tezhicatci = null, kategori = null, malzeme = null, countstring = null, barkod = null, mkod = null, id = null, fatno = null, fattarih = null;
            int count = 0;
            //delete data
            tr.DELETE_import_exc();
            //import data 
            // Getting data from DataGridView
            System.Data.DataTable myDt = new System.Data.DataTable();
            myDt = GetDTfromDGV(dataGridView1);

            // Writing to sql
            WriteToSQL(myDt);

            // tr.bulk_import_exc();




            // Tezhizatci Acilmasi

            SqlConnection con = new SqlConnection();
            SqlConnection cont = new SqlConnection();

            con.ConnectionString = Properties.Settings.Default.SqlCon;
            cont.ConnectionString = Properties.Settings.Default.SqlCon;

            string query = "SELECT count(*) AS COUNTS,TECHIZATCI_ADI \r\n  FROM [EXCELL_IMPORT_DATA_NEW]\r\n\r\n \r\n  WHERE TECHIZATCI_ADI NOT IN\r\n  (\r\n  SELECT  [SIRKET_ADI]\r\n    \r\n  FROM [NewIntekobir].[COMPANY].[TECHIZATCI] WHERE IsDeleted=0\r\n  )\r\n  group by TECHIZATCI_ADI";

            SqlCommand command = new SqlCommand(query, con);

            con.Open();
            SqlDataReader dr = command.ExecuteReader();

            while (dr.Read())
            {
                tezhicatci = dr["TECHIZATCI_ADI"].ToString();
                countstring = dr["COUNTS"].ToString();


                if (Convert.ToInt32(countstring) > 0)
                {

                    string query2 = "insert into COMPANY.TECHIZATCI (TECHIZATCI_NOMRE,SIRKET_ADI )    SELECT case when ('T-'+ RTRIM( LTRIM(CAST(MAX(CAST(REPLACE(TECHIZATCI_NOMRE,'T-','')  \r\n   AS INT)+1) AS NCHAR(10)))) ) is null then 'T-1' ELSE\r\n   \r\n   ('T-'+ RTRIM( LTRIM(CAST(MAX(CAST(REPLACE(TECHIZATCI_NOMRE,'T-','')  \r\n   AS INT)+1) AS NCHAR(10)))) )\r\n    END as col,@techizatci FROM COMPANY.TECHIZATCI";

                    SqlCommand command2 = new SqlCommand(query2, cont);

                    command2.Parameters.AddWithValue("@techizatci", tezhicatci);
                    cont.Open();
                    command2.ExecuteNonQuery();
                    cont.Close();
                }

                else
                {

                }


            }


            con.Close();




            // Kategori Acilmasi



            con.ConnectionString = Properties.Settings.Default.SqlCon;
            cont.ConnectionString = Properties.Settings.Default.SqlCon;

            string querykategori = "SELECT count(*) AS COUNTS,KATEGORIYA FROM [EXCELL_IMPORT_DATA_NEW] WHERE KATEGORIYA NOT IN\r\n  (\r\n  SELECT KATEGORIYA FROM KATEGORIYA\r\n  )\r\n  GROUP BY KATEGORIYA";

            SqlCommand commandkategori = new SqlCommand(querykategori, con);

            con.Open();
            SqlDataReader drkategori = commandkategori.ExecuteReader();

            while (drkategori.Read())
            {
                kategori = drkategori["KATEGORIYA"].ToString();
                countstring = drkategori["COUNTS"].ToString();


                if (Convert.ToInt32(countstring) > 0)
                {

                    string querykategori2 = "INSERT INTO [dbo].[KATEGORIYA] ([KATEGORIYA])  VALUES (@kategoriya)";

                    SqlCommand command2 = new SqlCommand(querykategori2, cont);

                    command2.Parameters.AddWithValue("@kategoriya", kategori);
                    cont.Open();
                    command2.ExecuteNonQuery();
                    cont.Close();
                }
            }

            con.Close();




            // Fatura No  



            con.ConnectionString = Properties.Settings.Default.SqlCon;
            cont.ConnectionString = Properties.Settings.Default.SqlCon;

            string queryfat = "SELECT COUNT(*) AS COUNTS FROM [EXCELL_IMPORT_DATA_NEW] WHERE\r\n\r\n(LEN(FAKTURA_NO)<2 OR LEN(FAKTURA_NO) IS NULL)";

            SqlCommand commandfat = new SqlCommand(queryfat, con);

            con.Open();
            SqlDataReader drfat = commandfat.ExecuteReader();

            while (drfat.Read())
            {

                countstring = drfat["COUNTS"].ToString();


                if (Convert.ToInt32(countstring) > 0)
                {





                }
            }

            con.Close();






            //  Malzeme Acilmasi



            con.ConnectionString = Properties.Settings.Default.SqlCon;
            cont.ConnectionString = Properties.Settings.Default.SqlCon;

            string querymalzeme = "SELECT *   ,\r\n\r\n CASE WHEN \r\n \r\n ( SELECT TOP 1 MEHSUL_ADI   FROM [MAL_ALISI_DETAILS] WHERE BARKOD=[EXCELL_IMPORT_DATA_NEW].BARKOD)=MEHSUL_ADI   \r\n THEN 1 ELSE    \r\n \r\n (   CASE WHEN    ( SELECT  TOP 1 MEHSUL_ADI   FROM [MAL_ALISI_DETAILS] WHERE MEHSUL_KODU=[EXCELL_IMPORT_DATA_NEW].MEHSUL_KODU)=[EXCELL_IMPORT_DATA_NEW].MEHSUL_ADI    \r\n \r\n   THEN  1 else 0 end)   \r\n \r\n  END AS [KNTBARKODSTOK] ,\r\n  \r\n\r\n  CASE WHEN \r\n \r\n ( SELECT TOP 1 MEHSUL_ADI   FROM [MAL_ALISI_DETAILS] WHERE MEHSUL_ADI=[EXCELL_IMPORT_DATA_NEW].MEHSUL_ADI)=MEHSUL_ADI   \r\n THEN 1 ELSE    \r\n 0 end MALZEMEADI\r\n  \r\n    \r\n   FROM [EXCELL_IMPORT_DATA_NEW] ";

            SqlCommand commandmalzeme = new SqlCommand(querymalzeme, con);

            con.Open();
            SqlDataReader drmalzeme = commandmalzeme.ExecuteReader();

            while (drmalzeme.Read())
            {
                malzeme = drmalzeme["MEHSUL_ADI"].ToString();
                mkod = drmalzeme["MEHSUL_KODU"].ToString();
                barkod = drmalzeme["BARKOD"].ToString();
                id = drmalzeme["ID"].ToString();
                string kontrolkod = drmalzeme["KNTBARKODSTOK"].ToString();
                int kontrolkodbarkod = Int32.Parse(kontrolkod);
                string kontrolad = drmalzeme["MALZEMEADI"].ToString();
                int kontrolmalzemead = Int32.Parse(kontrolad);
                if (kontrolkodbarkod == 0 && kontrolmalzemead == 1)
                {




                    string querymalzeme2 = "UPDATE  EXCELL_IMPORT_DATA_NEW  set KONTROL=0 where ID= (@id)";

                    SqlCommand command3 = new SqlCommand(querymalzeme2, cont);

                    command3.Parameters.AddWithValue("@id", id);
                    cont.Open();
                    command3.ExecuteNonQuery();
                    cont.Close();
                }




                else if (kontrolkodbarkod == 1 && kontrolmalzemead == 0)
                {




                    string querymalzeme2 = "UPDATE  EXCELL_IMPORT_DATA_NEW  set KONTROL=0 where ID= (@id)";

                    SqlCommand command3 = new SqlCommand(querymalzeme2, cont);

                    command3.Parameters.AddWithValue("@id", id);
                    cont.Open();
                    command3.ExecuteNonQuery();
                    cont.Close();


                }

                else if (kontrolkodbarkod == 1 && kontrolmalzemead == 1)
                {
                    string querymalzeme2 = "UPDATE  EXCELL_IMPORT_DATA_NEW  set KONTROL=0 where ID= (@id)";

                    SqlCommand command3 = new SqlCommand(querymalzeme2, cont);

                    command3.Parameters.AddWithValue("@id", id);
                    cont.Open();
                    command3.ExecuteNonQuery();
                    cont.Close();

                }

                else
                {



                    string querymalzeme2 = "UPDATE  EXCELL_IMPORT_DATA_NEW  set KONTROL=1 where ID= (@id)";

                    SqlCommand command3 = new SqlCommand(querymalzeme2, cont);

                    command3.Parameters.AddWithValue("@id", id);
                    cont.Open();
                    command3.ExecuteNonQuery();
                    cont.Close();


                }
            }

            con.Close();




            string queryqaimet = "SELECT  COUNT(*) TARIHKONTROL FROM [dbo].[EXCELL_IMPORT_DATA_NEW] \r\n\r\nwhere CONVERT(DATETIME,CONCAT(CONCAT(SUBSTRING(ALIS_TARIHI,7,4),'-',SUBSTRING(ALIS_TARIHI,4,2)),'-',SUBSTRING(ALIS_TARIHI,1,2)) ,102)>GETDATE()\r\n ";

            SqlCommand commandqaimet = new SqlCommand(queryqaimet, cont);


            cont.Open();
            SqlDataReader drqaimet = commandqaimet.ExecuteReader();

            while (drqaimet.Read())
            {
                string ALIS_TARIHIK = drqaimet["TARIHKONTROL"].ToString();




                if (ALIS_TARIHIK != "0")
                {
                    Helpers.Messages.ReadyMessages.WARNING_DEFAULT_MESSAGE($"Məhsul alış tarixi bugünün({DateTime.Now.ToString("dd.MM.yyyy")}) tarixindən böyük olabilməz");
                    // MessageBox.Show("Yeni qaime hata tarih bugunden buyuktur. Lutfen gerekli duzenlemeyi yapiniz?");
                    goto closethis;
                }



            }
            cont.Close();






            string queryqaime = "SELECT  [ALIS_TARIHI] ,[FAKTURA_NO],[TECHIZATCI_ADI],(SELECT TOP 1 [TECHIZATCI_ID]    FROM  [COMPANY].[TECHIZATCI] WHERE SIRKET_ADI=[EXCELL_IMPORT_DATA_NEW].[TECHIZATCI_ADI]) AS TID   FROM [dbo].[EXCELL_IMPORT_DATA_NEW]  group by ALIS_TARIHI,FAKTURA_NO,TECHIZATCI_ADI";

            SqlCommand commandqaime = new SqlCommand(queryqaime, cont);


            cont.Open();
            SqlDataReader drqaime = commandqaime.ExecuteReader();

            while (drqaime.Read())
            {
                string ALIS_TARIHI = drqaime["ALIS_TARIHI"].ToString();
                string FAKTURA_NO = drqaime["FAKTURA_NO"].ToString();
                string TECHIZATCI_ADI = drqaime["TECHIZATCI_ADI"].ToString();
                string TECHIZATCI_ID = drqaime["TID"].ToString();


                string mydate = ALIS_TARIHI.Substring(6, 4) + "-" + ALIS_TARIHI.Substring(3, 2) + "-" + ALIS_TARIHI.Substring(0, 2);



                System.DateTime tarihkontrol = Convert.ToDateTime(mydate);

                if (tarihkontrol > System.DateTime.Today)
                {
                    //MessageBox.Show("Yeni qaime hata tarih bugunden buyuktur. Lutfen gerekli duzenlemeyi yapiniz?");
                    ReadyMessages.WARNING_DEFAULT_MESSAGE($"Məhsul alış tarixi bugünün({DateTime.Now.ToString("dd.MM.yyyy")}) tarixindən böyük olabilməz");

                }

                string querydetailkayit = "INSERT INTO [MAL_ALISI_DETAILS] ([MAL_ALISI_MAIN_ID]\r\n           ,[KATEGORIYA]\r\n           ,[BARKOD]\r\n           ,[MEHSUL_ADI]\r\n           ,[MEHSUL_KODU]\r\n           ,[ANBAR_ID]\r\n           ,[MIGDARI]\r\n           ,[VAHID]\r\n           ,[VALYUTA]\r\n           ,[VERGI_DERECESI]\r\n           ,[SATIS_GIYMETI]\r\n           ,[ALIS_GIYMETI]\r\n           \r\n           ,[DiscountDate]\r\n ,[ISTEHSAL_TARIXI],[BITIS_TARIXI],[GEYD]         ) SELECT  \r\n\r\n(SELECT MAX(MAL_ALISI_MAIN_ID) FROM MAL_ALISI_MAIN)\r\n,(SELECT KATEGORIYA_ID FROM KATEGORIYA WHERE [KATEGORIYA]=[EXCELL_IMPORT_DATA_NEW].KATEGORIYA)\r\n   ,CASE WHEN [BARKOD] IS NULL THEN (SELECT TOP 1 BARKOD FROM MAL_ALISI_DETAILS WHERE MEHSUL_ADI=[EXCELL_IMPORT_DATA_NEW].MEHSUL_ADI) \r\n   \r\n    WHEN BARKOD<>(SELECT TOP 1 BARKOD FROM MAL_ALISI_DETAILS WHERE MEHSUL_ADI=[EXCELL_IMPORT_DATA_NEW].MEHSUL_ADI) THEN (SELECT TOP 1 BARKOD FROM MAL_ALISI_DETAILS WHERE MEHSUL_ADI=[EXCELL_IMPORT_DATA_NEW].MEHSUL_ADI)    \r\n  \r\n   \r\n   ELSE BARKOD END\r\n     ,CASE WHEN [MEHSUL_ADI] IS NULL THEN  (SELECT TOP 1 MEHSUL_ADI FROM MAL_ALISI_DETAILS WHERE BARKOD=[EXCELL_IMPORT_DATA_NEW].BARKOD) ELSE [MEHSUL_ADI] END\r\n ,CASE WHEN \r\n (CASE WHEN [MEHSUL_KODU] IS NULL THEN (SELECT TOP 1 [MEHSUL_KODU] FROM MAL_ALISI_DETAILS WHERE BARKOD=[EXCELL_IMPORT_DATA_NEW].BARKOD) WHEN MEHSUL_KODU<>(SELECT TOP 1 [MEHSUL_KODU] FROM MAL_ALISI_DETAILS WHERE BARKOD=[EXCELL_IMPORT_DATA_NEW].BARKOD) THEN (SELECT TOP 1 [MEHSUL_KODU] FROM MAL_ALISI_DETAILS WHERE BARKOD=[EXCELL_IMPORT_DATA_NEW].BARKOD) ELSE MEHSUL_KODU END ) IS NULL \r\n \r\n THEN (CASE WHEN [MEHSUL_KODU] IS NULL THEN (SELECT TOP 1 [MEHSUL_KODU] FROM MAL_ALISI_DETAILS WHERE MEHSUL_ADI=[EXCELL_IMPORT_DATA_NEW].MEHSUL_ADI) ELSE MEHSUL_KODU END )  \r\n \r\n \r\n WHEN MEHSUL_KODU<>(SELECT TOP 1 [MEHSUL_KODU] FROM MAL_ALISI_DETAILS WHERE MEHSUL_ADI=[EXCELL_IMPORT_DATA_NEW].MEHSUL_ADI) THEN (SELECT TOP 1 [MEHSUL_KODU] FROM MAL_ALISI_DETAILS WHERE MEHSUL_ADI=[EXCELL_IMPORT_DATA_NEW].MEHSUL_ADI)    \r\n  \r\n  ELSE MEHSUL_KODU\r\n\r\n  END\r\n\t   ,4\r\n\t     ,cast([MEHSULUN_MIGDARI] as decimal(9,2))\r\n\t\t ,CAST([VAHIDI] AS int)\r\n\t\t ,1\r\n\t\t   ,cast([EDV] as int)\r\n\t\t    ,cast(replace([SATINALMA_GIYMETI],',','.') as decimal(9,2))\r\n ,cast(replace([SATIS_GIYMETI],',','.') as decimal(9,2))\r\n     , GETDATE(),CONVERT(DATETIME, [ISTEHSAL_TARIHI], 104) ,CONVERT (DATETIME,[SONISTIFADE_TARIHI],104),  TESVIR    FROM [EXCELL_IMPORT_DATA_NEW]\r\n\r\n  WHERE  [KONTROL]=0\r\n\r\n  AND ALIS_TARIHI='" + ALIS_TARIHI + "'  AND FAKTURA_NO=N'" + FAKTURA_NO + "' AND TECHIZATCI_ADI=N'" + TECHIZATCI_ADI + "'";
                string querydetailkayit2 = "INSERT INTO [MAL_ALISI_DETAILS] ([MAL_ALISI_MAIN_ID]\r\n           ,[KATEGORIYA]\r\n           ,[BARKOD]\r\n           ,[MEHSUL_ADI]\r\n           ,[MEHSUL_KODU]\r\n           ,[ANBAR_ID]\r\n           ,[MIGDARI]\r\n           ,[VAHID]\r\n           ,[VALYUTA]\r\n           ,[VERGI_DERECESI]\r\n           ,[SATIS_GIYMETI]\r\n           ,[ALIS_GIYMETI]\r\n           \r\n           ,[DiscountDate]\r\n   ,[ISTEHSAL_TARIXI],[BITIS_TARIXI],[GEYD]       ) SELECT  \r\n\r\n(SELECT MAX(MAL_ALISI_MAIN_ID) FROM MAL_ALISI_MAIN)\r\n,(SELECT KATEGORIYA_ID FROM KATEGORIYA WHERE [KATEGORIYA]=[EXCELL_IMPORT_DATA_NEW].KATEGORIYA)\r\n, CASE WHEN [BARKOD] IS NULL THEN  CAST(CONVERT(int, RAND() * 100000) AS nvarchar)  ELSE BARKOD END\r\n ,CASE WHEN [MEHSUL_ADI] IS NULL THEN  (SELECT TOP 1 MEHSUL_ADI FROM MAL_ALISI_DETAILS WHERE BARKOD=[EXCELL_IMPORT_DATA_NEW].BARKOD) ELSE [MEHSUL_ADI] END\r\n ,CASE WHEN [MEHSUL_KODU] IS NULL THEN  CAST(CONVERT(INT, RAND() * 10000) AS   varchar(20))  ELSE MEHSUL_KODU END\r\n\t   ,4\r\n\t     ,cast([MEHSULUN_MIGDARI] as decimal(9,2))\r\n\t\t ,cast([VAHIDI] as int)\r\n\t\t ,1\r\n\t\t   ,cast([EDV] as int)\r\n\t\t    ,cast(replace([SATINALMA_GIYMETI],',','.') as decimal(9,2))\r\n ,cast(replace([SATIS_GIYMETI],',','.') as decimal(9,2))\r\n      ,GETDATE(), CONVERT(DATETIME, [ISTEHSAL_TARIHI], 104) ,CONVERT (DATETIME,[SONISTIFADE_TARIHI],104),  TESVIR   FROM [EXCELL_IMPORT_DATA_NEW]\r\n\r\n  WHERE  [KONTROL]=1  AND ALIS_TARIHI='" + ALIS_TARIHI + "'  AND FAKTURA_NO=N'" + FAKTURA_NO + "' AND TECHIZATCI_ADI=N'" + TECHIZATCI_ADI + "'";
                string queryambarmagazakontrol = "\r\ndelete from ANBAR_MAGAZA\r\n\r\n   INSERT INTO ANBAR_MAGAZA(ANBAR_ID,MAGAZA_ID,TARIX,EMELIYYAT_NOMRE,mal_details_id,migdar)\r\n\t\tselect 4, 1002,getdate(),1,MAL_ALISI_DETAILS_ID,MIGDARI from MAL_ALISI_DETAILS ";

                int supplierId = Convert.ToInt32(TECHIZATCI_ID);
                string proccessNo = DbProsedures.GET_ProductProcessNo();
                int addMainProduct = DbProsedures.InsertImportProductMain(new ProductsMain
                {
                    FakturaNo = FAKTURA_NO,
                    SupplierId = supplierId,
                    Date = tarihkontrol,
                    PaymentType = "0",
                    ProccessNo = proccessNo,
                    Status = "MƏHSUL ALIŞI"
                });

                con.Open();
                SqlCommand commandqaimeDETAILsave = new SqlCommand(querydetailkayit, con);

                commandqaimeDETAILsave.ExecuteNonQuery();
                con.Close();


                con.Open();
                SqlCommand commandqaimeDETAILsave2 = new SqlCommand(querydetailkayit2, con);

                commandqaimeDETAILsave2.ExecuteNonQuery();
                con.Close();


                con.Open();
                SqlCommand commandambarmagazakontrol = new SqlCommand(queryambarmagazakontrol, con);

                commandambarmagazakontrol.ExecuteNonQuery();
                con.Close();
            }





            cont.Close();

            ReadyMessages.SUCCESS_DEFAULT_MESSAGE("Məhsullar sistemə uğurla əlavə edildi");
            FormHelpers.Log("Yeni məhsullar Excel import ilə sistemə daxil edildi");
            DialogResult = DialogResult.OK;

            goto closethis;



        closethis:
            Close();

        }

        public void kontrollu()
        {


        Excelimport:
            string tezhicatci = null, kategori = null, malzeme = null, countstring = null, barkod = null, mkod = null, id = null, fatno = null, fattarih = null;
            int count = 0;
            //delete data
            tr.DELETE_import_exc();
            //import data 
            // Getting data from DataGridView
            System.Data.DataTable myDt = new System.Data.DataTable();
            myDt = GetDTfromDGV(dataGridView1);

            // Writing to sql
            WriteToSQL(myDt);

            // tr.bulk_import_exc();




            // Tezhizatci Acilmasi

            SqlConnection con = new SqlConnection();
            SqlConnection cont = new SqlConnection();

            con.ConnectionString = Properties.Settings.Default.SqlCon;
            cont.ConnectionString = Properties.Settings.Default.SqlCon;

            string query = "SELECT count(*) AS COUNTS,TECHIZATCI_ADI \r\n  FROM [EXCELL_IMPORT_DATA_NEW]\r\n\r\n \r\n  WHERE TECHIZATCI_ADI NOT IN\r\n  (\r\n  SELECT  [SIRKET_ADI]\r\n    \r\n  FROM [NewIntekobir].[COMPANY].[TECHIZATCI] WHERE IsDeleted=0\r\n  )\r\n  group by TECHIZATCI_ADI";

            SqlCommand command = new SqlCommand(query, con);

            con.Open();
            SqlDataReader dr = command.ExecuteReader();

            while (dr.Read())
            {
                tezhicatci = dr["TECHIZATCI_ADI"].ToString();
                countstring = dr["COUNTS"].ToString();


                if (Convert.ToInt32(countstring) > 0)
                {


                    DialogResult result1 = MessageBox.Show($"{tezhicatci} təchizatçısı yaradılsın ?", "Yeni Təchizatçı", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {




                        string query2 = "insert into COMPANY.TECHIZATCI (TECHIZATCI_NOMRE,SIRKET_ADI )    SELECT case when ('T-'+ RTRIM( LTRIM(CAST(MAX(CAST(REPLACE(TECHIZATCI_NOMRE,'T-','')  \r\n   AS INT)+1) AS NCHAR(10)))) ) is null then 'T-1' ELSE\r\n   \r\n   ('T-'+ RTRIM( LTRIM(CAST(MAX(CAST(REPLACE(TECHIZATCI_NOMRE,'T-','')  \r\n   AS INT)+1) AS NCHAR(10)))) )\r\n    END as col,@techizatci FROM COMPANY.TECHIZATCI";

                        SqlCommand command2 = new SqlCommand(query2, cont);

                        command2.Parameters.AddWithValue("@techizatci", tezhicatci);
                        cont.Open();
                        command2.ExecuteNonQuery();
                        cont.Close();
                    }
                    else
                    {
                        goto closethis;
                    }



                }
            }

            con.Close();




            // Kategori Acilmasi



            con.ConnectionString = Properties.Settings.Default.SqlCon;
            cont.ConnectionString = Properties.Settings.Default.SqlCon;

            string querykategori = "SELECT count(*) AS COUNTS,KATEGORIYA FROM [EXCELL_IMPORT_DATA_NEW] WHERE KATEGORIYA NOT IN\r\n  (\r\n  SELECT KATEGORIYA FROM KATEGORIYA\r\n  )\r\n  GROUP BY KATEGORIYA";

            SqlCommand commandkategori = new SqlCommand(querykategori, con);

            con.Open();
            SqlDataReader drkategori = commandkategori.ExecuteReader();

            while (drkategori.Read())
            {
                kategori = drkategori["KATEGORIYA"].ToString();
                countstring = drkategori["COUNTS"].ToString();


                if (Convert.ToInt32(countstring) > 0)
                {


                    DialogResult resultkategori = MessageBox.Show($"Yeni {kategori} kateqoriyası yaradılsın ?", "Yeni Kateqoriya", MessageBoxButtons.YesNo);
                    if (resultkategori == DialogResult.Yes)
                    {




                        string querykategori2 = "INSERT INTO [dbo].[KATEGORIYA] ([KATEGORIYA])  VALUES (@kategoriya)";

                        SqlCommand command2 = new SqlCommand(querykategori2, cont);

                        command2.Parameters.AddWithValue("@kategoriya", kategori);
                        cont.Open();
                        command2.ExecuteNonQuery();
                        cont.Close();
                    }
                    else
                    {
                        goto closethis;
                    }



                }
            }

            con.Close();




            // Fatura No  



            con.ConnectionString = Properties.Settings.Default.SqlCon;
            cont.ConnectionString = Properties.Settings.Default.SqlCon;

            string queryfat = "SELECT COUNT(*) AS COUNTS FROM [EXCELL_IMPORT_DATA_NEW] WHERE\r\n\r\n(LEN(FAKTURA_NO)<2 OR LEN(FAKTURA_NO) IS NULL)";

            SqlCommand commandfat = new SqlCommand(queryfat, con);

            con.Open();
            SqlDataReader drfat = commandfat.ExecuteReader();

            while (drfat.Read())
            {

                countstring = drfat["COUNTS"].ToString();


                if (Convert.ToInt32(countstring) > 0)
                {


                    DialogResult resultkategori = MessageBox.Show("Faktura nömrəsi boş olaraq daxil edilən məhsullar mövcuddur. Faktura nömrəsini qeyd etmədən alış fakturası yaratmağa əminsiniz ?",
"Məhsul alışı - Excel", MessageBoxButtons.YesNo);
                    if (resultkategori == DialogResult.Yes)
                    {

                        MessageBox.Show("Yeni alış fakturası olaraq açılacaqdır");
                    }
                    else
                    {
                        goto closethis;
                    }



                }
            }

            con.Close();






            //  Malzeme Acilmasi



            con.ConnectionString = Properties.Settings.Default.SqlCon;
            cont.ConnectionString = Properties.Settings.Default.SqlCon;

            string querymalzeme = "SELECT *   ,\r\n\r\n CASE WHEN \r\n \r\n ( SELECT TOP 1 MEHSUL_ADI   FROM [MAL_ALISI_DETAILS] WHERE BARKOD=[EXCELL_IMPORT_DATA_NEW].BARKOD)=MEHSUL_ADI   \r\n THEN 1 ELSE    \r\n \r\n (   CASE WHEN    ( SELECT  TOP 1 MEHSUL_ADI   FROM [MAL_ALISI_DETAILS] WHERE MEHSUL_KODU=[EXCELL_IMPORT_DATA_NEW].MEHSUL_KODU)=[EXCELL_IMPORT_DATA_NEW].MEHSUL_ADI    \r\n \r\n   THEN  1 else 0 end)   \r\n \r\n  END AS [KNTBARKODSTOK] ,\r\n  \r\n\r\n  CASE WHEN \r\n \r\n ( SELECT TOP 1 MEHSUL_ADI   FROM [MAL_ALISI_DETAILS] WHERE MEHSUL_ADI=[EXCELL_IMPORT_DATA_NEW].MEHSUL_ADI)=MEHSUL_ADI   \r\n THEN 1 ELSE    \r\n 0 end MALZEMEADI\r\n  \r\n    \r\n   FROM [EXCELL_IMPORT_DATA_NEW] ";

            SqlCommand commandmalzeme = new SqlCommand(querymalzeme, con);

            con.Open();
            SqlDataReader drmalzeme = commandmalzeme.ExecuteReader();

            while (drmalzeme.Read())
            {
                malzeme = drmalzeme["MEHSUL_ADI"].ToString();
                mkod = drmalzeme["MEHSUL_KODU"].ToString();
                barkod = drmalzeme["BARKOD"].ToString();
                id = drmalzeme["ID"].ToString();
                string kontrolkod = drmalzeme["KNTBARKODSTOK"].ToString();
                int kontrolkodbarkod = Int32.Parse(kontrolkod);
                string kontrolad = drmalzeme["MALZEMEADI"].ToString();
                int kontrolmalzemead = Int32.Parse(kontrolad);
                if (kontrolkodbarkod == 0 && kontrolmalzemead == 1)
                {

                    string message = $@"Bu məhsul adında ({malzeme}) sistemdə fərqli barkod və məhsul kodunda məhsulunuz mövcuddur. Yenisini yaratmağa əminsiniz ?

Bəli - Fərqli kodda {malzeme} məhsulu yaradılacaq
Xeyr - Əvvəlki barkod ilə {malzeme} məhsulu yaradılacaq";

                    DialogResult resultmalzeme = MessageBox.Show(message, "Mpos", MessageBoxButtons.YesNo);
                    if (resultmalzeme == DialogResult.Yes)
                    {

                        string querymalzeme2 = "UPDATE  EXCELL_IMPORT_DATA_NEW  set KONTROL=1 where ID= (@id)";

                        SqlCommand command3 = new SqlCommand(querymalzeme2, cont);

                        command3.Parameters.AddWithValue("@id", id);
                        cont.Open();
                        command3.ExecuteNonQuery();
                        cont.Close();
                    }
                    else
                    {

                    }



                }

                else if (kontrolkodbarkod == 1 && kontrolmalzemead == 0)
                {


                    DialogResult resultmalzeme = MessageBox.Show($"Bu məhsul kodu və barkodu ilə sistemdə {malzeme} məhsulu mövcuddur. Yenisini yaratmağa əminsiniz ?",
"Yeni Malzeme", MessageBoxButtons.YesNo);
                    if (resultmalzeme == DialogResult.Yes)
                    {

                        string querymalzeme2 = "UPDATE  EXCELL_IMPORT_DATA_NEW  set KONTROL=1 where ID= (@id)";

                        SqlCommand command3 = new SqlCommand(querymalzeme2, cont);

                        command3.Parameters.AddWithValue("@id", id);
                        cont.Open();
                        command3.ExecuteNonQuery();
                        cont.Close();
                    }
                    else
                    {

                    }



                }

                else if (kontrolkodbarkod == 1 && kontrolmalzemead == 1)
                {

                }

                else
                {

                    DialogResult resultmalzeme = MessageBox.Show($"Yeni {malzeme} məhsulunu yaratmağa əminsiniz ?",
"Yeni məhsul", MessageBoxButtons.YesNo);
                    if (resultmalzeme == DialogResult.Yes)
                    {

                        string querymalzeme2 = "UPDATE  EXCELL_IMPORT_DATA_NEW  set KONTROL=1 where ID= (@id)";

                        SqlCommand command3 = new SqlCommand(querymalzeme2, cont);

                        command3.Parameters.AddWithValue("@id", id);
                        cont.Open();
                        command3.ExecuteNonQuery();
                        cont.Close();
                    }
                    else
                    {

                    }

                }
            }

            con.Close();




            string queryqaimet = "SELECT  COUNT(*) TARIHKONTROL FROM [dbo].[EXCELL_IMPORT_DATA_NEW] \r\n\r\nwhere CONVERT(DATETIME,CONCAT(CONCAT(SUBSTRING(ALIS_TARIHI,7,4),'-',SUBSTRING(ALIS_TARIHI,4,2)),'-',SUBSTRING(ALIS_TARIHI,1,2)) ,102)>GETDATE()\r\n ";

            SqlCommand commandqaimet = new SqlCommand(queryqaimet, cont);


            cont.Open();
            SqlDataReader drqaimet = commandqaimet.ExecuteReader();

            while (drqaimet.Read())
            {
                string ALIS_TARIHIK = drqaimet["TARIHKONTROL"].ToString();




                if (ALIS_TARIHIK != "0")
                {
                    //MessageBox.Show("Yeni qaime hata tarih bugunden buyuktur. Lutfen gerekli duzenlemeyi yapiniz?");
                    ReadyMessages.WARNING_DEFAULT_MESSAGE($"Məhsul alış tarixi bugünün({DateTime.Now.ToString("dd.MM.yyyy")}) tarixindən böyük olabilməz");
                    goto closethis;
                }



            }
            cont.Close();


            DialogResult resultqaime = MessageBox.Show("Alış fakturası sistemə daxil edilsin ?",
"Yeni Qaime", MessageBoxButtons.YesNo);
            if (resultqaime == DialogResult.Yes)
            {




                string queryqaime = "SELECT  [ALIS_TARIHI] ,[FAKTURA_NO],[TECHIZATCI_ADI],(SELECT TOP 1 [TECHIZATCI_ID]    FROM  [COMPANY].[TECHIZATCI] WHERE SIRKET_ADI=[EXCELL_IMPORT_DATA_NEW].[TECHIZATCI_ADI]) AS TID  FROM [dbo].[EXCELL_IMPORT_DATA_NEW]  group by ALIS_TARIHI,FAKTURA_NO,TECHIZATCI_ADI";

                SqlCommand commandqaime = new SqlCommand(queryqaime, cont);


                cont.Open();
                SqlDataReader drqaime = commandqaime.ExecuteReader();

                while (drqaime.Read())
                {
                    string ALIS_TARIHI = drqaime["ALIS_TARIHI"].ToString();
                    string FAKTURA_NO = drqaime["FAKTURA_NO"].ToString();
                    string TECHIZATCI_ADI = drqaime["TECHIZATCI_ADI"].ToString();
                    string TECHIZATCI_ID = drqaime["TID"].ToString();


                    string mydate = ALIS_TARIHI.Substring(6, 4) + "-" + ALIS_TARIHI.Substring(3, 2) + "-" + ALIS_TARIHI.Substring(0, 2);



                    System.DateTime tarihkontrol = Convert.ToDateTime(mydate);

                    if (tarihkontrol > System.DateTime.Today)
                    {
                        //MessageBox.Show("Yeni qaime hata tarih bugunden buyuktur. Lutfen gerekli duzenlemeyi yapiniz?");
                        ReadyMessages.WARNING_DEFAULT_MESSAGE($"Məhsul alış tarixi bugünün({DateTime.Now.ToString("dd.MM.yyyy")}) tarixindən böyük olabilməz");

                    }

                    //string querqayit = "  INSERT INTO [MAL_ALISI_MAIN]  SELECT TOP 1  '" + FAKTURA_NO + "'," + TECHIZATCI_ID + ", '" + mydate + "' ,0 ,( SELECT CASE WHEN  ('MA-'+ RTRIM( LTRIM(CAST(MAX(CAST(REPLACE(EMELIYYAT_NOMRE,'MA-','')   AS INT)+1) AS NCHAR(10)))) )   IS NULL THEN 'MA-1'  ELSE   ('MA-'+ RTRIM( LTRIM(CAST(MAX(CAST(REPLACE(EMELIYYAT_NOMRE,'MA-','')    AS INT)+1) AS NCHAR(10)))) ) END   as col FROM MAL_ALISI_MAIN)  ,CAST(GETDATE() AS date)  ," + DbProsedures.GetUser().Id + " ,N'MƏHSUL ALIŞI'  FROM [userParol]";
                    string querydetailkayit = "INSERT INTO [MAL_ALISI_DETAILS] ([MAL_ALISI_MAIN_ID]\r\n           ,[KATEGORIYA]\r\n           ,[BARKOD]\r\n           ,[MEHSUL_ADI]\r\n           ,[MEHSUL_KODU]\r\n           ,[ANBAR_ID]\r\n           ,[MIGDARI]\r\n           ,[VAHID]\r\n           ,[VALYUTA]\r\n           ,[VERGI_DERECESI]\r\n           ,[SATIS_GIYMETI]\r\n           ,[ALIS_GIYMETI]\r\n           \r\n           ,[DiscountDate]\r\n ,[ISTEHSAL_TARIXI],[BITIS_TARIXI],[GEYD]         ) SELECT  \r\n\r\n(SELECT MAX(MAL_ALISI_MAIN_ID) FROM MAL_ALISI_MAIN)\r\n,(SELECT KATEGORIYA_ID FROM KATEGORIYA WHERE [KATEGORIYA]=[EXCELL_IMPORT_DATA_NEW].KATEGORIYA)\r\n   ,CASE WHEN [BARKOD] IS NULL THEN (SELECT TOP 1 BARKOD FROM MAL_ALISI_DETAILS WHERE MEHSUL_ADI=[EXCELL_IMPORT_DATA_NEW].MEHSUL_ADI) \r\n   \r\n    WHEN BARKOD<>(SELECT TOP 1 BARKOD FROM MAL_ALISI_DETAILS WHERE MEHSUL_ADI=[EXCELL_IMPORT_DATA_NEW].MEHSUL_ADI) THEN (SELECT TOP 1 BARKOD FROM MAL_ALISI_DETAILS WHERE MEHSUL_ADI=[EXCELL_IMPORT_DATA_NEW].MEHSUL_ADI)    \r\n  \r\n   \r\n   ELSE BARKOD END\r\n     ,CASE WHEN [MEHSUL_ADI] IS NULL THEN  (SELECT TOP 1 MEHSUL_ADI FROM MAL_ALISI_DETAILS WHERE BARKOD=[EXCELL_IMPORT_DATA_NEW].BARKOD) ELSE [MEHSUL_ADI] END\r\n ,CASE WHEN \r\n (CASE WHEN [MEHSUL_KODU] IS NULL THEN (SELECT TOP 1 [MEHSUL_KODU] FROM MAL_ALISI_DETAILS WHERE BARKOD=[EXCELL_IMPORT_DATA_NEW].BARKOD) WHEN MEHSUL_KODU<>(SELECT TOP 1 [MEHSUL_KODU] FROM MAL_ALISI_DETAILS WHERE BARKOD=[EXCELL_IMPORT_DATA_NEW].BARKOD) THEN (SELECT TOP 1 [MEHSUL_KODU] FROM MAL_ALISI_DETAILS WHERE BARKOD=[EXCELL_IMPORT_DATA_NEW].BARKOD) ELSE MEHSUL_KODU END ) IS NULL \r\n \r\n THEN (CASE WHEN [MEHSUL_KODU] IS NULL THEN (SELECT TOP 1 [MEHSUL_KODU] FROM MAL_ALISI_DETAILS WHERE MEHSUL_ADI=[EXCELL_IMPORT_DATA_NEW].MEHSUL_ADI) ELSE MEHSUL_KODU END )  \r\n \r\n \r\n WHEN MEHSUL_KODU<>(SELECT TOP 1 [MEHSUL_KODU] FROM MAL_ALISI_DETAILS WHERE MEHSUL_ADI=[EXCELL_IMPORT_DATA_NEW].MEHSUL_ADI) THEN (SELECT TOP 1 [MEHSUL_KODU] FROM MAL_ALISI_DETAILS WHERE MEHSUL_ADI=[EXCELL_IMPORT_DATA_NEW].MEHSUL_ADI)    \r\n  \r\n  ELSE MEHSUL_KODU\r\n\r\n  END\r\n\t   ,4\r\n\t     ,cast([MEHSULUN_MIGDARI] as decimal(9,2))\r\n\t\t ,CAST([VAHIDI] AS int)\r\n\t\t ,1\r\n\t\t   ,cast([EDV] as int)\r\n\t\t    ,cast(replace([SATINALMA_GIYMETI],',','.') as decimal(9,2))\r\n ,cast(replace([SATIS_GIYMETI],',','.') as decimal(9,2))\r\n     , GETDATE(),CONVERT(DATETIME, [ISTEHSAL_TARIHI], 104) ,CONVERT (DATETIME,[SONISTIFADE_TARIHI],104),  TESVIR   FROM [EXCELL_IMPORT_DATA_NEW]\r\n\r\n  WHERE  [KONTROL]=0\r\n\r\n  AND ALIS_TARIHI='" + ALIS_TARIHI + "'  AND FAKTURA_NO=N'" + FAKTURA_NO + "' AND TECHIZATCI_ADI=N'" + TECHIZATCI_ADI + "'";
                    string querydetailkayit2 = "INSERT INTO [MAL_ALISI_DETAILS] ([MAL_ALISI_MAIN_ID]\r\n           ,[KATEGORIYA]\r\n           ,[BARKOD]\r\n           ,[MEHSUL_ADI]\r\n           ,[MEHSUL_KODU]\r\n           ,[ANBAR_ID]\r\n           ,[MIGDARI]\r\n           ,[VAHID]\r\n           ,[VALYUTA]\r\n           ,[VERGI_DERECESI]\r\n           ,[SATIS_GIYMETI]\r\n           ,[ALIS_GIYMETI]\r\n           \r\n           ,[DiscountDate]\r\n   ,[ISTEHSAL_TARIXI],[BITIS_TARIXI],[GEYD]       ) SELECT  \r\n\r\n(SELECT MAX(MAL_ALISI_MAIN_ID) FROM MAL_ALISI_MAIN)\r\n,(SELECT KATEGORIYA_ID FROM KATEGORIYA WHERE [KATEGORIYA]=[EXCELL_IMPORT_DATA_NEW].KATEGORIYA)\r\n, CASE WHEN [BARKOD] IS NULL THEN  CAST(CONVERT(int, RAND() * 10000) AS nvarchar)  ELSE BARKOD END\r\n ,CASE WHEN [MEHSUL_ADI] IS NULL THEN  (SELECT TOP 1 MEHSUL_ADI FROM MAL_ALISI_DETAILS WHERE BARKOD=[EXCELL_IMPORT_DATA_NEW].BARKOD) ELSE [MEHSUL_ADI] END\r\n ,CASE WHEN [MEHSUL_KODU] IS NULL THEN  CAST(CONVERT(INT, RAND() * 10000)  AS varchar(20))  ELSE MEHSUL_KODU END\r\n\t   ,4\r\n\t     ,cast([MEHSULUN_MIGDARI] as decimal(9,2))\r\n\t\t ,cast([VAHIDI] as int)\r\n\t\t ,1\r\n\t\t   ,cast([EDV] as int)\r\n\t\t    ,cast(replace([SATINALMA_GIYMETI],',','.') as decimal(9,2))\r\n ,cast(replace([SATIS_GIYMETI],',','.') as decimal(9,2))\r\n      ,GETDATE(),CONVERT(DATETIME, [ISTEHSAL_TARIHI], 104) ,CONVERT (DATETIME,[SONISTIFADE_TARIHI],104),  TESVIR   FROM [EXCELL_IMPORT_DATA_NEW]\r\n\r\n  WHERE  [KONTROL]=1  AND ALIS_TARIHI='" + ALIS_TARIHI + "'  AND FAKTURA_NO=N'" + FAKTURA_NO + "' AND TECHIZATCI_ADI=N'" + TECHIZATCI_ADI + "'";
                    string queryambarmagazakontrol = "\r\ndelete from ANBAR_MAGAZA\r\n\r\n   INSERT INTO ANBAR_MAGAZA(ANBAR_ID,MAGAZA_ID,TARIX,EMELIYYAT_NOMRE,mal_details_id,migdar)\r\n\t\tselect 4, 1002,getdate(),1,MAL_ALISI_DETAILS_ID,MIGDARI from MAL_ALISI_DETAILS ";

                    int supplierId = Convert.ToInt32(TECHIZATCI_ID);
                    string proccessNo = DbProsedures.GET_ProductProcessNo();
                    int addMainProduct = DbProsedures.InsertImportProductMain(new ProductsMain
                    {
                        FakturaNo = FAKTURA_NO,
                        SupplierId = supplierId,
                        Date = tarihkontrol,
                        PaymentType = "0",
                        ProccessNo = proccessNo,
                        Status = "MƏHSUL ALIŞI"
                    });





                    //SqlCommand commandqaimesave = new SqlCommand(querqayit, con);

                    //con.Open();
                    //commandqaimesave.ExecuteNonQuery();
                    //con.Close();


                    con.Open();
                    SqlCommand commandqaimeDETAILsave = new SqlCommand(querydetailkayit, con);

                    commandqaimeDETAILsave.ExecuteNonQuery();
                    con.Close();


                    con.Open();
                    SqlCommand commandqaimeDETAILsave2 = new SqlCommand(querydetailkayit2, con);

                    commandqaimeDETAILsave2.ExecuteNonQuery();
                    con.Close();


                    con.Open();
                    SqlCommand commandambarmagazakontrol = new SqlCommand(queryambarmagazakontrol, con);

                    commandambarmagazakontrol.ExecuteNonQuery();
                    con.Close();
                }





                cont.Close();


                ReadyMessages.SUCCESS_DEFAULT_MESSAGE("Məhsullar sistemə uğurla əlavə edildi");
                FormHelpers.Log("Yeni məhsullar Excel import ilə sistemə daxil edildi");
                DialogResult = DialogResult.OK;
                goto closethis;
            }
            else
            {
                ReadyMessages.WARNING_DEFAULT_MESSAGE("Məhsulların sistemə əlavəsi ləğv edildi");
                goto closethis;
            }

        closethis:
            Close();

        }

        private void EXCELL_IMPORT_Load(object sender, EventArgs e)
        {

            comboBox1.Size = new Size(160, 30);
        }
    }
}