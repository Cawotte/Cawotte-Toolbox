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

            //Direction are misordered?
            public static Vector3Int TopRight       { get => new Vector3Int( 1, -1, 0 ); }
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

        public static float CubeDistance( Vector3Int a, Vector3Int b )
        {
            Vector3Int ab = a - b;
            return ( Mathf.Abs( ab.x ) + Mathf.Abs( ab.y ) + Mathf.Abs( ab.z ) ) / 2f;
        }

        public static bool AreCoordinatesAligned( Vector3Int a, Vector3Int b )
        {
            Vector3 ab = a - b;
            Vector3Int[] allDirections = Directions.AllDirections;

            for ( int i = 0; i < Directions.MaxDirections; i++ )
            {
                Vector3 normalizedDirection = Vector3.Normalize( allDirections[i] );
                float distance = Vector3.Distance( ab.normalized, allDirections[i] );
                Vector3 diff = ab.normalized - normalizedDirection;
                if ( ab.normalized == normalizedDirection )
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Get the closest direction vector from the direction represented by the given argument.
        /// Verify that given vector has origin 0,0,0.
        /// </summary>
        /// <param name="tile"></param>
        /// <returns></returns>
        public static Vector3Int GetClosestDirection( Vector2 worldPos )
        {
            Vector3Int[] allDirections = Directions.AllDirections;
            Vector3Int closestDirection = allDirections[0];

            float minDistance = Mathf.Infinity;
            for ( int i = 0; i < Directions.MaxDirections; i++ )
            {
                Vector3 worldDirection = HexToWorld( allDirections[i], Vector3.zero, 0.6f ); //( allDirections[i] );
                float distance = Vector3.Distance( worldPos.normalized, worldDirection );
                if ( distance < minDistance )
                {
                    minDistance = distance;
                    closestDirection = allDirections[i];
                }
            }

            return closestDirection;
        }

        #region Neighbor
        public static Vector3Int GetNeighborCoordinate( Vector3Int tile, Vector3Int direction )
        {
            return tile + direction;
        }
        public static List<Vector3Int> GetAllNeighborsCoordinate( Vector3Int tile )
        {
            List<Vector3Int> neighbors = new List<Vector3Int>();
            Vector3Int[] allDirections = Directions.AllDirections;

            for ( int i = 0; i < Directions.MaxDirections; i++ )
            {
                neighbors.Add( GetNeighborCoordinate( tile, allDirections[i] ) );
            }

            return neighbors;
        }

        #endregion

        #region Conversions
        public static Vector3 HexToWorld( Vector3Int coordinate )
        {
            return HexToWorld( coordinate, Vector3.zero, 1 );
        }

        public static Vector3 HexToWorld( Vector3Int coordinate, Vector3 worldOrigin, float oneUnitLenght )
        {
            // Converting Cube coordinate (q,r,s) to Axial (r, s)
            float x = oneUnitLenght * ( ( Mathf.Sqrt( 3 ) * coordinate.x ) +
                                      ( ( Mathf.Sqrt( 3 ) / 2 ) * coordinate.y ) );
            float y = oneUnitLenght * (3f/2f) * coordinate.y;

            return worldOrigin + new Vector3( x, y, 0 );
        }

        public static Vector3Int CubeToAxial( Vector3Int cubeCoordinate )
        {
            return new Vector3Int( cubeCoordinate.x, cubeCoordinate.y, 0 );
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
                    List<Vector3Int> directNeighbors = GetAllNeighborsCoordinate( tile );
                    foreach ( Vector3Int neighbor in directNeighbors )
                    {
                        if ( !grid.Contains( neighbor ) ) // Don't put tiles already placed
                        {
                            possibleNeighbors.Add( neighbor );
                        }
                    }
                } // Those loops scales very poorly (n^3), but will do the job at our scale.

                Vector3Int newTile = possibleNeighbors[ Random.Range( 0, possibleNeighbors.Count ) ];
                grid.Add( newTile );

            }

            return grid;
        }


    }
}