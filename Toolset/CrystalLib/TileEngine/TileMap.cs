using System;
using System.Collections.Generic;
using CrystalLib.Serialization;

namespace CrystalLib.TileEngine
{
    public class TileMap : ICloneable
    {
        #region Property Region

        public int ID { get; set; }
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int TileWidth { get; set; }
        public int TileHeight { get; set; }
        public List<TileLayer> Layers { get; set; }

        #endregion

        #region Constructor Region

        /// <summary>
        /// Initializes a new instance of the <see cref="TileMap"/> class.
        /// </summary>
        public TileMap()
        {
            Layers = new List<TileLayer>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TileMap"/> class.
        /// </summary>
        /// <param name="id">ID of the map.</param>
        /// <param name="name">Name of the map.</param>
        /// /// <param name="width">Width of the map.</param>
        /// <param name="height">Height of the map.</param>
        /// <param name="tilewidth">Width of the individual tiles.</param>
        /// <param name="tileheight">Height of the individual tiles.</param>
        public TileMap(int id, string name,int width, int height, int tilewidth, int tileheight)
        {
            Layers = new List<TileLayer>();

            ID = id;
            Name = name;
            Width = width;
            Height = height;
            TileWidth = tilewidth;
            TileHeight = tileheight;
        }

        #endregion

        #region Serialization Region

        /// <summary>
        /// Clones the <see cref="TileMap"/> object.
        /// </summary>
        /// <returns>Copy of the object.</returns>
        public TileMap Clone()
        {
            return (TileMap)MemberwiseClone();
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
        /// Serializes the <see cref="TileMap"/> object to an XML file.
        /// </summary>
        public void SaveToXml(string path)
        {
            Serializer.SerializeToXml(this, path);
        }

        /// <summary>
        /// Deserializes a <see cref="TileMap"/> object from an XML file.
        /// </summary>
        /// <param name="path">Location of the XML file.</param>
        /// <returns>Deserialized <see cref="TileMap"/> object.</returns>
        public static TileMap LoadFromXml(string path)
        {
            return Serializer.DeserializeFromXml<TileMap>(path);
        }

        #endregion
    }
}
