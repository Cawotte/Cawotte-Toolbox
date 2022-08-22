using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Static Class directions 
// Get RandomDirection

// Cube coordinate

public class HexGrid<T>
{
    [SerializeField]
    private Dictionary<Vector3Int, T> grid;

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

    // Remove Cell
    // Has Cell
    // Get Distance
    // Get Neighbor Coordinate
    // Get Neighbor Cell(s)
    // Get Neighbor(s) in Line

}
