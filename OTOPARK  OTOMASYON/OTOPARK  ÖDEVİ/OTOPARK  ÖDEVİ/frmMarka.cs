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
    public partial class frmMarka : Form
    {
        public frmMarka()
        {
            InitializeComponent();
        }
        SqlConnection baglantı = new SqlConnection("Data Source=DESKTOP-EKSE2T7;Initial Catalog=Araç_otopark;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("insert into marka_bilgileri(MARKA) values ('" + textBox1.Text + "')", baglantı);
            komut.ExecuteNonQuery();
            baglantı.Close();
            MessageBox.Show("marka eklendi");
            textBox1.Clear();
        }

        private void frmMarka_Load(object sender, EventArgs e)
        {

        }
    }
}
