using System;
using CrystalLib.Serialization;

namespace CrystalLib.TileEngine
{
    public class Tileset : ICloneable
    {
        #region Property Region

        public int ID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int TileWidth { get; set; }
        public int TileHeight { get; set; }

        #endregion

        #region Constructor Region

        /// <summary>
        /// Initializes a new instance of the <see cref="Tileset"/> class.
        /// </summary>
        public Tileset()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tileset"/> class.
        /// </summary>
        /// <param name="id">ID of the tileset.</param>
        /// <param name="name">Name of the tileset.</param>
        /// <param name="image">Image of the tileset.</param>
        /// <param name="tilewidth">Width of the individual tiles.</param>
        /// <param name="tileheight">Height of the individual tiles.</param>
        public Tileset(int id, string name, string image, int tilewidth, int tileheight)
        {
            ID = id;
            Name = name;
            Image = image;
            TileWidth = tilewidth;
            TileHeight = tileheight;
        }

        #endregion

        #region Serialization Region

        /// <summary>
        /// Clones the <see cref="Tileset"/> object.
        /// </summary>
        /// <returns>Copy of the object.</returns>
        public Tileset Clone()
        {
            return (Tileset)MemberwiseClone();
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
        /// Serializes the <see cref="Tileset"/> object to an XML file.
        /// </summary>
        public void SaveToXml(string path)
        {
            Serializer.SerializeToXml(this, path);
        }

        /// <summary>
        /// Deserializes a <see cref="Tileset"/> object from an XML file.
        /// </summary>
        /// <param name="path">Location of the XML file.</param>
        /// <returns>Deserialized <see cref="Tileset"/> object.</returns>
        public static Tileset LoadFromXml(string path)
        {
            return Serializer.DeserializeFromXml<Tileset>(path);
        }

        #endregion
    }
}
