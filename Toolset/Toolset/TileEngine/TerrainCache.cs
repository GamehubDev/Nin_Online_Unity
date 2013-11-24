using CrystalLib.ResourceEngine;
using CrystalLib.TileEngine;
using SFML.Graphics;
using SFML.Window;
using Toolset.Managers;

namespace Toolset.TileEngine
{
    public class TerrainCache
    {
        #region Property Region

        private TerrainTile _terrain;

        private Sprite[] _sprites { get; set; }

        private TerrainHelper Helper { get; set; }

        private int destX;
        private int destY;

        #endregion

        #region Constructor Region

        public TerrainCache()
        {
            _sprites = new Sprite[4];
        }

        #endregion

        #region Cache Region

        public void Cache(int x, int y, int tile, int NW, int N, int NE, int E, int SE, int S, int SW, int W)
        {
            var terrain = TerrainManager.Instance.GetTerrain(tile);
            if (terrain == null)
            {
                _sprites = null;
                return;
            }

            _terrain = terrain;

            Helper = new TerrainHelper(_terrain);
            Helper.CachePoints(_terrain.Type);

            var tileset = TilesetManager.Instance.GetTileset(terrain.Tileset);
            if (tileset == null)
            {
                _sprites = null;
                return;
            }

            destX = x;
            destY = y;

            switch (terrain.Type)
            {
                case TerrainType.RMVX_Ground:
                    _sprites[0] = SetSprite(Helper.CalculateNW_RMVX_Ground(tile, NW, N, W), 0);
                    _sprites[1] = SetSprite(Helper.CalculateNE_RMVX_Ground(tile, N, NE, E), 1);
                    _sprites[2] = SetSprite(Helper.CalculateSW_RMVX_Ground(tile, W, SW, S), 2);
                    _sprites[3] = SetSprite(Helper.CalculateSE_RMVX_Ground(tile, S, SE, E), 3);
                    break;
                case TerrainType.RMVX_Cliff:
                    _sprites[0] = SetSprite(Helper.CalculateNW_RMVX_Cliff(tile, NW, N, W), 0);
                    _sprites[1] = SetSprite(Helper.CalculateNE_RMVX_Cliff(tile, N, NE, E), 1);
                    _sprites[2] = SetSprite(Helper.CalculateSW_RMVX_Cliff(tile, W, SW, S), 2);
                    _sprites[3] = SetSprite(Helper.CalculateSE_RMVX_Cliff(tile, S, SE, E), 3);
                    break;
            }
        }

        private Sprite SetSprite(Point point, int offsetIndex)
        {
            if (_terrain == null) return null;

            var tileset = TilesetManager.Instance.GetTileset(_terrain.Tileset);
            if (tileset == null) return null;

            if (point == null) return null;

            var x = destX * tileset.TileWidth;
            var y = destY * tileset.TileHeight;

            var srcX = (_terrain.X * tileset.TileWidth) + point.X;
            var srcY = (_terrain.Y * tileset.TileHeight) + point.Y;

            var xoffset = tileset.TileWidth / 2;
            var yoffset = tileset.TileHeight / 2;

            switch (offsetIndex)
            {
                // NW
                case 0:
                    break;
                // NE
                case 1:
                    x += xoffset;
                    break;
                // SW
                case 2:
                    y += yoffset;
                    break;
                // SE
                case 3:
                    x += xoffset;
                    y += yoffset;
                    break;
            }

            var sprite = new Sprite
            {
                Texture = ResourceManager.Instance.LoadTexture(tileset.Image),
                Position = new Vector2f(x, y),
                TextureRect = new IntRect(srcX, srcY, tileset.TileWidth / 2, tileset.TileHeight / 2)
            };

            return sprite;
        }

        #endregion

        #region SFML Region

        public void Render(RenderWindow rw)
        {
            if (_sprites == null) return;

            for (int i = 0; i < 4; i++)
            {
                if (_sprites[i] != null)
                    rw.Draw(_sprites[i]);
            }
        }

        #endregion
    }
}
