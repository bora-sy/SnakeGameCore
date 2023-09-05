using System;
using System.Drawing;

namespace SnakeGameCore
{
    public class SnakeTexture
    {
        Bitmap mainTexture = null;

        public SnakeTexture(Bitmap texture)
        {
            if (texture == null) throw new ArgumentNullException(nameof(texture));
            if (texture.Width != SnakeImages.textureWidth || texture.Height != SnakeImages.textureHeight) throw new ArgumentException($"Texture image has the incorrect size. It must be {SnakeImages.textureWidth}x{SnakeImages.textureHeight}");
            this.mainTexture = texture;
        }

        internal SnakeImages getImagesFromTexture(int multiplier)
        {
            return new SnakeImages(mainTexture, multiplier);
        }


    }
}
