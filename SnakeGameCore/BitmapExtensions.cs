using System.Drawing;

namespace SnakeGameCore
{
    internal static class BitmapExtensions
    {
        public static Bitmap Crop(this Bitmap source, int index_X, int index_Y, int width, int height)
        {
            Rectangle cropRectangle = new Rectangle(index_X, index_Y, width, height);
            Bitmap croppedBitmap = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(croppedBitmap))
            {
                g.DrawImage(source, new Rectangle(0, 0, croppedBitmap.Width, croppedBitmap.Height), cropRectangle, GraphicsUnit.Pixel);
            }

            return croppedBitmap;
        }

        public static Bitmap Expand(this Bitmap bmp, int multiplier)
        {
            if (multiplier == 0) return bmp;

            int newWidth = bmp.Width * multiplier;
            int newHeight = bmp.Height * multiplier;
            Bitmap resizedBitmap = new Bitmap(newWidth, newHeight);

            for (int y_index = 0; y_index < bmp.Height; y_index++)
            {
                for (int x_index = 0; x_index < bmp.Width; x_index++)
                {
                    Color color = bmp.GetPixel(x_index, y_index);

                    int startindex_x = x_index * multiplier;
                    int startindex_y = y_index * multiplier;

                    for (int y = 0; y < multiplier; y++)
                    {
                        for (int x = 0; x < multiplier; x++)
                        {
                            resizedBitmap.SetPixel(startindex_x + x, startindex_y + y, color);
                        }
                    }
                }
            }
            return resizedBitmap;
        }
    }
}
