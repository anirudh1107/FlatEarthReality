using UnityEngine;
using UnityEngine.UI;

public abstract class RoamerBase : MonoBehaviour
{
    [Header("Roamer Settings")]
    [Tooltip("Movement speed of the roamer.")]
    public float speed = 5f;
    [Tooltip("Array of waypoints (Transforms) defining the roamer's path.")]
    public Transform[] waypoints;

    [HideInInspector]
    public int currentWaypointIndex = 0;
    [HideInInspector]
    public int lastCheckpointIndex = 0;

    // Each roamer must define what tile type it is allowed to be on.
    public abstract TileType AllowedTileType { get; }

    // Used by IsOnValidTile() to check for a valid tile.
    [Tooltip("Layer mask to identify tile colliders.")]
    public LayerMask tileLayerMask;
    [Tooltip("Radius for checking if the roamer is on a valid tile.")]
    public float tileCheckRadius = 0.1f;
    public Slider trustSlider;

    protected IRoamerState currentState;

    private TrustMeter trustMeter;

    protected virtual void Start()
    {
        // Start in the normal movement state.
        SwitchState(new RoamerNormalState());
        if (trustSlider != null)
        {
            trustMeter = trustSlider.GetComponent<TrustMeter>();
        }
    }

    protected virtual void Update()
    {
        // Check if the roamer is on a valid tile.
        if (!IsOnValidTile())
        {
            // If not, the roamer has fallen off—destroy it.
            trustMeter.DecreaseTrust();
            Destroy(gameObject);
            return;
        }
        if (Vector2.Distance(this.transform.position, waypoints[1].position) < 1f)
        {
            Destroy(gameObject);
        }

        // Delegate behavior to the current state.
        currentState?.UpdateState(this);
    }

    /// <summary>
    /// Switches the roamer's behavior state.
    /// </summary>
    public void SwitchState(IRoamerState newState)
    {
        currentState?.ExitState(this);
        currentState = newState;
        currentState?.EnterState(this);
    }

    /// <summary>
    /// Checks if there is a tile under the roamer that matches its allowed tile type.
    /// </summary>
    protected bool IsOnValidTile()
    {
        // Use OverlapPoint to find a tile at the roamer's position.
        int combinedMask = LayerMask.GetMask("Ground") | LayerMask.GetMask("Road") | LayerMask.GetMask("WaterWay");
        Collider2D col = Physics2D.OverlapPoint(transform.position, combinedMask);
        if (col == null)
            return false;
        else
        {
            TileBase tile = col.GetComponent<TileBase>();
            if (tile != null && tile.tileType == AllowedTileType)
            {
                return true;
            }
            else
            {
                Debug.Log("Roamer is on an invalid tile.");
                SwitchState(new RoamerReverseState());
                return true;
            }
        }
    }
}
