using System.Collections.Generic;
using System.IO;
using System.Linq;
using SFML.Graphics;

namespace CrystalLib.ResourceEngine
{
    public class ResourceManager
    {
        #region Field Region

        /// <summary>
        /// Array of loaded <see cref="TextureResource"/> objects.
        /// </summary>
        private readonly List<TextureResource> _textures = new List<TextureResource>();

        #endregion

        #region Property Region

        /// <summary>
        /// Returns the singleton instance of the <see cref="ResourceManager"/> class.
        /// </summary>
        public static readonly ResourceManager Instance = new ResourceManager();

        #endregion

        #region Resource Access Region

        /// <summary>
        /// Returns a reference to the texture if it already exists in memory.
        /// If not it will load the texture and make it available for future calls.
        /// </summary>
        /// <param name="path">Path of the image file.</param>
        /// <returns>Returns the SFML Texture.</returns>
        public Texture LoadTexture(string path)
        {
            if (!File.Exists(path)) return null;

            foreach (var textureResource in _textures.Where(textureResource => textureResource.Path == path))
            {
                if (!textureResource.Loaded) textureResource.LoadTexture();
                return textureResource.Texture;
            }

            return NewTexture(path);
        }

        /// <summary>
        /// Adds a new texture to the <see cref="ResourceManager"/>.
        /// </summary>
        /// <param name="path">Path of the image file.</param>
        /// <returns>Returns the SFML Texture.</returns>
        public Texture NewTexture(string path)
        {
            var texture = new TextureResource(path);
            texture.LoadTexture();
            _textures.Add(texture);
            return texture.Texture;
        }

        #endregion
    }
}