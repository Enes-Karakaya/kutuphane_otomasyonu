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

namespace kutuphane_otomasyonu
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        static string constring = ("Data Source=DESKTOP-ILG2BKJ\\SQLEXPRESS;Initial Catalog=kutuphane_otomasyonu;Integrated Security=True;Encrypt=False");
        SqlConnection baglan=new SqlConnection(constring);


    private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'kutuphane_otomasyonuDataSet.kitap' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.kitapTableAdapter.Fill(this.kutuphane_otomasyonuDataSet.kitap);

        }
        public void kayitlari_getir()
        {
            string getir = "select * from kitap";

            SqlCommand komut = new SqlCommand(getir, baglan);

            SqlDataAdapter ad = new SqlDataAdapter(komut);

            DataTable dt=new DataTable();

            ad.Fill(dt);
            dataGridView1.DataSource = dt;

            baglan.Close();
        }
        private void button_liste_Click(object sender, EventArgs e)
        {
            kayitlari_getir();
        }

        public void verisil(int id)
        {
            string sil = "Delete from kitap where kitap_no=@id";
            SqlCommand komut =new SqlCommand(sil,baglan);

            baglan.Open();

            komut.Parameters.AddWithValue("@id",id);

            komut.ExecuteNonQuery();
            baglan.Close() ;
        }

        private void button_ekle_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglan.State == ConnectionState.Closed)
                {
                    baglan.Open();
                    string ekle = "insert into kitap(kitap_adi,kitap_yazari,kitap_sayfa_no,kitap_ekleyen_kisi,raf_no)values(@kitapadi,@kitapyazari,@kitapsayfano,@kitapekleyenkisi,@rafno)";
                    SqlCommand komut = new SqlCommand(ekle, baglan);
                    komut.Parameters.AddWithValue("@kitapadi",textBox1.Text);
                    komut.Parameters.AddWithValue("@kitapyazari",textBox2.Text);
                    komut.Parameters.AddWithValue("@kitapsayfano",textBox3.Text);
                    komut.Parameters.AddWithValue("@kitapekleyenkisi",textBox4.Text);
                    komut.Parameters.AddWithValue("@rafno",textBox5.Text);

                    komut.ExecuteNonQuery();

                    MessageBox.Show("Kitap Ekleme İşlemi Başarılı");
                }
            }
            catch(Exception hata)
            {
                MessageBox.Show("Bir hata var!" + hata.Message);
            }
        }

        private void button_sil_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow drow in dataGridView1.SelectedRows)
            {
                int id = Convert.ToInt32(drow.Cells[0].Value);
                verisil(id);
                kayitlari_getir();

                MessageBox.Show("Kitap Silme İşlemi Başarılı");
            }
        }
        int i = 0;
        private void button_guncel_Click(object sender, EventArgs e)
        {
            baglan.Open();
            string kayitguncelle = ("update kitap set kitap_adi=@ad,kitap_yazari=@yazar,kitap_sayfa_no=@sno,kitap_ekleyen_kisi=@kek,raf_no=@rafno where kitap_no=@id");
            SqlCommand komut = new SqlCommand(kayitguncelle, baglan);

            komut.Parameters.AddWithValue("@ad", textBox1.Text);
            komut.Parameters.AddWithValue("@yazar", textBox2.Text);
            komut.Parameters.AddWithValue("@sno", textBox3.Text);
            komut.Parameters.AddWithValue("@kek", textBox4.Text);
            komut.Parameters.AddWithValue("@rafno", textBox5.Text);
            komut.Parameters.AddWithValue("id",dataGridView1.Rows[i].Cells[0].Value);
            komut.ExecuteNonQuery();
            MessageBox.Show("Kayıtlar Başarı İle Güncellendi");
            baglan.Close();
            kayitlari_getir();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            i = e.RowIndex;
            textBox1.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
        }
    }
}
