using SFML.Graphics;

namespace CrystalLib.ResourceEngine
{
    public class TextureResource
    {
        #region Field Region

        private string _path;
        private bool _loaded;

        #endregion

        #region Property Region

        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        public bool Loaded
        {
            get { return _loaded; }
            set { _loaded = value; }
        }

        public Texture Texture { get; private set; }

        #endregion

        #region Constructor Region

        public TextureResource()
        {

        }

        public TextureResource(string path)
        {
            _path = path;
        }

        #endregion

        #region Method Region

        public void LoadTexture()
        {
            Texture = new Texture(_path);
            _loaded = true;
        }

        public void UnloadTexture()
        {
            Texture = null;
            _loaded = false;
        }

        #endregion
    }
}