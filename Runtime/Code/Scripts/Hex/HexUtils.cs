using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cawotte.Toolbox
{
    public static class HexUtils
    {

        public static class Directions
        {
            //We are using static properties for Hex directions because enums can't hold a Vector3Int as a value.

            public static Vector3Int TopRight       { get => new Vector3Int( 1, 1, 0 ); }
            public static Vector3Int Right          { get => new Vector3Int( 1, 0, -1 ); }
            public static Vector3Int BottomRight    { get => new Vector3Int( 0, 1, -1 ); }
            public static Vector3Int BottomLeft     { get => new Vector3Int( -1, 1, 0 ); }
            public static Vector3Int Left           { get => new Vector3Int( -1, 0, 1 ); }
            public static Vector3Int TopLeft        { get => new Vector3Int( 0, -1, 1 ); }

            public static Vector3Int[] AllDirections { get => new Vector3Int[] { TopRight, Right, BottomRight, BottomLeft, Left, TopLeft }; }

            public static int MaxDirections = AllDirections.Length;

            public static Vector3Int GetRandomDirection()
		    {
                return AllDirections[ Random.Range( 0, MaxDirections )];
		    }

        }

        #region Neighbor
        public static Vector3Int GetNeighbor( Vector3Int tile, Vector3Int direction )
        {
            return tile + direction;
        }
        public static List<Vector3Int> GetAllPossibleNeighbors( Vector3Int tile )
        {
            List<Vector3Int> neighbors = new List<Vector3Int>();
            Vector3Int[] allDirections = Directions.AllDirections;

            for ( int i = 0; i < Directions.MaxDirections; i++ )
            {
                neighbors.Add( GetNeighbor( tile, allDirections[i] ) );
            }

            return neighbors;
        }

        #endregion

        /// <summary>
        /// Basic random generation algorithm for an Hex grid.
        /// Pick a random neighbors to be the new tile out of all possible neighbors of the current tile/grid.
        /// Weighted so tiles that are neighbor to several others have more chances to be picked.
        /// </summary>
        /// <param name="startCoordinate"></param>
        /// <returns></returns>
        public static List<Vector3Int> GenerateRandomGrid( int nbTiles )
        {
            List<Vector3Int> grid = new List<Vector3Int>();

            Vector3Int currentTile = Vector3Int.zero;
            grid.Add( currentTile );

            while ( grid.Count < nbTiles )
            {
                List<Vector3Int> possibleNeighbors = new List<Vector3Int>();

                // Get all possible neighbors of each tiles
                // Tiles that shares neighbor will be added several times, technically increasing their weight once picked
                foreach ( Vector3Int tile in grid )
                {
                    List<Vector3Int> directNeighbors = GetAllPossibleNeighbors( tile );
                    foreach ( Vector3Int neighbor in directNeighbors )
                    {
                        if ( !grid.Contains( neighbor ) ) // Don't put tiles already placed
                        {
                            possibleNeighbors.Add( neighbor );
                        }
                    }
                } // Those loops scales poorly (n^3), but will do the job at our scale.

                Vector3Int newTile = possibleNeighbors[ Random.Range( 0, possibleNeighbors.Count ) ];
                grid.Add( newTile );

            }

            return grid;
        }
    }

}