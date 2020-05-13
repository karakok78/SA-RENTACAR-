using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAURentaCar
{
    public partial class menu : Form
    {
        public menu()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            arackiralama arackiralama = new arackiralama();
            arackiralama.Show();
            this.Hide();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            aracislem aracislem = new aracislem();
            aracislem.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            musteribilgileri musteribilgileri = new musteribilgileri();
            musteribilgileri.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            aracteslimalma aracteslimalma = new aracteslimalma();
            aracteslimalma.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            aracislem aracislem = new aracislem();
            aracislem.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            musteribilgileri musteribilgileri = new musteribilgileri();
            musteribilgileri.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            arackiralama arackiralama = new arackiralama();
            arackiralama.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            aracteslimalma aracteslimalma = new aracteslimalma();
            aracteslimalma.Show();
            this.Hide();
        }
    }
}
