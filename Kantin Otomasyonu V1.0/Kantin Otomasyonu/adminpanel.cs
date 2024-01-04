using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Kantin_Otomasyonu
{
    public partial class adminpanel : Form
    {
        public adminpanel()
        {
            InitializeComponent();
        }

        OleDbConnection baglan = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=kantinveri.accdb");

        private void kapat_Click(object sender, EventArgs e)
        {
            DialogResult onay = MessageBox.Show("Oturumu Sonlandırmak İstediğinize Emin Misiniz ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (onay == DialogResult.Yes)
            {
                this.Hide();
                giris giris = new giris();
                giris.Show();
            }
            else
            {

            }
        }

        private void adminpanel_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'kantinDataSet2.urunler' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.urunlerTableAdapter1.Fill(this.kantinveriDataSet1.urunler);
            // TODO: Bu kod satırı 'kantinDataSet1.kullanicilar' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.kullanicilarTableAdapter1.Fill(this.kantinveriDataSet1.kullanicilar);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglan.Open();
            OleDbCommand komut = new OleDbCommand("insert into kullanicilar([k_adi], [k_sifre], [k_yetki]) values(?,?,?)", baglan);

            if(textBox1.Text == "" || textBox2.Text == "" || comboBox1.Text == "")
            {
                MessageBox.Show("Lütfen! Boş Alanları Doldurunuz", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                komut.Parameters.AddWithValue("@k_adi", textBox1.Text);
                komut.Parameters.AddWithValue("@k_sifre", textBox2.Text);
                komut.Parameters.AddWithValue("@k_yetki", comboBox1.SelectedItem.ToString());

                komut.ExecuteNonQuery();                
                MessageBox.Show("Kullanıcı kaydedildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.kullanicilarTableAdapter1.Fill(this.kantinveriDataSet1.kullanicilar);
            }
            baglan.Close();
        }

        private void k_guncelle_Click(object sender, EventArgs e)
        {
            DialogResult onay = MessageBox.Show("Kullanıcıyı Güncellemek İstediğinize Emin Misiniz ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (onay == DialogResult.Yes)
            {
                baglan.Open();
                OleDbCommand cmd = new OleDbCommand("update kullanicilar set k_adi = '" + textBox1.Text + "', k_sifre = '" + textBox2.Text + "', k_yetki = '" + comboBox1.Text + "' where k_adi = '" + textBox1.Text + "'", baglan);
                cmd.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Kullanıcı güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.kullanicilarTableAdapter1.Fill(this.kantinveriDataSet1.kullanicilar);
            }
            else
            {

            }
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            textBox1.Text = listBox1.Text;
            baglan.Open();
            OleDbCommand cmd = new OleDbCommand("select * from kullanicilar where k_adi='" + textBox1.Text + "'", baglan);
            OleDbDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                textBox2.Text = (rd[2].ToString());
                comboBox1.Text = (rd[3].ToString());
                break;
            }
            baglan.Close();
        }

        private void listBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            textBox4.Text = listBox2.Text;
            baglan.Open();
            OleDbCommand cmd = new OleDbCommand("select * from urunler where u_adi='" + textBox4.Text + "'", baglan);
            OleDbDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                textBox5.Text = (rd[2].ToString());
                textBox3.Text = (rd[3].ToString());
                break;
            }
            baglan.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglan.Open();
            OleDbCommand komut = new OleDbCommand("insert into urunler([u_adi], [u_ayrinti], [u_fiyat]) values(?,?,?)", baglan);

            komut.Parameters.AddWithValue("@u_adi", textBox4.Text);
            komut.Parameters.AddWithValue("@u_ayrinti", textBox5.Text);
            komut.Parameters.AddWithValue("@u_fiyat", textBox3.Text);

            komut.ExecuteNonQuery();
            baglan.Close();
            MessageBox.Show("Ürün kaydedildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.urunlerTableAdapter1.Fill(this.kantinveriDataSet1.urunler);
        }

        private void listBox1_SelectedValueChanged_1(object sender, EventArgs e)
        {
            textBox1.Text = listBox1.Text;
            baglan.Open();
            OleDbCommand cmd = new OleDbCommand("select * from kullanicilar where k_adi='" + textBox1.Text + "'", baglan);
            OleDbDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                textBox2.Text = (rd[2].ToString());
                comboBox1.Text = (rd[3].ToString());
                break;
            }
            baglan.Close();
        }

        private void listBox2_SelectedValueChanged_1(object sender, EventArgs e)
        {
            textBox4.Text = listBox2.Text;
            baglan.Open();
            OleDbCommand cmd = new OleDbCommand("select * from urunler where u_adi='" + textBox4.Text + "'", baglan);
            OleDbDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                textBox5.Text = (rd[2].ToString());
                textBox3.Text = (rd[3].ToString());
                break;
            }
            baglan.Close();
        }
    }
}
