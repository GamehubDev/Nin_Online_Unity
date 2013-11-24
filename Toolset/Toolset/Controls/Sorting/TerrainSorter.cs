using System.Collections.Generic;
using CrystalLib.TileEngine;

namespace Toolset.Controls.Sorting
{
    class TerrainSorter : IComparer<TerrainTile>
    {
        readonly NaturalComparer comparer = new NaturalComparer();

        public int Compare(TerrainTile x, TerrainTile y)
        {
            return comparer.Compare(x.Name, y.Name);
        }
    }
}