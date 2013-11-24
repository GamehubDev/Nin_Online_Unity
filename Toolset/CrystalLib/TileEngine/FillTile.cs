namespace CrystalLib.TileEngine
{
    public class FillTile
    {
        public int Tileset { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Terrain { get; set; }

        public FillTile()
        {

        }

        public FillTile(int tileset, int x, int y, int terrain)
        {
            Tileset = tileset; X = x; Y = y; Terrain = terrain;
        }
    }
}
