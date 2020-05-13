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

namespace SAURentaCar
{
    public partial class guncelle : Form
    {
        public guncelle()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=ISK3L3;Initial Catalog=saürentacar;Integrated Security=True");       //VERİTABANI BAĞLANTISI YAPILIYOR
        DataTable dt = new DataTable();         //VERİTABANINDAN TABLOLAR ALINIYOR
        private void guncelle_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'saürentacarDataSet.musteriler' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.musterilerTableAdapter.Fill(this.saürentacarDataSet.musteriler);
            comboBox1.Items.Clear();
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();         //BAĞLANTI KONTROL EDİLEREK AÇILIYOR
            }
            SqlCommand komut = new SqlCommand("select arac_plaka from araclar where durum='" + "Uygun" + "'", baglanti); //VERİTABANINDA ARAÇLAR TABLOSUNDAN URUMU UYGUN OLANLAR KOMUT YARDIMI İLE SEÇİLİYOR
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox1.Items.Add(oku["arac_plaka"].ToString()); //UYGUN ARAÇLARIN PLAKALARI COMBOBOXA VERİLİYOR
            }
        }
        void doldur()     //DOLDUR FONKSİYONU OLUŞTURULUYOR
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();    //BAĞLANTI KONTROLÜ SAĞLANIYOR VE KAPALI İSE AÇILIYOR
                } 

                dt.Clear();
                SqlDataAdapter listele = new SqlDataAdapter("select * from musteriler", baglanti);     //MÜŞTERİLER TABLOSUNDAKİ TÜM VERİLER ÇEKİLİYOR
                listele.Fill(dt);
                dataGridView1.DataSource = dt; 
                listele.Dispose();
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; //DATAGRİDVİEW İLE TÜM BİLGİLER GÖSTERİLİYOR
                baglanti.Close();


            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message); //HATA ALINIRSA HATALAR GÖSTERİLDİ
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();      //BAĞLANTI KONTROL EDİLEREK AÇILIYOR
                }

                SqlCommand adtr = new SqlCommand("UPDATE musteriler set tc='" + maskedTextBox1.Text + "',ad='" + textBox1.Text + "',soyad='" + textBox2.Text + "',telefon='" + maskedTextBox2.Text + "',sure='" + maskedTextBox3.Text + "',arac_plaka='" + comboBox1.Text + "'where id='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'", baglanti);     //GÜNCELLENECEK MÜŞTERİNİN BİLGİLERİ VERİTABANINDA GÜNCELLENİYOR
                adtr.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Güncelleme İşlemi Başarılı"); //GÜNCELLEME SONUCU BİLDRİLİYOR
                doldur();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            musteribilgileri musteribilgileri = new musteribilgileri();
            musteribilgileri.Show();
            this.Hide();

        }
    }
}
