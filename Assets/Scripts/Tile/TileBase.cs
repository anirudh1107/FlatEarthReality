using UnityEngine;

public enum TileType
{
    Road,
    Ground,
    Water
}

public abstract class TileBase : MonoBehaviour
{
    [Header("Tile Settings")]
    [Tooltip("Type of tile (default: Road, Ground, Water). Extend this enum for new types.")]
    public TileType tileType;

    [Tooltip("If true, the tile cannot be moved by the player.")]
    public bool isStatic = false;

    [Tooltip("Grid cell size for snapping (assumed square).")]
    public float gridSize = 1f;

    /// <summary>
    /// Snaps the tile to the center of the nearest grid cell.
    /// </summary>
    public virtual void SnapToGrid()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Round(pos.x / gridSize) * gridSize;
        pos.y = Mathf.Round(pos.y / gridSize) * gridSize;
        transform.position = pos;
    }

    /// <summary>
    /// Called after the tile has been placed on the grid.
    /// Extend this method in derived classes to add custom behavior.
    /// </summary>
    public virtual void OnPlace()
    {
        // Base implementation is empty.
    }
}
