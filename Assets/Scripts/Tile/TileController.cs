using UnityEngine;

public class TileController : TileBase
{
    private bool isDragging = false;
    private Vector3 offset;

    // Called when the left mouse button is pressed over this tile.
    private void OnMouseDown()
    {
        if (isStatic) return; // Ignore interaction if the tile is static.

        // Begin dragging if left mouse button is clicked.
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;

            // Calculate the offset between the tile's position and the mouse's world position.
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Preserve the z position (for 2D top-down, z can be constant)
            offset = transform.position - new Vector3(mouseWorldPos.x, mouseWorldPos.y, transform.position.z);
        }
    }

    // Called every frame while the mouse is held down over this tile.
    private void OnMouseDrag()
    {
        if (!isDragging) return;

        // Update the tile position to follow the mouse, applying the initial offset.
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 newPos = new Vector3(mouseWorldPos.x, mouseWorldPos.y, transform.position.z) + offset;
        transform.position = newPos;

        // Optional: you can add visual feedback here (e.g., a slight scale change)
    }

    private void Update()
    {
        // Allow tile rotation when dragging by pressing the R key.
        if (isDragging && Input.GetKeyDown(KeyCode.R))
        {
            // Rotate by 90 degrees.
            transform.Rotate(0, 0, 90);
        }
    }

    // Called when the mouse button is released.
    private void OnMouseUp()
    {
        if (!isDragging) return;

        isDragging = false;
        // Snap the tile's position to the grid.
        SnapToGrid();

        // Call additional behavior after placing the tile.
        OnPlace();
    }
}
