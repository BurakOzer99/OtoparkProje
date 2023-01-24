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
    public partial class frmAraçOtoparkÇıkışı : Form
    {
        public frmAraçOtoparkÇıkışı()
        {
            InitializeComponent();
        }
        SqlConnection baglantı = new SqlConnection("Data Source=DESKTOP-EKSE2T7;Initial Catalog=Araç_otopark;Integrated Security=True");
        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void frmAraçOtoparkÇıkışı_Load(object sender, EventArgs e)
        {
            DoluYerler();
            Plakalar();
            timer1.Enabled = true;

        }

        private void Plakalar()
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("select * from araç_otopark_kaydı", baglantı);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboPlaka.Items.Add(read["plaka"].ToString());
            }
            baglantı.Close();
        }

        private void DoluYerler()
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("select * from araçdurumu where durumu ='DOLU'", baglantı);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboParkYeri.Items.Add(read["parkyeri"].ToString());
            }
            baglantı.Close();
        }

        private void comboPlaka_SelectedIndexChanged(object sender, EventArgs e)
        {

            baglantı.Open();
            SqlCommand komut = new SqlCommand("select * from araç_otopark_kaydı where plaka='"+comboPlaka.SelectedItem+"'", baglantı);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtParkYeri.Text = read["parkyeri"].ToString();
            }
            baglantı.Close();
        }

        private void comboParkYeri_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("select * from araç_otopark_kaydı where parkyeri='" + comboParkYeri.SelectedItem + "'", baglantı);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtParkYeri2.Text = read["parkyeri"].ToString();
                txtTc.Text = read["tc"].ToString();
                txtAd.Text = read["ad"].ToString();
                txtSoyad.Text = read["Soyad"].ToString();
                txtMarka.Text = read["marka"].ToString();
                txtSeri.Text = read["seri"].ToString();
                txtPlaka.Text = read["plaka"].ToString();
                lblGelişTarihi.Text = read["tarih"].ToString();
            }
            baglantı.Close();
            DateTime geliş, çıkış;
            geliş = DateTime.Parse(lblGelişTarihi.Text);
            çıkış = DateTime.Parse(lblÇıkışTarihi.Text);
            TimeSpan fark;
            fark = çıkış - geliş;
            lblSüre.Text = fark.TotalHours.ToString("0.00");
            
           lblToplamTutar.Text = (double.Parse(lblSüre.Text) * 0.75).ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblÇıkışTarihi.Text = DateTime.Now.ToString();
        }

        private void txtParkYeri2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("delete araç_otopark_kaydı where plaka='" + txtPlaka.Text + "'", baglantı);
            komut.ExecuteNonQuery();
            SqlCommand komut2 = new SqlCommand("update araçdurumu set durumu='BOŞ' where parkyeri='" + txtParkYeri2.Text + "'", baglantı);
            komut2.ExecuteNonQuery();
            SqlCommand komut3 = new SqlCommand("insert into satiş(parkyeri,plaka,geliş_tarihi,çıkış_tarihi,süre,tutar) values(@parkyeri,@plaka,@geliş_tarihi,@çıkış_tarihi,@süre,@tutar)", baglantı);
            komut3.Parameters.AddWithValue("@parkyeri", txtParkYeri2.Text);
            komut3.Parameters.AddWithValue("@plaka", txtPlaka.Text);
            komut3.Parameters.AddWithValue("@geliş_tarihi", lblGelişTarihi.Text);
            komut3.Parameters.AddWithValue("@çıkış_tarihi", lblÇıkışTarihi.Text);
            komut3.Parameters.AddWithValue("@süre",double.Parse(lblSüre.Text));
            komut3.Parameters.AddWithValue("@tutar", double.Parse(lblToplamTutar.Text));
            komut3.ExecuteNonQuery();

            baglantı.Close();
            MessageBox.Show("Araç çıkışı yapıldı."); 
            foreach(Control item  in groupBox2.Controls)
            {
                if(item is TextBox)
                {
                    item.Text = "";
                    txtParkYeri.Text = "";
                    comboParkYeri.Text = "";
                    comboPlaka.Text = "";
                }

            }
            comboPlaka.Items.Clear();
            comboParkYeri.Items.Clear();
            DoluYerler();
            Plakalar();
        }
    }
}
