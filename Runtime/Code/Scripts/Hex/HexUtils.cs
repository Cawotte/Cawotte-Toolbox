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

        #region Directions
        public static bool IsAlignedWithDirection( Vector3 cubeDirection, Vector3Int direction )
        {
            float angleBetweenVec = Vector3.Angle( cubeDirection, direction );

            return angleBetweenVec < Mathf.Epsilon;
        }

        /// <summary>
        /// Take two Cube Coordinate of different cells, and if they are aligned in a hex grid, 
        /// return the hex direction between them.
        /// If there isn't an alignment, return Vector3.zero
        /// </summary>
        /// <param name="cubeA"></param>
        /// <param name="cubeB"></param>
        /// <returns></returns>
        public static Vector3Int GetDirection( Vector3Int cubeA, Vector3Int cubeB )
        {
            Vector3Int vec = cubeB - cubeA;

            Vector3Int[] allDirections = Directions.AllDirections;
            for ( int i = 0; i < Directions.MaxDirections; i++ )
            {
                if ( IsAlignedWithDirection( vec, allDirections[i]  ) )
                    return allDirections[i];
            }

            return Vector3Int.zero;
        }

        public static bool AreAlignedInHexCubeDirection( Vector3Int cubeA, Vector3Int cubeB )
        {
            return GetDirection( cubeA, cubeB ) != Vector3Int.zero;
        }

        #endregion
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
        public static Vector3 HexCubeToWorld( Vector3Int coordinate )
        {
            return HexCubeToWorld( coordinate, Vector3.zero, 1 );
        }

        public static Vector3 HexCubeToWorld( Vector3Int coordinate, Vector3 worldOrigin, float hexWidth )
        {
            // Converting Cube coordinate (q,r,s) to Axial (r, s)
            float x = hexWidth * ( ( Mathf.Sqrt( 3 ) * coordinate.x ) +
                                      ( ( Mathf.Sqrt( 3 ) / 2 ) * coordinate.y ) );
            float y = hexWidth * (3f/2f) * coordinate.y;

            return worldOrigin + new Vector3( x, y, 0 );
        }

        public static Vector3Int WorldToHexCube( Vector3 worldPos, Vector3 worldOrigin, float hexWidth )
        {
            worldPos = worldPos - worldOrigin;

            float q = ( ( Mathf.Sqrt( 3 ) / 3 ) * worldPos.x - ( 1f/3f ) * worldPos.y ) / hexWidth;
            float r = ( 2f/3f * worldPos.y ) / hexWidth;

            return CubeRound( AxialToCube( new Vector3( q, r, 0 ) ) );
        }

        public static Vector3Int WorldToHexAxial( Vector3 worldPos, Vector3 worldOrigin, float hexWidth )
        {
            worldPos = worldPos - worldOrigin;

            float q = ( ( Mathf.Sqrt( 3 ) / 3 ) * worldPos.x - ( 1f/3f ) * worldPos.y ) / hexWidth;
            float r = ( 2f/3f * worldPos.y ) / hexWidth;

            return AxialRound( new Vector3( q, r, 0 ) );
        }

        public static Vector3Int CubeToAxial( Vector3Int cubeCoordinate )
        {
            return new Vector3Int( cubeCoordinate.x, cubeCoordinate.y, 0 );
        }

        public static Vector3Int AxialToCube( Vector3Int axial )
        {
            return new Vector3Int( axial.x, axial.y, - axial.x - axial.y );
        }

        public static Vector3 AxialToCube( Vector3 axial )
        {
            return new Vector3( axial.x, axial.y, -axial.x - axial.y );
        }

        // Convert World Cube Pos to nearest Coordinate
        public static Vector3Int CubeRound( Vector3 pos )
        {
            int q = Mathf.RoundToInt( pos.x );
            int r = Mathf.RoundToInt( pos.y );
            int s = Mathf.RoundToInt( pos.z );

            float qDiff = Mathf.Abs( q - pos.x );
            float rDiff = Mathf.Abs( r - pos.y );
            float sDiff = Mathf.Abs( s - pos.z );

            // Because a Cube Coordinate must respect q + r + s = 0, do some check to verify and correct the equation
            if ( qDiff > rDiff && qDiff > sDiff )
                q = -r - s;
            else if ( rDiff > sDiff )
                r = -q - s;
            else
                s = -q - r;

            return new Vector3Int( q, r, s );
        }

        public static Vector3Int AxialRound( Vector3 pos )
        {
            return CubeToAxial( CubeRound( AxialToCube( pos ) ) );
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