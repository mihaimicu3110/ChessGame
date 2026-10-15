using System;
using System.Drawing;
using System.Windows.Forms;
using System.Media;

namespace ChessGame
{
    public partial class Form1 : Form
    {
        private Button selectedButton = null;
        private Color originalColor;
        private bool gameStarted = false;
        private string currentTurn = "white";
        private int whiteTime = 600;
        private int blackTime = 600;

        public Form1()
        {
            InitializeComponent();

            SetPieces();
            SetButtonEvents();

            lblWhiteTime.Text = "10:00";
            lblBlackTime.Text = "10:00";


        }

        private void SetPieces()
        {
           
            btnA8.BackgroundImage = Properties.Resources.black_rook;
            btnA8.Tag = "black";
            btnB8.BackgroundImage = Properties.Resources.black_knight;
            btnB8.Tag = "black";
            btnC8.BackgroundImage = Properties.Resources.black_bishop;
            btnC8.Tag = "black";
            btnD8.BackgroundImage = Properties.Resources.black_queen;
            btnD8.Tag = "black";
            btnE8.BackgroundImage = Properties.Resources.black_king;
            btnE8.Tag = "black_king";
            btnF8.BackgroundImage = Properties.Resources.black_bishop;
            btnF8.Tag = "black";
            btnG8.BackgroundImage = Properties.Resources.black_knight;
            btnG8.Tag = "black";
            btnH8.BackgroundImage = Properties.Resources.black_rook;
            btnH8.Tag = "black";

            btnA7.BackgroundImage = Properties.Resources.black_pawn;
            btnA7.Tag = "black";
            btnB7.BackgroundImage = Properties.Resources.black_pawn;
            btnB7.Tag = "black";
            btnC7.BackgroundImage = Properties.Resources.black_pawn;
            btnC7.Tag = "black";
            btnD7.BackgroundImage = Properties.Resources.black_pawn;
            btnD7.Tag = "black";
            btnE7.BackgroundImage = Properties.Resources.black_pawn;
            btnE7.Tag = "black";
            btnF7.BackgroundImage = Properties.Resources.black_pawn;
            btnF7.Tag = "black";
            btnG7.BackgroundImage = Properties.Resources.black_pawn;
            btnG7.Tag = "black";
            btnH7.BackgroundImage = Properties.Resources.black_pawn;
            btnH7.Tag = "black";

            btnA2.BackgroundImage = Properties.Resources.white_pawn;
            btnA2.Tag = "white";
            btnB2.BackgroundImage = Properties.Resources.white_pawn;
            btnB2.Tag = "white";
            btnC2.BackgroundImage = Properties.Resources.white_pawn;
            btnC2.Tag = "white";
            btnD2.BackgroundImage = Properties.Resources.white_pawn;
            btnD2.Tag = "white";
            btnE2.BackgroundImage = Properties.Resources.white_pawn;
            btnE2.Tag = "white";
            btnF2.BackgroundImage = Properties.Resources.white_pawn;
            btnF2.Tag = "white";
            btnG2.BackgroundImage = Properties.Resources.white_pawn;
            btnG2.Tag = "white";
            btnH2.BackgroundImage = Properties.Resources.white_pawn;
            btnH2.Tag = "white";

            btnA1.BackgroundImage = Properties.Resources.white_rook;
            btnA1.Tag = "white";
            btnB1.BackgroundImage = Properties.Resources.white_knight;
            btnB1.Tag = "white";
            btnC1.BackgroundImage = Properties.Resources.white_bishop;
            btnC1.Tag = "white";
            btnD1.BackgroundImage = Properties.Resources.white_queen;
            btnD1.Tag = "white";
            btnE1.BackgroundImage = Properties.Resources.white_king;
            btnE1.Tag = "white_king";
            btnF1.BackgroundImage = Properties.Resources.white_bishop;
            btnF1.Tag = "white";
            btnG1.BackgroundImage = Properties.Resources.white_knight;
            btnG1.Tag = "white";
            btnH1.BackgroundImage = Properties.Resources.white_rook;
            btnH1.Tag = "white";


            SetImageLayout();
        }

        private void SetImageLayout()
        {
            foreach (Control control in this.Controls)
            {
                if (control is Button button)
                {
                    button.BackgroundImageLayout = ImageLayout.Zoom;
                }
            }
        }

        private void SetButtonEvents()
        {
            foreach (Control control in this.Controls)
            {
                if (control is Button button)
                {
                    if (button != btnStart && button != btnRestart && button != btnExit)
                    {
                        button.Click += Square_Click;
                    }
                }
            }
        }

        private void Square_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            if (!gameStarted)
            {
                return;
            }

            if (selectedButton == null)
            {
                if (clickedButton.BackgroundImage != null &&
                    GetPieceColor(clickedButton) == currentTurn)
                {
                    selectedButton = clickedButton;

                    originalColor = selectedButton.BackColor;
                    selectedButton.BackColor = Color.Yellow;
                }

                return;
            }

            if (clickedButton.BackgroundImage != null &&
                GetPieceColor(clickedButton) == GetPieceColor(selectedButton))
            {
                selectedButton.BackColor = originalColor;

                selectedButton = clickedButton;

                originalColor = selectedButton.BackColor;
                selectedButton.BackColor = Color.Yellow;

                return;
            }

            string capturedPiece = "";

            if (clickedButton.Tag != null)
            {
                capturedPiece = clickedButton.Tag.ToString();
            }

            Button oldButton = selectedButton;

            clickedButton.BackgroundImage = oldButton.BackgroundImage;
            clickedButton.Tag = oldButton.Tag;

            oldButton.BackgroundImage = null;
            oldButton.Tag = null;

            oldButton.BackColor = originalColor;

            selectedButton = null;

            if (capturedPiece == "black_king")
            {
                timerWhite.Stop();
                gameStarted = false;

                PlayGameOverSound();

                MessageBox.Show(
                    "GAME OVER\nWHITE WINS!",
                    "Game Over",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }

            if (capturedPiece == "white_king")
            {
                timerWhite.Stop();
                gameStarted = false;

                PlayGameOverSound();

                MessageBox.Show(
                    "GAME OVER\nBLACK WINS!",
                    "Game Over",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }

            if (currentTurn == "white")
            {
                currentTurn = "black";
            }
            else
            {
                currentTurn = "white";
            }
        }

        private void ClearBoard()
        {
            foreach (Control control in this.Controls)
            {
                if (control is Button button)
                {
                    if (button != btnRestart &&
                        button != btnStart &&
                        button != btnExit)
                    {
                        button.BackgroundImage = null;
                        button.Tag = null;
                    }
                }
            }
        }

        private string GetPieceColor(Button button)
        {
            if (button.Tag == null)
            {
                return "";
            }

            string tag = button.Tag.ToString();

            if (tag.StartsWith("white"))
            {
                return "white";
            }

            if (tag.StartsWith("black"))
            {
                return "black";
            }

            return "";
        }

        private void PlayGameOverSound()
        {
            string path = Application.StartupPath + @"\gameover.wav";

            SoundPlayer player = new SoundPlayer(path);
            player.Play();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            if (selectedButton != null)
            {
                selectedButton.BackColor = originalColor;
                selectedButton = null;
            }

            timerWhite.Stop();

            whiteTime = 600;
            blackTime = 600;

            lblWhiteTime.Text = "10:00";
            lblBlackTime.Text = "10:00";

            currentTurn = "white";
            gameStarted = false;

            ClearBoard();
            SetPieces();

            btnStart.Enabled = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void timerWhite_Tick(object sender, EventArgs e)
        {
            if (currentTurn == "white")
            {
                if (whiteTime > 0)
                {
                    whiteTime--;

                    int minutes = whiteTime / 60;
                    int seconds = whiteTime % 60;

                    lblWhiteTime.Text =
                        minutes.ToString("00") + ":" +
                        seconds.ToString("00");
                }

                if (whiteTime == 0)
                {
                    timerWhite.Stop();
                    gameStarted = false;

                    PlayGameOverSound();

                    MessageBox.Show(
                        "GAME OVER\nBLACK WINS!",
                        "Game Over",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            else
            {
                if (blackTime > 0)
                {
                    blackTime--;

                    int minutes = blackTime / 60;
                    int seconds = blackTime % 60;

                    lblBlackTime.Text =
                        minutes.ToString("00") + ":" +
                        seconds.ToString("00");
                }

                if (blackTime == 0)
                {
                    timerWhite.Stop();
                    gameStarted = false;

                    PlayGameOverSound();

                    MessageBox.Show(
                        "GAME OVER\nWHITE WINS!",
                        "Game Over",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            gameStarted = true;
            currentTurn = "white";

            timerWhite.Start();

            btnStart.Enabled = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}