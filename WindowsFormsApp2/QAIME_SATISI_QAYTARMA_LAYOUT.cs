﻿using DevExpress.CodeParser;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2
{
    public partial class QAIME_SATISI_QAYTARMA_LAYOUT : DevExpress.XtraEditors.XtraForm
    {
        public int gg_user_id;
        public QAIME_SATISI_QAYTARMA_LAYOUT(int _use_id)
        {
            InitializeComponent();
            gg_user_id = _use_id;
            GridPanelText(gridView1);
            GridLocalizer.Active = new MyGridLocalizer();
        }

        private void QAIME_SATISI_QAYTARMA_LAYOUT_Load(object sender, EventArgs e)
        {
            gridControl1.TabStop = false;
            DateTime dateTime = DateTime.UtcNow.Date;

            dateEdit4.Text = dateTime.ToShortDateString();
            textEdit6.Text = DbProsedures.GET_GaimeRefundProccessNo();
        }



       
        CRUD_GAIME_SATISI CG = new CRUD_GAIME_SATISI();
        private void simpleButton6_Click(object sender, EventArgs e)
        {
            foreach (int i in gridView1.GetSelectedRows())
            {
                DataRow row = gridView1.GetDataRow(i);

                int B = Convert.ToInt32(row[0].ToString());
                if (B > 0)
                {
                    int x = CG.insert_gaime_satis_gaytarma_proc_(
                        row[0].ToString(),
                        row[7].ToString(),
                            textEdit6.Text,
                            Convert.ToDateTime(dateEdit4.Text),
                            memoEdit1.Text);

                    FormHelpers.Log($"{row[1]} nömrəli satış qaiməsində {row[7]} ədəd {row[4]} məhsulu geri qaytarıldı");

                }


            }

            gridControl1.DataSource = null;
            textEdit2.Text = "";
            textEdit5 = "";
            textEdit1.Text = "";
            textEdit6.Text = DbProsedures.GET_GaimeRefundProccessNo();
        }
        public static string textEdit5 = "";
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            FormHelpers.OpenForm<gaytarilan_siyahi>();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            musteri_gaytar MG = new musteri_gaytar(this);
            MG.Show();
        }
        public void data_(string musteri_adi_, string _emeliyyat_nomre_, string gaime_main_id_, string gaime_nomre_)
        {
            textEdit2.Text = musteri_adi_;
            textEdit1.Text = gaime_nomre_;
            textEdit5 = _emeliyyat_nomre_;
            GetallData_id_(_emeliyyat_nomre_);
        }

        public void GetallData_id_(string id_)
        {
            try
            {
                string queryString = $@"select gd.GAIME_SATISI_DETAILS_ID,
       gm.EMELIIYYAT_NOMRE AS N'SATIŞ ƏMƏLİYYAT №',
	   gm.TARIX AS N'TARİX',
       ct.SIRKET_ADI AS N'TƏCHİZATÇI ADI',
       md.MEHSUL_ADI AS N'MƏHSUL ADI',
       gd.SATIS_GIYMETI as 'BİR VAHİDİN QİYMƏTİ',
       gd.MIGDARI AS N'MİQDARI' ,
       0 AS N'QAYTARILACAQ MİQDAR',
	  gd.YEKUN_MEBLEG as N'YEKUN MƏBLƏĞ'
from GAIME_SATISI_MAIN gm
inner
 join GAIME_SATISI_DETAILS gd on gm.GAIME_SATISI_MAIN_ID = gd.GAIME_SATISI_MAIN_ID
left join (
             (select gaime_satis_details_id,
                     sum(isnull(migdar, 0.00)) migdar
              from gaime_satis_gaytarma group  by gaime_satis_details_id)) gdg on gdg.gaime_satis_details_id=gd.GAIME_SATISI_DETAILS_ID
inner join MAL_ALISI_DETAILS md on md.MAL_ALISI_DETAILS_ID = gd.MAL_DETAILS_ID
inner join MAL_ALISI_MAIN mm on mm.MAL_ALISI_MAIN_ID = md.MAL_ALISI_MAIN_ID
inner join COMPANY.TECHIZATCI ct on ct.TECHIZATCI_ID = mm.TECHIZATCI_ID
WHERE GM.EMELIIYYAT_NOMRE = '{id_}'
  and gd.MIGDARI -isnull(gdg.migdar, 0.00)>0";

                var data = DbProsedures.ConvertToDataTable(queryString);

                gridControl1.DataSource = data;
                gridView1.Columns[0].Visible = false;
                gridView1.OptionsSelection.MultiSelect = true;
                gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;

                decimal toplam = 0;
                for (int i = 0; i < gridView1.RowCount; i++)
                    toplam += Convert.ToDecimal(gridView1.GetRowCellValue(i, "YEKUN MƏBLƏĞ"));


                textEdit14.Text = toplam.ToString("C2");


            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(e.Message);
            }
        }
    }
}