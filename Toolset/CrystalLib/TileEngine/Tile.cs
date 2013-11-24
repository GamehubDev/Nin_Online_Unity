using System;

namespace CrystalLib.TileEngine
{
    public class Tile : ICloneable
    {
        #region Property Region

        public int X { get; set; }
        public int Y { get; set; }
        public int Tileset { get; set; }
        public int SrcX { get; set; }
        public int SrcY { get; set; }
        public int Terrain { get; set; }

        #endregion

        #region Constructor Region

        /// <summary>
        /// Initializes a new instance of the <see cref="Tile"/> class.
        /// </summary>
        public Tile()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tile"/> class.
        /// </summary>
        /// <param name="x">X co-ordinate of the tile.</param>
        /// <param name="y">Y co-ordinate of the tile.</param>
        /// <param name="tileset">Tileset of the tile.</param>
        /// <param name="srcX">X co-ordinate of the TextureRect.</param>
        /// <param name="srcY">Y co-ordinate of the TextureRect.</param>
        /// <param name="terrain">Terrain of the tile.</param>
        public Tile(int x, int y, int tileset, int srcX, int srcY, int terrain)
        {
            X = x;
            Y = y;
            Tileset = tileset;
            SrcX = srcX;
            SrcY = srcY;
            Terrain = terrain;
        }

        #endregion

        #region Serialization Region

        /// <summary>
        /// Clones the <see cref="Tile"/> object.
        /// </summary>
        /// <returns>Copy of the object.</returns>
        public Tile Clone()
        {
            return (Tile)MemberwiseClone();
        }

        /// <summary>
        /// Implements method from <see cref="ICloneable"/> interface.
        /// </summary>
        /// <returns></returns>
        object ICloneable.Clone()
        {
            return Clone();
        }

        #endregion
    }
}
