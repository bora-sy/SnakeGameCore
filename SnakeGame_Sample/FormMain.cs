using SnakeGameCore;

namespace SnakeGame_Sample
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            // When set to true, instead of the focused inner control receiving the key events, the form will receive all of them.
            this.KeyPreview = true;

            // For receiving key strokes to change snake direction later on
            this.KeyDown += FormMain_KeyDown;
        }

        // Main Snake Game
        SnakeGame Game;

        // Form's load event, to initialize the SnakeGame
        private void FormMain_Load(object sender, EventArgs e)
        {
            Bitmap textureFile = (Bitmap)Bitmap.FromFile(@"Your_Path_To_Texture_File");

            SnakeTexture texture = new SnakeTexture(textureFile);

            SnakeConfiguration config = new SnakeConfiguration(texture, 500, 50, 2);

            Game = new SnakeGame(config);


            Game.OnGameLose += Game_OnGameLose;
            Game.OnGameWin += Game_OnGameWin;
            Game.OnFoodEaten += Game_OnFoodEaten;

            RefreshPictureBox();
        }



        // Start button's click event, to trigger the timer
        private void buttonStart_Click(object sender, EventArgs e)
        {
            buttonStart.Enabled = false;
            timerMain.Start();
        }


        // Timer's tick event, to move the snake
        private void timerMain_Tick(object sender, EventArgs e)
        {
            Game.Tick();
            RefreshPictureBox();
        }


        // The method that updates the picturebox with the latest grid image
        private void RefreshPictureBox()
        {
            pictureBoxMain.Image = Game.GridImage;
        }


        // The event that receives the key strokes to chnage snake direction
        private void FormMain_KeyDown(object? sender, KeyEventArgs e)
        {
            SnakeGame.Direction? direction;

            switch (e.KeyCode)
            {
                case Keys.W: direction = SnakeGame.Direction.Up; break;
                case Keys.S: direction = SnakeGame.Direction.Down; break;
                case Keys.A: direction = SnakeGame.Direction.Left; break;
                case Keys.D: direction = SnakeGame.Direction.Right; break;
                default: direction = null; break;
            }

            if (direction == null) return;


            Game.SetNextDirection(direction.Value);


            e.Handled = true;
            e.SuppressKeyPress = true;
        }


        // Game Events

        private void Game_OnGameLose(object? sender, EventArgs e)
        {
            MessageBox.Show("You Lost!");
        }
        private void Game_OnGameWin(object? sender, EventArgs e)
        {
            MessageBox.Show("You Won!");
        }

        int pointCounter = 0;
        private void Game_OnFoodEaten(object? sender, EventArgs e)
        {
            labelPoints.Text = (++pointCounter).ToString();
        }


    }
}