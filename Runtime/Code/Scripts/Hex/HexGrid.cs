using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Static Class directions 
// Get RandomDirection

// Cube coordinate

namespace Cawotte.Toolbox
{
    public class HexGrid<T>
    {
        [SerializeField]
        private Dictionary<Vector3Int, T> grid;

        #region Add/Remove
        public bool AddCell( Vector3Int coordinate, T cell )
        {
            try
            {
                grid.Add( coordinate, cell );
                return true;
            }
            catch ( System.ArgumentException )
            {
                return false;
            }
        }

        public bool RemoveCell( Vector3Int coordinate )
        {
            return grid.Remove( coordinate );
        }

        public bool HasCell( Vector3Int coordinate )
        {
            return grid.ContainsKey( coordinate );
        }

        #endregion
        // Get Distance

        #region Neighbor



        public T GetNeighborCell( Vector3Int tile, Vector3Int direction )
        {
            Vector3Int neighbordCoord = HexUtils.GetNeighbor( tile, direction );
            if ( HasCell( neighbordCoord ) )
            {
                return grid[neighbordCoord];
            }
            else
            {
                // Todo : Code error
                return default( T );
            }
        }

        #endregion
        // Get Neighbor Cell(s)
        // Get Neighbor(s) in Line

    }
}