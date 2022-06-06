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

namespace final2Tekrar
{
    public partial class Form1 : Form
    {
        OleDbCommand cmd;
        OleDbConnection con;
        OleDbDataAdapter da;
        DataSet ds;

        private string ogrFoto = "";

        public Form1()
        {
            InitializeComponent();
        }
        private void ogrenciListele()
        {
            con = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = C:/Users/Burak/source/repos/final2Tekrar/final2Tekrar/bin/Debug/vt.mdb");
            da = new OleDbDataAdapter("SELECT * FROM okul",con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds,"okul");
            dataGridView1.DataSource = ds.Tables["okul"];
            con.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ogrenciListele();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            string ogrNo = txtOgrNo.Text, ogrAdiSoyadi = txtOgrAdSoyad.Text, ogrTel = txtOgrTel.Text;
            if (ogrNo == "" || ogrAdiSoyadi == "" || ogrTel == "")
            {
                MessageBox.Show("Lütfen Verileri Boş Bırakmayınız", "Hata", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                cmd = new OleDbCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "INSERT INTO okul (ogrNo,ogrAdiSoyadi,ogrTel,ogrFoto) VALUES ('"+ogrNo+"','"+ogrAdiSoyadi+"','"+ogrTel+"','"+ogrFoto+"')";
                cmd.ExecuteNonQuery();
                con.Close();
                ogrenciListele();
            }
        }

        private void btnResimSec_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosya = new OpenFileDialog();
            dosya.Filter = "Resim Dosyası |*.jpg;*.nef;*.png|Tüm Dosyalar |*.*";
            dosya.ShowDialog();
            ogrFoto = dosya.FileName;
            pictureBox1.ImageLocation = ogrFoto;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells.Count == 0)
            {

            }
            else
            {
                txtOgrNo.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtOgrAdSoyad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                txtOgrTel.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                if (dataGridView1.CurrentRow.Cells[4].Value.ToString() == "")
                {
                    pictureBox1.ImageLocation = "";
                }
                else
                {
                    pictureBox1.ImageLocation = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                }
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            string ogrNo = txtOgrNo.Text, ogrAdiSoyadi = txtOgrAdSoyad.Text, ogrTel = txtOgrTel.Text;
            if (ogrNo == "" || ogrAdiSoyadi == "" || ogrTel == "")
            {
                MessageBox.Show("Lütfen Verileri Boş Bırakmayınız", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                cmd = new OleDbCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "UPDATE okul SET ogrNo='"+ogrNo+"', ogrAdiSoyadi='"+ogrAdiSoyadi+"', ogrTel='"+ogrTel+"', ogrFoto='"+ogrFoto+"' WHERE ogrNo='" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "' ";
                cmd.ExecuteNonQuery();
                con.Close();
                ogrenciListele();
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (txtOgrNo.Text == "")
            {
                MessageBox.Show("Lütfen Silmek İstenilen Satırı Seçiniz..::..", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string ogrNo = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                cmd = new OleDbCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "DELETE FROM okul WHERE ogrNo ='"+ogrNo+"'";
                cmd.ExecuteNonQuery();
                con.Close();
                ogrenciListele();
            }
        }

        private void txtOgrenciAra_MouseEnter(object sender, EventArgs e)
        {
            txtOgrenciAra.Text = "";
        }
    }
}
