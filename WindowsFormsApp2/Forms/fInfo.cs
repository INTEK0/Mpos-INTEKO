using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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

        private async void pictureEdit1_Click(object sender, EventArgs e)
        {
            var data = new
            {
                customerName = "Ali Aliyev",
                customerPhone = "+994501234567",
                gender = "Male",
                productName = "Kola",
                categoryName = "İçkilər",
                quantity = 2,
                barcode = "1234567890123",
                purchasePrice = 1.2,
                salePrice = 1.5,
                unitType = "ədəd",
                taxRate = 18,
                cashierName = "Admin"
            };

            var json = JsonSerializer.Serialize(data);

            var client = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:44377/api/Sale", content);

            var result = await response.Content.ReadAsStringAsync();
        }
    }
}