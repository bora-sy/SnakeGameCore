using System;
using System.Collections.Generic;
using System.Drawing;
using static SnakeGameCore.SnakeGame;

namespace SnakeGameCore
{
    internal class SnakeGrid
    {
        //private PictureBox PB;

        private Graphics graphics;
        //private Bitmap img;

        public int sideGameCellCount { get; private set; }
        private int gameCellSideLength;

        private SnakeImages Images;
        public Bitmap GridImage { get; private set; }
        public SnakeGrid(int grid_sideLength, int gameCell_sideLength, SnakeTexture images)
        {
            if (grid_sideLength % gameCell_sideLength != 0) throw new ArgumentException("grid_sideLength must be divisible by gameCell_sideLength");
            if (gameCell_sideLength % SnakeImages.sideLength != 0) throw new ArgumentException($"The game cell side length must be divisible by {SnakeImages.sideLength}");
            if (gameCell_sideLength < SnakeImages.sideLength) throw new ArgumentException($"gameCell_sideLength cannot be less than {SnakeImages.sideLength}");

            this.GridImage = new Bitmap(grid_sideLength, grid_sideLength);
            this.graphics = Graphics.FromImage(GridImage);
            this.sideGameCellCount = grid_sideLength / gameCell_sideLength;
            this.gameCellSideLength = gameCell_sideLength;

            this.Images = images.getImagesFromTexture(gameCell_sideLength / SnakeImages.sideLength);


            //PB.Image = img;

            resetImage();
        }

        ~SnakeGrid()
        {
            graphics.Dispose();
        }

        public void redrawGrid(Point foodPos, List<SnakeGame.SnakeCell> snake, SnakeGame.Direction currentHeadDirection)
        {

            resetImage();

            setCell(foodPos, CellState.Food);

            for (int i = 0; i < snake.Count; i++)
            {
                var snakeCell = snake[i];

                if (snakeCell.state == SnakeCellState.Head)
                {
                    setCell(snakeCell.location, CellState.Snake_Head, currentHeadDirection);
                }
                else if (i == snake.Count - 1)
                {
                    Point previousSnakeCellLocation = snake[i - 1].location;
                    Direction? tailEndDirection = getDirectionFromBase(snakeCell.location, previousSnakeCellLocation);
                    setCell(snakeCell.location, CellState.Snake_TailEnd, tailEndDirection ?? Direction.Up);
                }
                else
                {
                    Point previousSnakeCellLocation = snake[i - 1].location;
                    Point nextSnakeCellLocation = snake[i + 1].location;


                    if ((previousSnakeCellLocation.X != nextSnakeCellLocation.X && (previousSnakeCellLocation.Y) != nextSnakeCellLocation.Y))
                    {
                        Direction? cornerDirection = 0;

                        cornerDirection |= getDirectionFromBase(snakeCell.location, nextSnakeCellLocation);
                        cornerDirection |= getDirectionFromBase(snakeCell.location, previousSnakeCellLocation);

                        setCell(snakeCell.location, CellState.Snake_TailCorner, cornerDirection);
                    }
                    else
                    {
                        Direction tailDirection = previousSnakeCellLocation.Y == nextSnakeCellLocation.Y ? Direction.Left : Direction.Up; 
                        setCell(snakeCell.location, CellState.Snake_Tail, tailDirection);
                    }
                }
            }

            Direction? getDirectionFromBase(Point basePoint, Point otherPoint)
            {
                if (otherPoint.X - 1 == basePoint.X) return Direction.Right;
                else if (otherPoint.X + 1 == basePoint.X) return Direction.Left;

                else if (otherPoint.Y - 1 == basePoint.Y) return Direction.Down;
                else if (otherPoint.Y + 1 == basePoint.Y) return Direction.Up;

                else return null;
            }
        }

        public void resetImage()
        {
            for (int x = 0; x < sideGameCellCount; x++)
            {
                for (int y = 0; y < sideGameCellCount; y++)
                {
                    Point p = new Point(x, y);
                    setCell(p, CellState.Empty);
                }
            }
        }

        public void setCell(Point gameCell, CellState state, Direction? direction = null)
        {
            switch (state)
            {
                case CellState.Empty: setEmpty(); break;

                case CellState.Food: setFood(); break;

                case CellState.Snake_Head: setSnakeHead(); break;
                case CellState.Snake_DeadHead: setSnakeDeadHead(); break;

                case CellState.Snake_Tail: setSnakeTail(); break;
                case CellState.Snake_TailEnd: setSnakeTailEnd(); break;
                case CellState.Snake_TailCorner: setSnakeTailCorner(); break;
            }


            void setEmpty()
            {
                bool x_even = gameCell.X % 2 == 0;
                bool y_even = gameCell.Y % 2 == 0;

                if (x_even == y_even)
                    drawBitmap(gameCell, Images.EmptyGameCell_1);
                //drawRectangle(gameCell, Colors.emptyPixelColor_1);
                else
                    drawBitmap(gameCell, Images.EmptyGameCell_2);
                //drawRectangle(gameCell, Colors.emptyPixelColor_2);
            }

            void setFood() => drawBitmap(gameCell, Images.Food);

            void setSnakeHead()
            {
                drawBitmap(gameCell, Images.Head[direction.Value]);
                // drawRectangle(gameCell, Colors.snakeHeadColor);
            }

            void setSnakeDeadHead()
            {
                drawBitmap(gameCell, Images.DeadHead[direction.Value]);
                // drawRectangle(gameCell, Colors.snakeHeadColor);
            }

            void setSnakeTail()
            {
                drawBitmap(gameCell, Images.Tail[direction.Value]);
                //drawRectangle(gameCell, Colors.snakeTailColor);
            }

            void setSnakeTailEnd()
            {
                drawBitmap(gameCell, Images.TailEnd[direction.Value]);
            }

            void setSnakeTailCorner()
            {
                drawBitmap(gameCell, Images.TailCorner[direction.Value]);
            }

        }

        private void drawBitmap(Point gamePixel, Bitmap bmp)
        {
            int startXindex = gamePixel.X * gameCellSideLength;
            int startYindex = gamePixel.Y * gameCellSideLength;

            graphics.DrawImage(bmp, startXindex, startYindex);
        }


        public enum CellState
        {
            Empty = 0,

            Food,

            Snake_Head,
            Snake_DeadHead,

            Snake_Tail,
            Snake_TailEnd,
            Snake_TailCorner

        };
    }
}
