using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAURentaCar
{
    public partial class aracislem : Form
    {
        public aracislem()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=ISK3L3;Initial Catalog=saürentacar;Integrated Security=True"); //VERİTABANI BAĞLANTISI OLUŞTURULDU 
        DataTable dt = new DataTable();   // VERİTABLOSU OLUŞTURULDU
        
        private void label7_Click(object sender, EventArgs e)
        {

        }

        

        void doldur()            //DOLDUR FONKSİYONU OLUŞTURULDU
        {
            try
            {
                if (baglanti.State==ConnectionState.Closed)
                {
                    baglanti.Open();          //BAĞLANTI KONTROLÜ SAĞLANARAK AÇILDI
                }

                dt.Clear();        //VERİ TABLOSU TEMİZLENDİ
                SqlDataAdapter listele = new SqlDataAdapter("select * from araclar", baglanti);   //ARAÇLAR TABLOSUNDAN TÜM VERİLER ÇEKİLDİ
                listele.Fill(dt);
                dataGridView1.DataSource = dt;
                listele.Dispose();
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;       //DATAGRİDVİEW İLE ARACLAR TABLOSUNDAKİ TÜM BİLGİLER EKRANDA GÖSTERİLDİ
                baglanti.Close();
                textBox1.Text = "";
                textBox2.Text = "";
                maskedTextBox1.Text = "";
                comboBox1.Text = "";
                comboBox2.Text = "";
                comboBox3.Text = "";
                comboBox4.Text = "";
               
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);          //HATA ALINIRSA EKRANDA GÖSTERİLİYOR
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand komut = new SqlCommand("insert into araclar(arac_plaka,marka,model,tip,renk,yil,durum) values ('" + textBox1.Text + "','" + comboBox1.Text + "','" + textBox2.Text + "',' " + comboBox2.Text + "','" + comboBox3.Text + "','" + maskedTextBox1.Text    + "','" + comboBox4.Text + "')", baglanti);   //YENİ ARAÇ EKLEME İŞLEMİ YAPILIYOR
                if (baglanti.State==ConnectionState.Closed)
                {
                    baglanti.Open();      //BAĞLANTI KONTROLÜ SAĞLANARAK TEKRAR AÇILDI

                }
              

                komut.ExecuteNonQuery();
                MessageBox.Show("Ekleme İşlemi Başarılı");  //EKLEME İŞLEMİNİN DURUMU EKRANDA BİLDİRLİDİ
                baglanti.Close();

                doldur();
            
            }
            catch(Exception hata)
            {
                MessageBox.Show(hata.Message);   //HATA ALINIRSA EKRANDA GÖSTERİLİYOR
            } 

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void aracislem_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'saürentacarDataSet.araclar' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.araclarTableAdapter.Fill(this.saürentacarDataSet.araclar);



        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult cevap;
            cevap = MessageBox.Show("Kaydı Silmek İstediğinize Emin Misiniz?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);      //SİLME İŞLEMİNDEN EMİN OLUNUP OLUNMADIĞI KULLANICIYA SORULARAK CEVABI BEKLENİYOR

            if (cevap == DialogResult.Yes && dataGridView1.CurrentRow.Cells[0].Value.ToString().Trim() != "")    //EĞER CEVAP EVET İSE
            {
                try
                {
                    if (baglanti.State==ConnectionState.Closed)
                    {
                        baglanti.Open();      //BAĞLANTI KONTROLLÜ OLARAK TEKRAR AÇILDI
                    }

                    SqlCommand sil = new SqlCommand("delete from araclar where id='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'", baglanti);       //SEÇİLEN İD YE AİT ARAÇ BİLGİLERİNİN TÜMÜ VERİTABANINDAN SİLİNOYOR
                    sil.ExecuteNonQuery();
                    doldur();       //DOLDUR FONKSİYONU TEKRAR ÇAĞRILARAK DATAGRİDVİEW YENİLENEREK GÖSTERİLMESİ SAĞLANIYOR
                    baglanti.Close();

                }
                catch (Exception hata)
                {

                    MessageBox.Show(hata.Message);//HATA ALINIRSA EKRANDA GÖSTERİLİYOR
                }
            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            menu menu = new menu();
            menu.Show();
            this.Hide();
            
        }
    }
}
