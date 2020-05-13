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
    public partial class arackiralama : Form
    {
        public arackiralama()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=ISK3L3;Initial Catalog=saürentacar;Integrated Security=True");
        DataTable dt = new DataTable();


        private void arackiralama_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'saürentacarDataSet2.musteriler' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.musterilerTableAdapter.Fill(this.saürentacarDataSet2.musteriler);
            // TODO: Bu kod satırı 'saürentacarDataSet2.musteriler' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.musterilerTableAdapter.Fill(this.saürentacarDataSet2.musteriler);
            comboBox1.Items.Clear();                                       
            if (baglanti.State == ConnectionState.Closed)                         //eğer VERİTABANI BAĞLANTISI KAPALI İSE BAĞLANTI TEKRAR AÇILIR
            {
                baglanti.Open();
            }
            SqlCommand komut = new SqlCommand("select arac_plaka from araclar where durum='" + "Uygun" + "'", baglanti);              //ARACLAR TABLOSUNDAN DURUMU UYGUN OLAN ARAÇLAR ALINIR
            SqlDataReader oku = komut.ExecuteReader(); 
            while (oku.Read())
            {
                comboBox1.Items.Add(oku["arac_plaka"].ToString());         //COMBOBOX'IN DEĞERLERİ VERİTABANINDAN ALINAN DURUMU UYGUN OLAN ARAÇLAR İLE DOLDURULUR
            }
            baglanti.Close();     //VERİTABANI BAĞLANTISI KAPATILIR
            doldur();                  //DOLDUR ADINDAKİ FONKSİYON ÇAĞRILIYOR
            
        }

        void doldur()           //DOLDUR ADINDA FONKSİYON OLUŞTURULDU
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();                   //BAĞLANTI KONTROLÜ YAPILARAK TEKRAR AÇOILDI
                }

                dt.Clear();
                SqlDataAdapter listele = new SqlDataAdapter("select * from araclar where durum='" + "Uygun" + "'", baglanti);              //ARAÇLAR TABLOSUNDA DURUMU UYGUN OLAN ARAÇ BİLGİLERİ ALINDI
                listele.Fill(dt);
                dataGridView1.DataSource = dt;
                listele.Dispose();
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;        //DATAGRİDVİEW İÇİN VERİTABAINDAN UYGUN OLAN ARAÇLARIN TÜÖ ÖZELLİKLERİ LİSTELENDİ
                baglanti.Close();
                maskedTextBox1.Text = "";
                maskedTextBox2.Text = "";             //KUTUCUKLARIN İÇİ TEMİZLENDİ
                maskedTextBox3.Text = "";
                textBox1.Text = "";
                textBox2.Text = "";
                comboBox1.Text = "";
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);          //EĞER HATA ALINIRSA HATA MESSAGEBOX İLE EKRANDA GÖSTERİLDİ
            }
        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand komut = new SqlCommand("insert into musteriler(tc,ad,soyad,telefon,sure,arac_plaka) values('" + maskedTextBox1.Text + "','" + textBox1.Text + "','" + textBox2.Text + "','" + maskedTextBox3.Text + "','" + maskedTextBox2.Text + "','" + comboBox1.Text + "')", baglanti);   //KİRALAMA İŞLEMİ YAPAN MÜŞTERİ BİLGİLERİ VERİTABANINA KAYDEDİLDİ

                if (baglanti.State==ConnectionState.Closed)
                {
                    baglanti.Open();         //BAĞLANTI KONTROLÜ SĞLANDI VE TEKRAR AÇILDI
                }

                komut.ExecuteNonQuery();

                try
                {
                    if (baglanti.State==ConnectionState.Closed)
                    {
                        baglanti.Open();
                    }
                    SqlCommand guncelle = new SqlCommand("update araclar set durum ='" + "Kirada" + "'where arac_plaka='" + comboBox1.Text + "' ", baglanti);  //KİRALANAN ARACIN DURUMU VERİTABANINDAN "KİRADA" OLARAK GÜNCELLENDİ
                    guncelle.ExecuteNonQuery();

                
                
                }
                
                catch (Exception)
                {

                    throw;
                }

                MessageBox.Show("Kiralama İşleminiz Başarılı");           
                baglanti.Close();
                doldur();

            }
            
            catch (Exception hata)
            {

                MessageBox.Show(hata.Message);             //HATA ALINIRSA EKRANDA GÖSTERİLİYOR 
            }

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            menu menu = new menu();
            menu.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            musteribilgileri musteribilgileri = new musteribilgileri();
            musteribilgileri.Show();
            this.Hide();
        }
    }
}
