using UnityEngine;

public class TileController : TileBase
{
    private bool isDragging = false;
    private Vector3 offset;

    [Header("Outline Settings")]
    [Tooltip("Color of the outline.")]
    public Color outlineColor = Color.red;
    [Tooltip("Percentage increase for the outline's scale compared to the original sprite.")]
    [Range(0.05f, 0.5f)]
    public float outlineThickness = 0.1f;

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
        if (isStatic)
        {
            if (tileSpriteRenderer != null && tileSpriteRenderer.sprite != null)
            {
                // Create a new GameObject to serve as the outline.
                GameObject outlineObj = new GameObject("Outline");
                outlineObj.transform.SetParent(transform);
                outlineObj.transform.localPosition = Vector3.zero;
                outlineObj.transform.localRotation = Quaternion.identity;

                // Scale up the outline based on the thickness value.
                outlineObj.transform.localScale = Vector3.one * (1f + outlineThickness);

                // Add a SpriteRenderer to the outline object.
                SpriteRenderer outlineSR = outlineObj.AddComponent<SpriteRenderer>();
                outlineSR.sprite = tileSpriteRenderer.sprite;
                outlineSR.color = outlineColor;

                // Ensure the outline is rendered behind the original sprite.
                outlineSR.sortingLayerID = tileSpriteRenderer.sortingLayerID;
                outlineSR.sortingOrder = tileSpriteRenderer.sortingOrder - 1;

                // Optionally, you can share the same material if it supports emission/outline properties.
                outlineSR.material = tileSpriteRenderer.material;
            }
        }
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
