using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RoamerPathDisplay : MonoBehaviour
{
    [Header("Path Display Settings")]
    [Tooltip("List of waypoints that define the roamer's path.")]
    public Transform[] waypoints;
    [Tooltip("Color of the path glow effect.")]
    public Color glowColor = Color.cyan;
    [Tooltip("Speed at which the path glow pulses.")]
    public float glowSpeed = 2f;
    [Tooltip("Minimum emission intensity for the path.")]
    public float minEmission = 0.5f;
    [Tooltip("Maximum emission intensity for the path.")]
    public float maxEmission = 2f;

    private LineRenderer lineRenderer;
    private Material lineMaterial;
    private float emissionIntensity;
    private bool increasing = true;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        // The path starts with current position plus all waypoints.
        lineRenderer.positionCount = 0;
        // Create a new material instance for the line renderer.
        lineMaterial = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.material = lineMaterial;
        lineMaterial.SetColor("_Color", glowColor);
        lineMaterial.EnableKeyword("_EMISSION");
        emissionIntensity = minEmission;
    }

    private void Update()
    {
        // Update the line renderer positions:
        // Starting at the roamer's current position, then through each waypoint.
        if (waypoints != null && waypoints.Length > 0)
        {
            lineRenderer.positionCount = waypoints.Length + 1;
            lineRenderer.SetPosition(0, transform.position);
            for (int i = 0; i < waypoints.Length; i++)
            {
                lineRenderer.SetPosition(i + 1, waypoints[i].position);
            }
        }
        else
        {
            lineRenderer.positionCount = 0;
        }

        // Animate the glow effect on the line renderer.
        if (increasing)
        {
            emissionIntensity += glowSpeed * Time.deltaTime;
            if (emissionIntensity >= maxEmission)
            {
                emissionIntensity = maxEmission;
                increasing = false;
            }
        }
        else
        {
            emissionIntensity -= glowSpeed * Time.deltaTime;
            if (emissionIntensity <= minEmission)
            {
                emissionIntensity = minEmission;
                increasing = true;
            }
        }
        Color finalColor = glowColor * emissionIntensity;
        lineMaterial.SetColor("_EmissionColor", finalColor);
    }
}
