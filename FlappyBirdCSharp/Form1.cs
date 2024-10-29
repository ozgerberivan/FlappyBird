using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;


namespace FlappyBirdCSharp
{
    public partial class Form1 : Form
    {


        // Oyun Değişkenlerinin Tanımlandığı yer.
        int cloudSpeed = 2; 
        int pipeSpeed = 6;
        int gravity = 1;
        int score = 0;
        int count = 0;
        int cloudCount = 0;
        public Form1()
        {
            InitializeComponent();
          
        }

        private void gameTimerEvent(object sender, EventArgs e)
        {
            var randomNum = new Random();
            bird.Top += gravity;
            pipeBottom.Left -= pipeSpeed;
            pipeTop.Left -= pipeSpeed;
            clouds1.Left -= cloudSpeed;
            scoreText.Text = "Score: " + score;

            


            // Bulutun artış sayısını ve yönünü
            if (clouds1.Left < -200)
            {
                cloudCount++;
                clouds1.Left = 950;
                // Bulutlar için rastgele dikey konumu ayarlar.
                if (cloudCount % 2 == 0)
                {
                    clouds1.Top = randomNum.Next(158, 386);
                }
                else if (cloudCount % 2 == 1)
                {
                    clouds1.Top = randomNum.Next(237, 368);
                }
            }

            //  Boruları sağa konumlandırır.
            if (pipeBottom.Left < -150)
            {
                pipeBottom.Left = randomNum.Next(650, 950);
                score++;
            }

            // Üstü boru ekran dışına çıkınca sağa konumlandırılır.
            if (pipeTop.Left < -150)
            {
                
                pipeTop.Left = randomNum.Next(750, 1050);
                score++;
                count++;
                
                if (count % 2 == 0)
                {
                    pipeTop.Top = randomNum.Next(-94, -31);
                }
                else if (count % 2 == 1)
                {
                    pipeTop.Top = randomNum.Next(-31, -10);
                }

            }
            // Boru hızını arttırmak için ( zorlaştırılmış halini )
            if (score > 5 && score <= 10)
            {
                pipeSpeed = 8;
            }

            if (score > 10 && score <= 15)
            {
                pipeSpeed = 10;
            }

            // Çaprışmayı algılamak için.
            if (bird.Bounds.IntersectsWith(pipeBottom.Bounds) ||
                bird.Bounds.IntersectsWith(pipeTop.Bounds) ||
                bird.Bounds.IntersectsWith(ground.Bounds) ||
                bird.Top < -25)

            {
                //bird.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                endGame();
            }

        }

        // Boşluğa basıldığı zaman kuşun yukarı önlü haraket etmesini sağlıyor.
        private void gameKeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
               
                gravity = -20;
               
                e.Handled = true;
                e.SuppressKeyPress = true; 

            }
        }


        // Boşluk tuşuna basılmadığı zaman kuşun yere çakılması
        private void gameKeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                
                gravity = 5;
            }
        }

        private void endGame()
        {
            gameTimer.Stop();
            scoreText.Text += " Game Over!!!!";
            
            restartButton.Visible = true;
            restartButton.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            scoreText.Parent = ground;
            scoreText.BackColor = Color.Transparent;
        }

        private void restartButton_Click(object sender, EventArgs e)
        {
            
            pipeSpeed = 6;
            gravity = 5;
            score = 0;
            scoreText.Text = "Score: " + score;
            count = 0;
            bird.Location = new Point(53, 175);
            pipeTop.Location = new Point(423, -94);
            pipeBottom.Location = new Point(320, 401);
            clouds1.Location = new Point(198, 115);
            restartButton.Visible = false;
            restartButton.Enabled = false;
            gameTimer.Start();
            
        }



        private void scoreBackground_Click(object sender, EventArgs e)
        {

        }
    }
}
