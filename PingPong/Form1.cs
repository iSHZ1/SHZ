using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PingPong
{
    public partial class Form1 : Form
    {
        private int speedVertical = 3;
        private int speedHorizon = 3;
        private int score = 0;

        public Form1()
        {

            InitializeComponent();



            timer.Enabled = true;
            Cursor.Hide();
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
            this.Bounds = Screen.PrimaryScreen.Bounds;

            gamePanel.Top = backGround.Bottom - (backGround.Bottom / 10);

            loseLabel.Visible = false;
            loseLabel.Left = (backGround.Width / 2) - (loseLabel.Width / 2);
            loseLabel.Top = (backGround.Height / 2) - (loseLabel.Height / 2);



        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
            if (e.KeyCode == Keys.R)
            {
                gameBall.Top = 50;
                gameBall.Left = 70;
                score = 0;
                speedHorizon = 2;
                speedVertical = 2;
                loseLabel.Visible = false;
                timer.Enabled = true;
                result.Text = "Результат: 0";
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {


            gamePanel.Left = Cursor.Position.X - (gamePanel.Width / 2);

            gameBall.Left += speedHorizon;
            gameBall.Top += speedVertical;

            if (gameBall.Left <= backGround.Left)
                speedHorizon *= -1;


            if (gameBall.Right >= backGround.Right)
                speedHorizon *= -1;

            if (gameBall.Top <= backGround.Top)
                speedVertical *= -1;

            if (gameBall.Bottom >= gamePanel.Top && gameBall.Bottom <= gamePanel.Bottom 
                && gameBall.Right >= gamePanel.Left && gameBall.Left <= gamePanel.Right)
            {
                speedHorizon += 3;
                speedVertical += 2;
                speedVertical *= -1;
                score++;
                result.Text = "Результат: " + score.ToString();
            }

            if (gameBall.Bottom >= backGround.Bottom)
            { 
                loseLabel.Visible = true;
                timer.Enabled = false;
            }
                


            //Random randColor = new Random();
            //backGround.BackColor = Color.FromArgb(randColor.Next(150, 255), randColor.Next(150, 255), randColor.Next(150, 255));   // при отбивании меняется цвет бекграунда


        }
    }
}
