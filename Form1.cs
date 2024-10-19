using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media; 

namespace carRace
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void sesAc()
        {
            SoundPlayer ses= new SoundPlayer();
            string sesYol = Application.StartupPath + "\\lostsky.wav";
            ses.SoundLocation = sesYol;
            ses.Play();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            oyunuBaslat();
            sesAc();
        }

        int kazanilanPuan=0;
        int yolHizi = 10;
        int arabaHizi = 5;//benim arabamın hızı

        bool solYon=false;
        bool sagYon=false;

        int digerArabaHizlari = 5;// diğer arabaların hızı

        Random rnd= new Random();

        public void oyunuBaslat()
        {
            btn_oyunuBaslat.Enabled = false;
            carpma.Visible = false;

            lbl_yuksekSkor.Text=Settings1.Default.yuksekSkor;

            arabaHizi = 25;
            digerArabaHizlari= 5;
            kazanilanPuan = 0;

            //kendi arabamın koordinatları
            bizimaraba.Left = 160;
            bizimaraba.Top = 250;

            //diğer arabaların başlangıçtaki koordinatları

            araba1.Left = 80;
            araba1.Top = 50;

            araba2.Left = 230;
            araba2.Top = 50;

            solYon =false;
            sagYon =false;

            carpma.Left = 165;
            carpma.Top = 200;

            timer1.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            kazanilanPuan++;
            lbl_puan.Text=kazanilanPuan.ToString();

            yol.Top += yolHizi;
            if (yol.Top > 400) { yol.Top = -100; }


            if (solYon) { bizimaraba.Left -= arabaHizi; }
            if (sagYon) { bizimaraba.Left += arabaHizi; }

            if (bizimaraba.Left < 1) { solYon = false; }
            else if (bizimaraba.Left + bizimaraba.Width > 550) { sagYon = false; }
            //arabamızın sol tarafında kalan alan ve arabamızın genişliği panelin genişliğinden büyükse sağ yönü pasif hale getir.

            araba1.Top += arabaHizi;
            araba2.Top += arabaHizi;

            if(araba1.Top>panel1.Height)
            {
                araba1Degistir();
                araba1.Left = rnd.Next(20,350);
                araba1.Top = rnd.Next(40,140)* -1; 
            }

            if (araba2.Top > panel1.Height)
            {
                araba2Degistir();
                araba2.Left = rnd.Next(20,350);
                araba2.Top = rnd.Next(40,140)* -1;
            }

            //Benim arabamın bulunduğu koordinat ile araba1'in bulunduğu koordinat çakışırsa demek ki birbirleriyle çarpışmışlardır.
            if (bizimaraba.Bounds.IntersectsWith(araba1.Bounds) || bizimaraba.Bounds.IntersectsWith(araba2.Bounds))
            {
                oyunBitti();
            }

        }

        private void araba1Degistir()
        {
            int sira = rnd.Next(1, 7);
            switch (sira)
            {
                case 1:
                    araba1.Image=Properties.Resources.araba5;
                    break;

                case 2:
                    araba1.Image = Properties.Resources.araba7;
                    break;
                case 3:
                    araba1.Image = Properties.Resources.araba3;
                    break;
                case 4:
                    araba1.Image = Properties.Resources.araba4;
                    break;
                case 5:
                    araba1.Image = Properties.Resources.araba5;
                    break;
                case 6:
                    araba1.Image = Properties.Resources.araba6;
                    break;
                case 7:
                    araba1.Image = Properties.Resources.araba7;
                    break;
                
            }
    
        
        
        }
        private void araba2Degistir()
        {
            int sira = rnd.Next(1, 7);
            switch (sira)
            {
                case 1:
                    araba2.Image = Properties.Resources.araba5;
                    break;

                case 2:
                    araba2.Image = Properties.Resources.araba7;
                    break;
                case 3:
                    araba2.Image = Properties.Resources.araba3;
                    break;
                case 4:
                    araba2.Image = Properties.Resources.araba4;
                    break;
                case 5:
                    araba2.Image = Properties.Resources.araba5;
                    break;
                case 6:
                    araba2.Image = Properties.Resources.araba6;
                    break;
                case 7:
                    araba2.Image = Properties.Resources.araba7;
                    break;

            }



        }

        private void oyunBitti() 
        {
            timer1.Stop();


            if(Convert.ToInt32(lbl_puan.Text) > Convert.ToInt32(Settings1.Default.yuksekSkor.ToString()))
            {
                Settings1.Default.yuksekSkor = lbl_puan.Text;
            }


            btn_oyunuBaslat.Enabled = true;
            carpma.Visible = true;
            bizimaraba.Controls.Add(carpma);
            carpma.Location=new Point(7,-5);
            carpma.BringToFront(); //çarpmayı öne al
            carpma.BackColor = Color.Transparent;
            MessageBox.Show("Tebrikler kazandığınız puan: "+lbl_puan.Text,"Bilgilendirme",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left && bizimaraba.Left>0)
            {
                solYon = true;
            }
            if(e.KeyCode == Keys.Right && bizimaraba.Left+bizimaraba.Width<panel1.Width)
            {
                sagYon = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                solYon = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                sagYon=false;
            }
        }

        private void btn_oyunuBaslat_Click(object sender, EventArgs e)
        {
            oyunuBaslat();
        }
    }


}
