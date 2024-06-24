using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace kutuphane_otomasyonu
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlDataReader dr;
        SqlCommand com;


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Kullanici = textBox1.Text;
            string sifre = textBox2.Text;
            con= new SqlConnection("Data Source=DESKTOP-ILG2BKJ\\SQLEXPRESS;Initial Catalog=kutuphane_otomasyonu;Integrated Security=True;Encrypt=False");
            com = new SqlCommand();
            con.Open();
            com.Connection=con;
            com.CommandText = "select*from kullanici where kullanici_adi='" + textBox1.Text +
                "'and sifre='" + textBox2.Text + "'";
            dr=com.ExecuteReader();
            if (dr.Read() )
            {
                MessageBox.Show("Giriş Başarılı");
                Form2 gecis = new Form2();
                gecis.Show();
                this.Hide();
            }

            else
            {
                MessageBox.Show("Hatalı Kullanıcı Adı veya Şifre");
            }
            con.Close();

        }
    }
}
