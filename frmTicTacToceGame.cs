using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tic_Tac_Toe_Game.Properties;

namespace Tic_Tac_Toe_Game
{
    public partial class frmTicTacToceGame : Form
    {
        byte counter = 1 , turn = 1; string player = "Player 1";
        public frmTicTacToceGame()
        {
            InitializeComponent();
        }

        private void frmTicTacToceGame_Paint(object sender, PaintEventArgs e)
        {
            Color color = ColorTranslator.FromHtml("#2C3E50");
            Pen pen = new Pen(color);
            pen.Width = 10;
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            e.Graphics.DrawLine(pen, 500, 100, 500, 600);
            e.Graphics.DrawLine(pen, 720, 100, 720, 600);

            e.Graphics.DrawLine(pen, 280, 250, 925, 250);
            e.Graphics.DrawLine(pen, 280, 440, 925, 440);
        }

        private void frmTicTacToceGame_Load(object sender, EventArgs e)
        {
            lbTurn.Text = "Turn 1";
            lbPlayer.Text = "Player   Player1";
        }

        private void button_click(object sender, EventArgs e)
        {
            try
            {
                if (counter % 2 == 0) lbPlayer.Text = "Player:   Player1";
                else lbPlayer.Text = "Player   Player2";
                counter++;
                

                changImage((Button)sender);
            }
            catch { 
            }
        }

        private void changImage (object sender)
        {
            Button btn = ( Button )sender;
            switch (player)
            {
                case "Player 1":
                    if (btn.Tag == "?")
                    {
                        btn.Image = Resources.X;
                        btn.Tag = "x";
                        player = "Player 2";
                        CheckWinCondition();
                        if (counter > 9)
                        {
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("This cell is already taken.\nPlease choose an empty one.","Invalid Move",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    }
                    break;

                case "Player 2":
                    if (btn.Tag == "?")
                    {
                        btn.Image = Resources.O;
                        btn.Tag = "o";
                        player = "Player 1";
                        CheckWinCondition();
                        if (counter > 9)
                        {
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("This cell is already taken.\nPlease choose an empty one.", "Invalid Move", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    break;

            }


        }

        private bool CheckWinCondition()
        {
            if (CheckValue(button1, button2, button3)) return true;
            if (CheckValue(button4, button5, button6)) return true;
            if (CheckValue(button7, button8, button9)) return true;
            if (CheckValue(button1, button4, button7)) return true;
            if (CheckValue(button2, button5, button8)) return true;
            if (CheckValue(button3, button6, button9)) return true;
            if (CheckValue(button1, button5, button9)) return true;
            if (CheckValue(button3, button5, button7)) return true;

            return false;
        }

        private bool CheckValue(Button btn1, Button btn2, Button btn3)
        {
            if (btn1.Tag.ToString() != "?" &&btn1.Tag.ToString() == btn2.Tag.ToString() &&btn1.Tag.ToString() == btn3.Tag.ToString())
            {
                // Highlight winner buttons
                btn1.BackColor = Color.YellowGreen;
                btn2.BackColor = Color.YellowGreen;
                btn3.BackColor = Color.YellowGreen;

                // Disable all buttons
                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl is Button b) b.Enabled = false;
                    btnRestarGame.Enabled = true;
                }

                // Winner
                if (btn1.Tag.ToString() == "x")
                {
                    lbWinner.Text = "Winner: Player 1";
                }
                else
                {
                    lbWinner.Text = "Winner: Player 2";
                }

                lbResult.Text = "Game Over";
                MessageBox.Show("Game Over", "Game Over",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                return true; // game finished
            }

            // Check draw only when all 9 moves are played
            if (counter == 10)
            {
                lbWinner.Text = "Draw";
                lbResult.Text = "Game Over";
                MessageBox.Show("Draw! Game Over", "Game Over",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true; // game finished as draw
            }

            return false; // no winner, no draw yet
        }



        private void btnRestarGame_Click(object sender, EventArgs e)
        {
            turn++;
            counter = 1;


            foreach(Control ctrl in this.Controls)
            {
                if (ctrl is Button b) b.Tag = "?";
            }

            foreach(Control ctrl in this.Controls)
            {
                if (ctrl is Button b) b.Enabled = true;
  
            }

            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Button b) b.Image = Resources.question_mark_96;
                btnRestarGame.Image = null;
            }

            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Button b) b.BackColor = ColorTranslator.FromHtml("#0D1117");

            }

            lbResult.Text = "In Progress";
            lbWinner.Text = "Winner";
            lbPlayer.Text = "Player";
            lbTurn.Text = "Turn " + turn;
            player = "Player 1";

        }

    }
}
