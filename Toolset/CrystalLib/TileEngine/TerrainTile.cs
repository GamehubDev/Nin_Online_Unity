using System;
using CrystalLib.Serialization;

namespace CrystalLib.TileEngine
{
    public class TerrainTile : ICloneable
    {
        #region Property Region

        public int ID { get; set; }
        public string Name { get; set; }
        public TerrainType Type { get; set; }
        public int Tileset { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        #endregion

        #region Constructor Region

        /// <summary>
        /// Initializes a new instance of the <see cref="Tileset"/> class.
        /// </summary>
        public TerrainTile()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tileset"/> class.
        /// </summary>
        /// <param name="id">ID of the terrain.</param>
        /// <param name="name">Name of the terrain.</param>
        /// <param name="type">Type of the terrain.</param>
        /// <param name="tileset">Tileset of the terrain.</param>
        /// <param name="x">X co-ordinate of the terrain.</param>
        /// <param name="y">Y co-ordinate of the terrain.</param>
        /// <param name="width">Width of the terrain.</param>
        /// <param name="height">Height of the terrain.</param>
        public TerrainTile(int id, string name, TerrainType type, int tileset, int x, int y, int width, int height)
        {
            ID = id;
            Name = name;
            Type = type;
            Tileset = tileset;
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        #endregion

        #region Serialization Region

        /// <summary>
        /// Clones the <see cref="TerrainTile"/> object.
        /// </summary>
        /// <returns>Copy of the object.</returns>
        public TerrainTile Clone()
        {
            return (TerrainTile)MemberwiseClone();
        }

        /// <summary>
        /// Implements method from <see cref="ICloneable"/> interface.
        /// </summary>
        /// <returns></returns>
        object ICloneable.Clone()
        {
            return Clone();
        }

        /// <summary>
        /// Serializes the <see cref="TerrainTile"/> object to an XML file.
        /// </summary>
        public void SaveToXml(string path)
        {
            Serializer.SerializeToXml(this, path);
        }

        /// <summary>
        /// Deserializes a <see cref="TerrainTile"/> object from an XML file.
        /// </summary>
        /// <param name="path">Location of the XML file.</param>
        /// <returns>Deserialized <see cref="TerrainTile"/> object.</returns>
        public static TerrainTile LoadFromXml(string path)
        {
            return Serializer.DeserializeFromXml<TerrainTile>(path);
        }

        #endregion
    }
}
