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

namespace SAURentaCar
{
    public partial class giris : Form
    {
        public giris()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection veritabani = new SqlConnection("Data Source=ISK3L3;Initial Catalog=saürentacar;Integrated Security=True");      //VERİTABANI BAĞLANTISI YAPILDI 
            SqlCommand komut = new SqlCommand("select * from giris where tc='" + textBox1.Text + "' and sifre= '" + textBox2.Text + "'", veritabani);     // VERİTABAINDAN KULLANICI GİRİŞ BİLGİLERİ KONTROLÜ YAPILDI
            veritabani.Open();       //VERİTABANI AÇILDI

            SqlDataReader oku = komut.ExecuteReader();
            if (oku.Read())
            {
                menu menu = new menu();                                //BAŞARILI GİRİŞ SAĞLANDIĞINDA "MENU" FORMU AÇILIP LOGİN EKRANI KAPANIR VE EKRANA MESSAGEBOX TA BİLDİRİM VERİLİR
                menu.Show();
                this.Hide();
                MessageBox.Show("Başarılı Giriş");
                veritabani.Close();
                
            }
            else
            {
                MessageBox.Show("Hatalı Giriş! Lütfen Tekrar Deneyin");
                textBox1.Text = "";                                                //HATALI GİRİŞ OLDUĞU TAKDİRDE TEXTBOXLAR TEMİZLENİR VE HATALI GİRİŞ UYARISI VERİLİR
                textBox2.Text = "";
            }


            
        }
    }
}
