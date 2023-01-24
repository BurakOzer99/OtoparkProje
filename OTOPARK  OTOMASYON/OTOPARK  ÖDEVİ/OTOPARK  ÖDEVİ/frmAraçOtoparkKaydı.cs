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
    public partial class frmAraçOtoparkKaydı : Form
    {
        public frmAraçOtoparkKaydı()
        {
            InitializeComponent();
        }
        SqlConnection baglantı = new SqlConnection("Data Source=DESKTOP-EKSE2T7;Initial Catalog=Araç_otopark;Integrated Security=True");

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frmAraçOtoparkKaydı_Load(object sender, EventArgs e)
        {
            BoşAraçlar();
            Marka();
            
        }

        private void Marka()
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("select marka from marka_bilgileri ", baglantı);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboMarka.Items.Add(read["marka"].ToString());
            }
            baglantı.Close();
        }

        private void BoşAraçlar()
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("select *from araçdurumu WHERE durumu= 'BOŞ' ", baglantı);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboParkyeri.Items.Add(read["parkyeri"].ToString());
            }
            baglantı.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("insert into araç_otopark_kaydı(tc,ad,soyad,telefon,email,plaka,marka,seri,renk,parkyeri,tarih) values(@tc,@ad,@soyad,@telefon,@email,@plaka,@marka,@seri,@renk,@parkyeri,@tarih)", baglantı);
            komut.Parameters.AddWithValue("@tc", txtTc.Text);
            komut.Parameters.AddWithValue("@ad", txtAd.Text);
            komut.Parameters.AddWithValue("@soyad", txtSoyad.Text);
            komut.Parameters.AddWithValue("@telefon", txtTelefon.Text);
            komut.Parameters.AddWithValue("@email", txtEmail.Text);
            komut.Parameters.AddWithValue("@plaka", txtPlaka.Text);
            komut.Parameters.AddWithValue("@marka", comboMarka.Text);
            komut.Parameters.AddWithValue("@seri", comboSeri.Text);
            komut.Parameters.AddWithValue("@renk", txtRenk.Text);
            komut.Parameters.AddWithValue("@parkyeri", comboParkyeri.Text);
            komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());
            komut.ExecuteNonQuery();
            SqlCommand komut2 = new SqlCommand("Update araçdurumu set durumu='DOLU' where parkyeri='"+comboParkyeri.SelectedItem+ "' ", baglantı);
            baglantı.Close();
            MessageBox.Show("araç kaydı oluşturuldu", "KAYIT");
            comboParkyeri.Items.Clear();
            BoşAraçlar();
            comboMarka.Items.Clear();
            Marka();
            comboSeri.Items.Clear();
            foreach( Control item in grupKişi.Controls)
            {
                if( item is TextBox)
                {
                    item.Text = "";
                }
            }
            foreach (Control item in grupAraç.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
            foreach (Control item in grupAraç.Controls)
            {
                if (item is ComboBox)
                {
                    item.Text = "";
                }
            }
        }

        private void btnMarka_Click(object sender, EventArgs e)
        {
            frmMarka marka = new frmMarka();
            marka.ShowDialog();
        }

        private void btnSeri_Click(object sender, EventArgs e)
        {
            frmSeri Seri = new frmSeri();
            Seri.ShowDialog();
        }

        private void comboMarka_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboSeri.Items.Clear();
            baglantı.Open();
            SqlCommand komut = new SqlCommand("select marka,seri from seri_bilgileri where marka='"+comboMarka.SelectedItem+"' ", baglantı);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboSeri.Items.Add(read["seri"].ToString());
            }
            baglantı.Close();
        }
    }
}
