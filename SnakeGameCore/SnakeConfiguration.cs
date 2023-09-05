using System;
using System.Drawing;

namespace SnakeGameCore
{
    public class SnakeConfiguration
    {

        internal SnakeTexture texture { get; private set; }

        internal int grid_sideLength { get; private set; }
        internal int gameCell_sideLength { get; private set; }

        internal int tailLengthOnStart { get; private set; }

        public SnakeConfiguration(SnakeTexture Texture, int Grid_SideLength, int GameCell_SideLength, int TailLengthOnStart = 2)
        {
            if (Texture == null) throw new ArgumentNullException(nameof(Texture));

            if (Grid_SideLength < 1) throw new ArgumentOutOfRangeException(nameof(Grid_SideLength));
            if (GameCell_SideLength < 1) throw new ArgumentOutOfRangeException(nameof(GameCell_SideLength));
            if (TailLengthOnStart < 1) throw new ArgumentOutOfRangeException(nameof(TailLengthOnStart));

            this.texture = Texture;
            this.grid_sideLength = Grid_SideLength;
            this.gameCell_sideLength = GameCell_SideLength;
            this.tailLengthOnStart = TailLengthOnStart;
        }
    }
}
