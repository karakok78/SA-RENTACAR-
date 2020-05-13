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
    public partial class musteribilgileri : Form
    {
        public musteribilgileri()
        {
            InitializeComponent();
        } 
        SqlConnection baglanti = new SqlConnection("Data Source=ISK3L3;Initial Catalog=saürentacar;Integrated Security=True");       //VERİTABANI BAĞLANTISI YAPILYOR
        DataTable dt = new DataTable();   //VERİTABANINDAN TABLOLAR ALINIYOR
        
        private void musteribilgileri_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'saürentacarDataSet.musteriler' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.musterilerTableAdapter.Fill(this.saürentacarDataSet.musteriler);
            
        }


        void doldur()     //DOLDUR FONKSİYONU OLUŞTURULUYOR
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open(); //BAĞLANTI KONTROL EDİLEREK AÇILIYOR
                }

                dt.Clear();
                SqlDataAdapter listele = new SqlDataAdapter("select * from musteriler", baglanti);   //MÜŞTERİ BİLGİLERİ MÜŞTERİ TABLOSUNDAN ALINIYOR
                listele.Fill(dt);
                dataGridView1.DataSource = dt;
                listele.Dispose();                   //TÜM VERİLER DATAGRİDVİEW ÜZERİNDE GÖSTERİLİYOR
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                baglanti.Close();
                

            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);  //HATA GÖSTERİLİYOR EĞER VARSA
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult cevap; 
            cevap = MessageBox.Show("Kaydı Silmek İstediğinize Emin Misiniz?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);    //MÜŞTERİ TABLOMUZDAN MÜŞTERİ SİLECEKSEK KARARIMIZI SORUGULANIYOR
             
            if (cevap == DialogResult.Yes && dataGridView1.CurrentRow.Cells[0].Value.ToString().Trim() != "")      //CEVAP EVET İSE SİLME İŞLEMİNE GEÇİLİYOR.
            {
                try
                {
                    if (baglanti.State == ConnectionState.Closed)
                    {
                        baglanti.Open();       //BAĞLANTI KONTROL EDİLEREK AÇILIYOR
                    }

                    SqlCommand sil = new SqlCommand("delete from musteriler where id='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'", baglanti);  //SEÇİLEN İD YE SAHİP MÜŞTERİ VERİTABAINDAN SİLİNİYOR
                    sil.ExecuteNonQuery();
                    doldur();    //DATAGRİDVİEW GÜNCEL HALİ İLE TEKRAR YENİLENİYOR
                    baglanti.Close();

                }
                catch (Exception hata)
                {

                    MessageBox.Show(hata.Message);  // HATA OLMASI HALİNDE HATA GÖSTERİLİYOR
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            guncelle guncelle = new guncelle();
            guncelle.Show();
            this.Hide();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            menu menu = new menu();
            menu.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            arackiralama arackiralama = new arackiralama();
            arackiralama.Show();
            this.Hide();
        }
    }
}
