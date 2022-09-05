using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Static Class directions 
// Get RandomDirection

// Cube coordinate

namespace Cawotte.Toolbox
{
    /// <summary>
    /// Data structure to represent an Hexagonal grid.
    /// Use cube coordinates, with the coordinate as a dictionary key.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HexGrid<T>
    {
        [SerializeField]
        private Dictionary<Vector3Int, T> grid;

        public int Count { get => grid.Count; }

        public HexGrid()
        {
            this.grid = new Dictionary<Vector3Int, T>();
        }

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

        public T GetCell( Vector3Int coordinate )
        {
            return grid[coordinate];
        }

        public bool RemoveCell( Vector3Int coordinate )
        {
            return grid.Remove( coordinate );
        }

        public bool Clear()
        {
            bool wasNotEmpty = grid.Count > 0;
            grid.Clear();
            return wasNotEmpty;
        }

        public bool HasCell( Vector3Int coordinate )
        {
            return grid.ContainsKey( coordinate );
        }
        
        public ICollection<Vector3Int> GetAllCoordinates()
        {
            //Maybe return IEnumerable insteand?
            return grid.Keys;
        }

        #endregion
        // Get Distance

        #region Neighbor

        public T GetNeighborCell( Vector3Int tile, Vector3Int direction )
        {
            Vector3Int neighbordCoord = HexUtils.GetNeighborCoordinate( tile, direction );
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

        public ICollection<Vector3Int> GetNeighborsCoordinatesInDirection( Vector3Int tile, Vector3Int direction )
        {
            return GetNeighborsCoordinatesInDirection( tile, direction, HasCell );
        }

        public ICollection<Vector3Int> GetNeighborsCoordinatesInDirection( Vector3Int tile, Vector3Int direction, Func<Vector3Int, bool> stopCondition )
        {
            List<Vector3Int> neighbors = new List<Vector3Int>();

            int failSafeCount = 0;
            Vector3Int currentCellInLine = tile;
            bool shouldContinue = true;

            do
            {
                currentCellInLine = HexUtils.GetNeighborCoordinate( currentCellInLine, direction );
                shouldContinue = !stopCondition( currentCellInLine );

                if ( shouldContinue )
                    neighbors.Add( currentCellInLine );

                failSafeCount++;
                if ( failSafeCount > 20 )
                {
                    Debug.Log( "OVERFLOW!" );
                    return neighbors;
                }

            } while ( shouldContinue );

            return neighbors;
        }

        public ICollection<T> GetNeighborsInDirection( Vector3Int tile, Vector3Int direction )
        {
            return GetNeighborsInDirection( tile, direction, HasCell );
        }

        public ICollection<T> GetNeighborsInDirection( Vector3Int tile, Vector3Int direction, Func<Vector3Int, bool> stopCondition )
        {
            List<T> neighbors = new List<T>();

            int failSafeCount = 0;
            Vector3Int currentCellInLine = tile;
            bool shouldContinue = true;

            do
            {
                currentCellInLine = HexUtils.GetNeighborCoordinate( currentCellInLine, direction );
                shouldContinue = !stopCondition( currentCellInLine );

                if ( shouldContinue )
                    neighbors.Add( GetCell( currentCellInLine ) );

                failSafeCount++;
                if ( failSafeCount > 20 )
                {
                    Debug.Log( "OVERFLOW!" );
                    return neighbors;
                }

            } while ( shouldContinue );

            return neighbors;
        }

    }
}