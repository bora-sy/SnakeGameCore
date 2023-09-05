using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SnakeGameCore
{

    internal class SnakeImages
    {
        public const int sideLength = 10;

        public const int textureWidth = 40;
        public const int textureHeight = 20;


        public Bitmap EmptyGameCell_1 { get; private set; }

        public Bitmap EmptyGameCell_2 { get; private set; }

        public Bitmap Food { get; private set; }

        public ReadOnlyDictionary<SnakeGame.Direction, Bitmap> Head { get; private set; }

        public ReadOnlyDictionary<SnakeGame.Direction, Bitmap> DeadHead { get; private set; }

        public ReadOnlyDictionary<SnakeGame.Direction, Bitmap> Tail { get; private set; }

        public ReadOnlyDictionary<SnakeGame.Direction, Bitmap> TailCorner { get; private set; }

        public ReadOnlyDictionary<SnakeGame.Direction, Bitmap> TailEnd { get; private set; }


        public SnakeImages(Bitmap texture, int multiplier)
        {
            #region INDEXES

            const int index_food = 2;

            const int index_EmptyGameCell1 = 3;
            const int index_EmptyGameCell2 = 7;

            const int index_head = 4;
            const int index_deadHead = 6;

            const int index_tail = 0;
            const int index_tailCorner = 1;
            const int index_tailEnd = 5;

            #endregion


            Bitmap food = getBitmapFromTextureByIndex(texture, index_food);

            Bitmap egc1 = getBitmapFromTextureByIndex(texture, index_EmptyGameCell1);

            Bitmap egc2 = getBitmapFromTextureByIndex(texture, index_EmptyGameCell2);

            Bitmap head_right = getBitmapFromTextureByIndex(texture, index_head);

            Bitmap deadHead_right = getBitmapFromTextureByIndex(texture, index_deadHead);
            
            Bitmap tail_leftRight = getBitmapFromTextureByIndex(texture, index_tail);

            Bitmap tailCorner_leftDown = getBitmapFromTextureByIndex(texture, index_tailCorner);

            Bitmap tailEnd_left = getBitmapFromTextureByIndex(texture, index_tailEnd);

            initWithSeperateBitmaps(food, egc1, egc2, head_right, deadHead_right, tail_leftRight, tailCorner_leftDown, tailEnd_left, multiplier);
        }


        public SnakeImages(Bitmap food, Bitmap egc1, Bitmap egc2, Bitmap head_right, Bitmap deadHead_right, Bitmap tail_leftRight, Bitmap tailCorner_leftDown,Bitmap tailEnd_left, int multiplier)
        {
            initWithSeperateBitmaps(food,egc1,egc2,head_right,deadHead_right,tail_leftRight,tailCorner_leftDown,tailEnd_left,multiplier);
        }


        private void initWithSeperateBitmaps(Bitmap food, Bitmap egc1, Bitmap egc2, Bitmap head_right, Bitmap deadHead_right, Bitmap tail_leftRight, Bitmap tailCorner_leftDown, Bitmap tailEnd_left, int multiplier)
        {
            #region Food
            Food = food.Expand(multiplier);
            #endregion


            #region Empty Game Cell 1
            EmptyGameCell_1 = egc1.Expand(multiplier);
            #endregion

            #region Empty Game Cell 2
            EmptyGameCell_2 = egc2.Expand(multiplier);
            #endregion


            #region Head
            Bitmap _head_right = head_right.Expand(multiplier);
            Bitmap _head_left = new Bitmap(_head_right);
            Bitmap _head_up = new Bitmap(_head_right);
            Bitmap _head_down = new Bitmap(_head_right);

            _head_left.RotateFlip(RotateFlipType.Rotate180FlipNone);
            _head_up.RotateFlip(RotateFlipType.Rotate270FlipNone);
            _head_down.RotateFlip(RotateFlipType.Rotate90FlipNone);

            Head = new ReadOnlyDictionary<SnakeGame.Direction, Bitmap>(new Dictionary<SnakeGame.Direction, Bitmap>()
                {
                    {SnakeGame.Direction.Up, _head_up},
                    {SnakeGame.Direction.Down, _head_down},
                    {SnakeGame.Direction.Left, _head_left},
                    {SnakeGame.Direction.Right, _head_right}
                });
            #endregion

            #region Dead Head
            Bitmap _deadHead_right = deadHead_right.Expand(multiplier);
            Bitmap _deadHead_left = new Bitmap(_deadHead_right);
            Bitmap _deadHead_up = new Bitmap(_deadHead_right);
            Bitmap _deadHead_down = new Bitmap(_deadHead_right);

            _deadHead_left.RotateFlip(RotateFlipType.Rotate180FlipNone);
            _deadHead_up.RotateFlip(RotateFlipType.Rotate270FlipNone);
            _deadHead_down.RotateFlip(RotateFlipType.Rotate90FlipNone);

            DeadHead = new ReadOnlyDictionary<SnakeGame.Direction, Bitmap>(new Dictionary<SnakeGame.Direction, Bitmap>()
                {
                    {SnakeGame.Direction.Up, _deadHead_up},
                    {SnakeGame.Direction.Down, _deadHead_down},
                    {SnakeGame.Direction.Left, _deadHead_left},
                    {SnakeGame.Direction.Right, _deadHead_right}
                });
            #endregion


            #region Tail
            Bitmap _tail_leftRight = tail_leftRight.Expand(multiplier);
            Bitmap _tail_upDown = new Bitmap(_tail_leftRight);
            _tail_upDown.RotateFlip(RotateFlipType.Rotate90FlipNone);

            Tail = new ReadOnlyDictionary<SnakeGame.Direction, Bitmap>(new Dictionary<SnakeGame.Direction, Bitmap>()
                {
                    { SnakeGame.Direction.Up, _tail_upDown},
                    { SnakeGame.Direction.Down, _tail_upDown},
                    { SnakeGame.Direction.Left, _tail_leftRight},
                    { SnakeGame.Direction.Right, _tail_leftRight}
                });
            #endregion

            #region Tail Corner
            Bitmap _tailCorner_leftDown = tailCorner_leftDown.Expand(multiplier);

            Bitmap _tailCorner_downRight = new Bitmap(_tailCorner_leftDown);
            Bitmap _tailCorner_rightUp = new Bitmap(_tailCorner_leftDown);
            Bitmap _tailCorner_upLeft = new Bitmap(_tailCorner_leftDown);

            _tailCorner_downRight.RotateFlip(RotateFlipType.Rotate270FlipNone);
            _tailCorner_rightUp.RotateFlip(RotateFlipType.Rotate180FlipNone);
            _tailCorner_upLeft.RotateFlip(RotateFlipType.Rotate90FlipNone);

            TailCorner = new ReadOnlyDictionary<SnakeGame.Direction, Bitmap>(new Dictionary<SnakeGame.Direction, Bitmap>()
                {
                    {(SnakeGame.Direction.Left | SnakeGame.Direction.Down), _tailCorner_leftDown },
                    {(SnakeGame.Direction.Down | SnakeGame.Direction.Right), _tailCorner_downRight },
                    {(SnakeGame.Direction.Right | SnakeGame.Direction.Up), _tailCorner_rightUp },
                    {(SnakeGame.Direction.Up | SnakeGame.Direction.Left), _tailCorner_upLeft }
                });
            #endregion

            #region Tail End
            Bitmap _tailEnd_left = tailEnd_left.Expand(multiplier);

            Bitmap _tailEnd_right = new Bitmap(_tailEnd_left);
            Bitmap _tailEnd_up = new Bitmap(_tailEnd_left);
            Bitmap _tailEnd_down = new Bitmap(_tailEnd_left);

            _tailEnd_right.RotateFlip(RotateFlipType.Rotate180FlipNone);
            _tailEnd_up.RotateFlip(RotateFlipType.Rotate90FlipNone);
            _tailEnd_down.RotateFlip(RotateFlipType.Rotate270FlipNone);


            TailEnd = new ReadOnlyDictionary<SnakeGame.Direction, Bitmap>(new Dictionary<SnakeGame.Direction, Bitmap>()
                {
                    {SnakeGame.Direction.Up, _tailEnd_up},
                    {SnakeGame.Direction.Down, _tailEnd_down},
                    {SnakeGame.Direction.Left, _tailEnd_left},
                    {SnakeGame.Direction.Right, _tailEnd_right}
                });
            #endregion

        }


        private Bitmap getBitmapFromTextureByIndex(Bitmap texture, int index)
        {
            const int bitmapCountPerRow = textureWidth / sideLength;

            int totalPixelIndex = index * sideLength;

            int y_index = (totalPixelIndex / textureWidth) * sideLength;
            int x_index = totalPixelIndex - (y_index * bitmapCountPerRow);

            Bitmap cropped = texture.Crop(x_index, y_index, sideLength, sideLength);

            return cropped;
            /*
             * 0 -> 0,0
             * 1 -> 10,0
             * 2 -> 20,0
             * 3 -> 0,10
             * 4 -> 10,10
             * 5 -> 20,10
             */

        }
    }
}
