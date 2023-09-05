using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SnakeGameCore
{
    public class SnakeGame
    {
        private Direction nextDirection;
        private Direction lastDirection;

        private List<SnakeCell> snake = new List<SnakeCell>();

        private Point foodPos = new Point(-1, -1);

        private bool isGameOver = false;

        private SnakeGrid grid;
        private Random rnd = new Random();

        private int maxSnakeLength = -1;

        public event EventHandler<EventArgs> OnGameWin;
        public event EventHandler<EventArgs> OnGameLose;
        public event EventHandler<EventArgs> OnFoodEaten;

        public Bitmap GridImage 
        { 
            get
            {
                return this.grid.GridImage;
            } 
        }
        public SnakeGame(SnakeConfiguration config)
        {
            if(config == null) throw new ArgumentNullException(nameof(config));

            this.grid = new SnakeGrid(config.grid_sideLength, config.gameCell_sideLength, config.texture);

            if (config.tailLengthOnStart + 1 > getMaxSnakeLength()) throw new Exception("The tail is too long");

            snake.Add(new SnakeCell(new Point(0, 0), SnakeCellState.Head));

            for(int i = 0 - config.tailLengthOnStart; i < 0; i++)
            snake.Add(new SnakeCell(new Point(0, i), SnakeCellState.Tail));

            GenerateFood(false);

            nextDirection = Direction.Down;
            lastDirection = nextDirection;
            redrawGrid();
        }

        private int getMaxSnakeLength()
        {
            maxSnakeLength = (int)Math.Pow(grid.sideGameCellCount, 2);
            return maxSnakeLength;
        }

        public bool SetNextDirection(Direction direction)
        {
            Point headPoint = snake.Where(x => x.state == SnakeCellState.Head).First().location;
            Point nextCell = calculateNextCellByDirection(headPoint, direction);

            if (snake[1].location == nextCell) return false;

            nextDirection = direction;

            return true;
        }

        internal enum SnakeCellState
        {
            Head,
            Tail
        }

        [Flags]
        public enum Direction
        {
            Up = 1,
            Down = 2,
            Left = 4,
            Right = 8
        }


        public void Tick()
        {
            if(isGameOver) return;

            Point? nextCellToMove = getNextCellToMove();

            if (nextCellToMove == null)
            {
                isGameOver = true;
                HandleGameLose();
                return;
            }

            lastDirection = nextDirection;


            SnakeCell HeadPixel = snake[0];

            Point nextPos = new Point(HeadPixel.location.X, HeadPixel.location.Y);

            HeadPixel.location = nextCellToMove.Value;

            if (foodPos == nextCellToMove)
            {
                SnakeCell lastSnakePixel = snake[snake.Count - 1];

                SnakeCell newPixel = new SnakeCell(lastSnakePixel.location,SnakeCellState.Tail);

                snake.Add(newPixel);

                if(OnFoodEaten != null) OnFoodEaten.Invoke(this, new EventArgs());
            }

            for (int i = 1; i < snake.Count; i++) // Skip the first element, head
            {
                SnakeCell snakePixel = snake[i];
                Point oldPos = new Point(snakePixel.location.X, snakePixel.location.Y);

                snakePixel.location = nextPos;

                nextPos = oldPos;
            }

            if (snake.Count >= maxSnakeLength)
            {
                isGameOver = true;
                foodPos = new Point(-1, -1);
                redrawGrid();
                HandleGameWin();
            }
            else
            {
                if (foodPos == nextCellToMove) GenerateFood(false);
                redrawGrid();
            }
        }

        void HandleGameLose()
        {
            SnakeCell head = snake.Where(x => x.state == SnakeCellState.Head).First();
            grid.setCell(head.location, SnakeGrid.CellState.Snake_DeadHead, lastDirection);

            if (OnGameLose != null) OnGameLose(this, new EventArgs());
        }

        void HandleGameWin()
        {
            if (OnGameWin != null) OnGameWin(this, new EventArgs());
        }



        private void redrawGrid()
        {
            grid.redrawGrid(foodPos, snake, lastDirection);
        }

        private Point? getNextCellToMove()
        {
            Point head = snake.Where(x => x.state == SnakeCellState.Head).First().location;


            Point nextCellPoint = calculateNextCellByDirection(head, nextDirection);

            // Wall Check
            if (nextCellPoint.X < 0 || nextCellPoint.Y < 0 || nextCellPoint.X >= grid.sideGameCellCount || nextCellPoint.Y >= grid.sideGameCellCount)
            {
                return null;
            }


            // Tail Check
            if (snake.Where(x => x.location == nextCellPoint && x.state == SnakeCellState.Tail).FirstOrDefault() != null && snake.Last().location != nextCellPoint)
            {
                return null;
            }

            return nextCellPoint;


        }

        private Point calculateNextCellByDirection(Point basePoint,  Direction direction)
        {
            int newX = basePoint.X;
            int newY = basePoint.Y;

            switch (direction)
            {
                case Direction.Up: newY--; break;
                case Direction.Down: newY++; break;
                case Direction.Left: newX--; break;
                case Direction.Right: newX++; break;
            }

            return new Point(newX, newY);
        }


        private void GenerateFood(bool UpdatePB = false)
        {
            Point newFoodPos;

            do
            {
                int x = rnd.Next(grid.sideGameCellCount);
                int y = rnd.Next(grid.sideGameCellCount);
                newFoodPos = new Point(x, y);

            } while (snake.Where(x => x.location == newFoodPos).FirstOrDefault() != null || foodPos == newFoodPos);

            foodPos = newFoodPos;

            if(UpdatePB)
                redrawGrid();

        }

        internal class SnakeCell
        {
            public SnakeCell(Point loc, SnakeCellState state)
            {
                this.location = loc;
                this.state = state;
            }
            public Point location;
            public SnakeCellState state;
        }
    }


}
