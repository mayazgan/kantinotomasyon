using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Kantin_Otomasyonu
{
    public partial class giris : Form
    {
        public giris()
        {
            InitializeComponent();
        }

        OleDbConnection baglan = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=kantinveri.accdb");

        bool gorunme = false;
        char c = (char)0x2022;

        private void button1_Click(object sender, EventArgs e)
        {
            if (gorunme)
            {
                button1.BackgroundImage = Properties.Resources.gozac;
                sifre_txtbox.PasswordChar = c;
            }
            else
            {
                button1.BackgroundImage = Properties.Resources.gozkapat;
                sifre_txtbox.PasswordChar = kadi_txtbox.PasswordChar;
            }
            gorunme = !gorunme;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Random rastgele = new Random();
            int r_sayi = rastgele.Next(1111, 9999);
            g_kod.Text = r_sayi.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox4_Click(sender, e);
            sifre_txtbox.PasswordChar = c;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            DialogResult onay = MessageBox.Show("Çıkmak İstediğinize Emin Misiniz ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (onay == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {

            }
        }

        private void btn_giris_Click(object sender, EventArgs e)
        {
            string kvt = "select * from kullanicilar where k_adi='" + kadi_txtbox.Text + "' and k_sifre='" + sifre_txtbox.Text + "' and k_yetki";
            baglan.Open();
            OleDbCommand cmd = new OleDbCommand(kvt, baglan);
            OleDbDataReader rd = cmd.ExecuteReader();
            if (rd.Read() == true)
            {
                if (rd["k_adi"].ToString() == kadi_txtbox.Text)
                {
                    if (rd["k_sifre"].ToString() == sifre_txtbox.Text)
                    {
                        if (rd["k_yetki"].ToString() == "Yetkili")
                        {
                            if (gkod_txtbox.Text == g_kod.Text)
                            {
                                MessageBox.Show("Giriş Başarılı!\nGiriş Tipi: Yetkili", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                adminpanel adminpanel = new adminpanel();
                                adminpanel.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Güvenlik Kodunu Hatalı Girdiniz!\nLütfen Tekrar Deneyiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else if (rd["k_yetki"].ToString() == "Normal")
                        {
                            if (gkod_txtbox.Text == g_kod.Text)
                            {
                                MessageBox.Show("Giriş Başarılı!\nGiriş Tipi: Normal", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                kullanicipanel kullanicipanel = new kullanicipanel();
                                kullanicipanel.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Güvenlik Kodunu Hatalı Girdiniz\nLütfen Tekrar Deneyiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Geçersiz Yetki, Yetki Bulunamadı!\nLütfen Tekrar Deneyiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Hatalı Şifre\nLütfen Tekrar Deneyiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (rd["k_adi"].ToString() != kadi_txtbox.Text)
                {
                    MessageBox.Show("Kullanıcı Adı Bulunamadı!\nLütfen Tekrar Deneyiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                }
            }
            else if (kadi_txtbox.Text == "" || sifre_txtbox.Text == "" || gkod_txtbox.Text == "")
            {
                MessageBox.Show("Lütfen Boş Alanları Doldurunuz\nVe Tekrar Deneyiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Eksik Veya Hatalı Giriş Yaptınız!\nLütfen Tekrar Deneyiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            baglan.Close();
        }
    }
}
