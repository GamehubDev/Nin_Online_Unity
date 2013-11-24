using System;
using System.Collections.Generic;

namespace CrystalLib.TileEngine
{
    public class TileLayer : ICloneable
    {
        #region Property Region

        public string Name { get; set; }
        public int Opacity { get; set; }
        public bool Visible { get; set; }
        public List<Tile> Tiles { get; set; }

        #endregion

        #region Constructor Region

        /// <summary>
        /// Initializes a new instance of the <see cref="TileLayer"/> class.
        /// </summary>
        public TileLayer()
        {
            Tiles = new List<Tile>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TileLayer"/> class.
        /// </summary>
        /// <param name="name">Name of the layer.</param>
        /// <param name="opacity">Opacity of the layer.</param>
        public TileLayer(string name, int opacity, bool visible)
        {
            Tiles = new List<Tile>();

            Name = name;
            Opacity = opacity;
            Visible = visible;
        }

        #endregion

        #region Serialization Region

        /// <summary>
        /// Clones the <see cref="TileLayer"/> object.
        /// </summary>
        /// <returns>Copy of the object.</returns>
        public TileLayer Clone()
        {
            return (TileLayer)MemberwiseClone();
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