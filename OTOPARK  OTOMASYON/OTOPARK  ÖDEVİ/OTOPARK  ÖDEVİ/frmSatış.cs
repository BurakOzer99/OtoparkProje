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

namespace OTOPARK__ÖDEVİ
{
    public partial class frmSatış : Form
    {
        public frmSatış()
        {
            InitializeComponent();
        }
        SqlConnection baglantı = new SqlConnection("Data Source=DESKTOP-EKSE2T7;Initial Catalog=Araç_otopark;Integrated Security=True");
        DataSet daset = new DataSet();
        private void frmSatış_Load(object sender, EventArgs e)
        {
            SatislariListele();
            Hesapla();
        }

        private void Hesapla()
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("select sum(tutar) from satiş", baglantı);
            label1.Text = "Toplam tutar=" + komut.ExecuteScalar() + "TL";
            baglantı.Close();
        }

        private void SatislariListele()
        {
            baglantı.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from satiş", baglantı);
            adtr.Fill(daset, "satiş");
            dataGridView1.DataSource = daset.Tables["satiş"];
            baglantı.Close();
        }
    }
}
