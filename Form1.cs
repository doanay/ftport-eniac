using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace FtpPortv0
{
    public partial class Form1 : Form
    {
        string konum;
        List <string> Files=new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnBaglan_Click(object sender, EventArgs e)
        {
            lblbaglantı.Visible = false;lblbaglantı.Text = "Baglantı Kuruluyor.....";
            try
            {
                FtpWebRequest request = FtpWebRequest.Create(txtFtpAdres.Text) as FtpWebRequest;
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential(txtKullaniciAdi.Text,maskedTextBox1.Text);
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                while (!reader.EndOfStream)
                {
                    Application.DoEvents();
                    Files.Add(reader.ReadLine());
                    lblbaglantı.Text = "Bağlantı Kuruldu..";
                }
                response.Close();
                responseStream.Close();
                reader.Close();
                lblbaglantı.Text = "Baglandı";
                lblbaglantı.ForeColor = Color.Green;
                foreach (string file in Files)
                {
                    listBox1.Items.Add(file);
                }
            }
            catch (Exception ex)
            {
                lblbaglantı.Text = "Baglantı Kurulamadı. Lütfen tekrar deneyin.";
                MessageBox.Show(ex.Message,"Error 1 : Hata kodları için siteyi kontrol edin!");
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void lblbaglantı_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string curItem = listBox1.SelectedItem.ToString();     
            string inputfilepath = curItem;
            string ftphost = txtFtpAdres.Text;
            string ftpfilepath = konum;

            string ftpfullpath =  ftphost + ftpfilepath;

            using (WebClient request = new WebClient())
            {
                request.Credentials = new NetworkCredential(txtKullaniciAdi.Text, maskedTextBox1.Text);
                byte[] fileData = request.DownloadData(ftpfullpath);

                using (FileStream file = File.Create(inputfilepath))
                {
                    file.Write(fileData, 0, fileData.Length);
                    file.Close();
                }
                MessageBox.Show("İndirme "+konum+" ' konumuna indirildi.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {

                    konum = folderDialog.SelectedPath;
                }
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Error 103 : Lütfen Sunucunuzun internet bağlantısını kontrol edin.\nHatayla tekrar karşılaşıyorsanız teameniac.net/support adresinden bize ulaşın!");
        }
    }
}
