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
    public partial class frmSeri : Form
    {
        public frmSeri()
        {
            InitializeComponent();
        }
        SqlConnection baglantı = new SqlConnection("Data Source=DESKTOP-EKSE2T7;Initial Catalog=Araç_otopark;Integrated Security=True");
        private void marka()
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("select marka from marka_bilgileri ", baglantı);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboBox1.Items.Add(read["marka"].ToString());
            }
            baglantı.Close();
        }
        private void frmSeri_Load(object sender, EventArgs e)
        {
            marka();
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("insert into seri_bilgileri(marka,seri) values ('" + comboBox1.Text + "','"+textBox1.Text+"')", baglantı);
            komut.ExecuteNonQuery();
            baglantı.Close();
            MessageBox.Show("markaya bağlı araç serisi eklendi");
            textBox1.Clear();
            comboBox1.Text = "";
            comboBox1.Items.Clear();
            marka();
        }
    }
}
