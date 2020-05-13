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
    public partial class aracteslimalma : Form
    {
        public aracteslimalma()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=ISK3L3;Initial Catalog=saürentacar;Integrated Security=True");     //VERİTABANI BAĞLANTISI AÇILIYOR
        DataTable dt = new DataTable();
        private void aracteslimalma_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'saürentacarDataSet.musteriler' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.musterilerTableAdapter.Fill(this.saürentacarDataSet.musteriler);
            comboBox1.Items.Clear();
            if (baglanti.State == ConnectionState.Closed)        //BAĞLANTI KONTROLÜ SAĞLANARAK AÇILIOYR
            {
                baglanti.Open();
            }
            SqlCommand komut = new SqlCommand("select arac_plaka from musteriler " , baglanti);  //MÜŞTERİLER TABLOSUNDAN TÜM PLAKALAR ÇEKİLİYOR
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox1.Items.Add(oku["arac_plaka"].ToString());  //TESLİM ALINACAK PLAKALAR LİSTELENDİ
            } 
            baglanti.Close();
        }
        void doldur() //DATAGRİDVİEW İÇİN DOLDUR FONKSİYONU OLUŞTURULDU
        {

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open(); //BAĞLANTI KONTROL EDİLEREK KAPALIYSA TEKRAR AÇILIYOR
                }

                dt.Clear();
                SqlDataAdapter listele = new SqlDataAdapter("select arac_plaka from musteriler", baglanti);     //MÜŞTERİLER TABLOSUNDAN ARAC PLAKALARI TEKRAR ÇEKİLEREK GÜNCEL HALİ DATA GRİDVİEWDE GÖSTERİLİYOR
                listele.Fill(dt);
                dataGridView1.DataSource = dt;
                listele.Dispose();
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                baglanti.Close();


            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);      //HATA ALINIRSA HATA EKRANA VERİLİYOR
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();    //BAĞLANTI KONTROL EDİLEREK TEKRAR AÇILIYOR
            }  
            SqlCommand guncelle = new SqlCommand("update araclar set durum ='" + "Uygun" + "'where arac_plaka='" + comboBox1.Text + "' ", baglanti);  //TESLİM ALINAN ARACIN DURUMU ARAÇLAR TABLOSUNDA TEKRAR UYGUN HALE GETİRİLİYOR
            guncelle.ExecuteNonQuery();

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();    //BAĞLANTI KONTROL EDİLEREK TEKRAR AÇILDI
                }

                SqlCommand sil = new SqlCommand("delete from musteriler where arac_plaka='" + comboBox1.Text + "'", baglanti);      //ARAC TESLİM ALININCA MÜŞTERİ BİLGİLERİ VERİTABANINDAN SİLİNİYOR
                sil.ExecuteNonQuery();
                
                baglanti.Close();

            }
            catch (Exception hata)
            {

                MessageBox.Show(hata.Message);   //HATA OLMASI DURUMUNDA BİLDİRİM VERİLİYOR
            }


            
            baglanti.Close();
            MessageBox.Show("Araç Başarılı Şekilde Teslim Alınmıştır");        //İŞLEM SONUCU EKRANDA KUTUCUKTA GÖSTERİLİYOR
            doldur();  
            comboBox1.Text = ""; //PLAKA SEÇME KUTUSU TEMİZLENDİ
        }

        private void button2_Click(object sender, EventArgs e)
        {
            menu menu = new menu();
            menu.Show();      //ANASAYFA BUTONUNDA MENUYE DÖNÜŞ FORMU AÇILIYOR
            this.Hide();
        }
    }
}
