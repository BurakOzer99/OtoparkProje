using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using Microsoft.SqlServer.Server;

namespace OTOPARK__ÖDEVİ
{
    public partial class frmAraçOtoparkYerleri : Form
    {
        public frmAraçOtoparkYerleri()
        {
            InitializeComponent();
        }
        SqlConnection baglantı = new SqlConnection("Data Source=DESKTOP-EKSE2T7;Initial Catalog=Araç_otopark;Integrated Security=True");
        private void frmAraçOtoparkYerleri_Load(object sender, EventArgs e)
        {
            
            BoşParkYerleri();
            DoluParkYerleri();
            baglantı.Open();
            SqlCommand komut = new SqlCommand("select *from araç_otopark_kaydı", baglantı);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                foreach (Control item in Controls)
                {
                    if (item is Button)
                    {
                        if (item.Text == read["parkyeri"].ToString())
                        {
                            item.Text=read["plaka"].ToString();
                        }
                    }
                }
            }
            baglantı.Close();
        }

        private void DoluParkYerleri()
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("select *from araçdurumu", baglantı);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                foreach (Control item in Controls)
                {
                    if (item is Button)
                    {
                        if (item.Text == read["parkyeri"].ToString() && read["durumu"].ToString() == "DOLU")
                        {
                            item.BackColor = Color.Red;
                                
                        }
                    }
                }
            }
            baglantı.Close();
        }

        private void BoşParkYerleri()
        {
            int sayac = 1;
            foreach (Control item in Controls)

            {
                if (item is Button)
                {
                    item.Text = "P-" + sayac;
                    item.Name = "P-" + sayac;
                    sayac++;
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
