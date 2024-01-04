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
    public partial class kullanicipanel : Form
    {
        public kullanicipanel()
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

        private void kullanicipanel_Load(object sender, EventArgs e)
        {
            this.urunlerTableAdapter1.Fill(this.kantinveriDataSet1.urunler);
            baglan.Open();
            OleDbCommand cmd = new OleDbCommand("select * from urunler", baglan);
            OleDbDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                ListViewItem item = new ListViewItem(dr["u_id"].ToString());
                item.SubItems.Add(dr["u_adi"].ToString());
                item.SubItems.Add(dr["u_ayrinti"].ToString());
                item.SubItems.Add(dr["u_fiyat"].ToString() + " ₺");
                listView1.Items.Add(item);
            }
            baglan.Close();
        }        

        private void btn_giris_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(comboBox1.Text);
            double tutar;
            tutar = Convert.ToDouble(comboBox1.SelectedValue);
            listBox2.Items.Add(tutar);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double topla = 0;
            int saylist = listBox2.Items.Count;
            for (int i = 0; i < saylist; i++)
            {
                topla += Convert.ToDouble(listBox2.Items[i]);
            }
            MessageBox.Show("Siparişiniz Tamamlandı\nÖdemeniz Gereken Tutar: " + topla.ToString() + " ₺", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            comboBox1.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double topla = 0;
            int saylist = listBox2.Items.Count;
            for (int i = 0; i < saylist; i++)
            {
                topla += Convert.ToDouble(listBox2.Items[i]);
            }
            MessageBox.Show("Hesaplanan Tutar: " + topla.ToString() + " ₺", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
        }
    }
}
