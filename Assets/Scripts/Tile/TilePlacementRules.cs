using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// A utility class for checking tile placement rules and applying border effects based on neighboring tiles.
/// </summary>
public static class TilePlacementRules
{
    /// <summary>
    /// Checks if a tile can be placed at the specified grid position.
    /// For example, placement is allowed if the cell is empty.
    /// You can extend this to include more complex rules.
    /// </summary>
    /// <param name="tileType">Type of the tile to place.</param>
    /// <param name="gridPosition">Grid coordinate where the tile is to be placed.</param>
    /// <param name="grid">A dictionary mapping grid positions to the existing TileBase instances.</param>
    /// <returns>True if placement is allowed; otherwise false.</returns>
    public static bool CanPlaceTile(TileType tileType, Vector2Int gridPosition, Dictionary<Vector2Int, TileBase> grid)
    {
        // Disallow placement if a tile already exists at that grid position.
        if (grid.ContainsKey(gridPosition))
        {
            return false;
        }
        // Additional placement rules (based on neighboring tiles) can be added here.
        return true;
    }

    /// <summary>
    /// Checks neighboring cells of a placed tile and applies border rules.
    /// For any adjacent cell containing a tile of a different type (and if a border is required),
    /// a border tile prefab is instantiated at the common edge with a growing effect.
    /// </summary>
    /// <param name="placedTile">The tile that was just placed.</param>
    /// <param name="grid">A dictionary mapping grid positions to TileBase instances.</param>
    /// <param name="borderTilePrefab">Prefab for the border tile that contains the BorderTile component.</param>
    public static void ApplyBorderRules(TileBase placedTile, Dictionary<Vector2Int, TileBase> grid, GameObject borderTilePrefab)
    {
        if (placedTile == null || borderTilePrefab == null)
            return;

        // Compute the grid position of the placed tile.
        float gridSize = placedTile.gridSize;
        Vector2Int tilePos = new Vector2Int(
            Mathf.RoundToInt(placedTile.transform.position.x / gridSize),
            Mathf.RoundToInt(placedTile.transform.position.y / gridSize)
        );

        // Define the four primary directions.
        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(0, 1),   // Up
            new Vector2Int(0, -1),  // Down
            new Vector2Int(-1, 0),  // Left
            new Vector2Int(1, 0)    // Right
        };

        foreach (var dir in directions)
        {
            Vector2Int neighborPos = tilePos + dir;
            // Check if there is a neighboring tile at this grid position.
            if (grid.TryGetValue(neighborPos, out TileBase neighborTile))
            {
                // If the neighbor exists and its tile type is different,
                // then a border should be placed at the common edge.
                if (neighborTile.tileType != placedTile.tileType)
                {
                    // Determine the border's world position:
                    // place it halfway between the centers of the two tiles.
                    Vector3 borderPos = placedTile.transform.position + new Vector3(dir.x, dir.y, 0) * (gridSize / 2f);

                    // Instantiate the border tile.
                    GameObject borderObj = GameObject.Instantiate(borderTilePrefab, borderPos, Quaternion.identity);

                    // Rotate the border tile if needed (e.g. for left/right vs up/down orientation).
                    if (dir.x != 0)
                    {
                        borderObj.transform.rotation = Quaternion.Euler(0, 0, 90);
                    }

                    // Optionally, you can parent the border tile to a dedicated container in your scene.
                    // For example: borderObj.transform.parent = GameObject.Find("BordersContainer").transform;
                }
            }
            else
            {
                // Optionally, you could also handle cases where no neighbor exists,
                // if your design calls for an external border even along the edge of the grid.
            }
        }
    }
}
