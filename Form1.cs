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

namespace YazLab3
{
    public partial class Form1 : Form
    {
        String dosyaUzantisi;
        String dosyaadi;
        double k;
        Byte[] bdizi;
        int w = 176;
        int h = 144;
        int sayac3 = 0;
        Bitmap[] fdizi;
        Bitmap[] rfdizi;
        int rw, rh;

        Bitmap bitmap;
        int fsayisi = 0;
        int tsayac = 0;

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void resizer()
        {
            if (textBox3.Text != "" && textBox4.Text != "")
            {
                rw = Int32.Parse(textBox3.Text);
                rh = Int32.Parse(textBox4.Text);
            }

            rfdizi = new Bitmap[fsayisi];

            for (int i = 0; i < fsayisi; i++)
            {
                rfdizi[i] = new Bitmap(fdizi[i], new Size(rw, rh));
            }
           
        }

        public void video2(int sayac)
        {

            if (sayac >= fsayisi)
            {
                sayac3 = 0;
                sayac = 0;
                timer1.Enabled = false;
            }
            pictureBox1.Image = rfdizi[sayac];

        }
        public void video(int sayac)
        {
            if (sayac >= fsayisi)
            {
                sayac3 = 0;
                sayac = 0;
                timer1.Enabled = false;
            }
            pictureBox1.Image = fdizi[sayac];

        }



        public void show()//
        {

            int sayac1 = 0;
            int sayac2 = 0;

            //MessageBox.Show(""+fsayisi);
            while (sayac2 < fsayisi)
            {
                bitmap = new Bitmap(w, h);
                int periyot = (int)(w * h * (k) * sayac2);
                for (int i = 0; i < h; i++)
                {
                    for (int j = 0; j < w; j++)
                    {
                        Color renk = Color.FromArgb(bdizi[sayac1 + periyot], bdizi[sayac1 + periyot], bdizi[(sayac1++) + periyot]);
                        bitmap.SetPixel(j, i, renk);
                    }
                }
                //pictureBox1.Image = bitmap;
                fdizi[sayac2] = bitmap;
                sayac2++;
                sayac1 = 0;


            }
        }


        public void fRead()//
        {
            w = Int32.Parse(textBox1.Text);
            h = Int32.Parse(textBox2.Text);
            rw = w;
            rh = h;
            FileStream fs = new FileStream(dosyaUzantisi, FileMode.Open);
            //MessageBox.Show("" +(int) fs.Length);
            int a = 0;
            int index = 0;
            bdizi = new Byte[fs.Length];
            fsayisi = bdizi.Length / (int)(k * w * h);
            fdizi = new Bitmap[fsayisi];
            while (a > -1)
            {
                a = fs.ReadByte();
                if (a != -1)
                {
                    bdizi[index++] = (Byte)a;
                    //MessageBox.Show(bdizi.Length+ " "+ bdizi[index-1]);
                }
            }
            fs.Close();
        }

        private void wmp_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tsayac == 0)
            {
                resizer();
                tsayac++;
            }
            
            timer1.Interval = 40;
            timer1.Enabled = true;
            timer2.Enabled = false;
            video(sayac3++);
        }



        private void timer1_Tick_1(object sender, EventArgs e)
        {
            video(sayac3++);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fdizi[sayac3].Save("C:\\Users\\Sinan\\Desktop\\yzlb3\\Anlık Yakalanan Fotoğraflar\\a" + sayac3 + ".bmp");
            if (rfdizi != null)
            {
                rfdizi[sayac3].Save("C:\\Users\\Sinan\\Desktop\\yzlb3\\Anlık Yakalanan Fotoğraflar\\ra" + sayac3 + ".bmp");

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            String s = comboBox1.Text;
            if (s.Equals("4-4-4"))
            {
                k = 3;
            }
            else if (s.Equals("4-2-2"))
            {
                k = 2;
            }
            else
            {
                k = 1.5;
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            tsayac = 0;
            OpenFileDialog dosyaSec = new OpenFileDialog();

            if (dosyaSec.ShowDialog() == DialogResult.OK)
            {
                FileInfo dosyabilgisi = new FileInfo(dosyaSec.FileName);
                dosyaUzantisi = dosyabilgisi.FullName;
                dosyaadi = dosyabilgisi.Name;

            }
            fRead();
            show();

            Directory.CreateDirectory("C:\\Users\\Sinan\\Desktop\\yzlb3\\" + dosyaadi + "Frame Listesi");

            for (int i = 0; i < fsayisi; i++)
            {
                fdizi[i].Save("C:\\Users\\Sinan\\Desktop\\yzlb3\\" + dosyaadi + "Frame Listesi\\a" + i + ".bmp");
            }

            MessageBox.Show("Videonuz Oynatılmaya Hazır!!");



        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

            resizer();
            timer2.Interval = 40;
            timer2.Enabled = true;
            timer1.Enabled = false;
            video2(sayac3++);


        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            video2(sayac3++);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            timer2.Enabled = false;

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }




    }
}


