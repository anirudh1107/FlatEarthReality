using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class BorderTileGlow : MonoBehaviour
{
    [Header("Glow Settings")]
    [Tooltip("Color of the glow effect.")]
    public Color glowColor = Color.yellow;
    [Tooltip("Speed at which the glow pulses.")]
    public float glowSpeed = 2f;
    [Tooltip("Minimum emission intensity.")]
    public float minEmission = 0.5f;
    [Tooltip("Maximum emission intensity.")]
    public float maxEmission = 2f;

    private Material materialInstance;
    private float emissionIntensity;
    private bool increasing = true;

    private void Start()
    {
        // Create a unique instance of the material so the effect doesn't affect all objects sharing it.
        Renderer rend = GetComponent<Renderer>();
        materialInstance = rend.material;
        materialInstance.EnableKeyword("_EMISSION");

        emissionIntensity = minEmission;
        SetEmission(emissionIntensity);
    }

    private void Update()
    {
        // Pulse the emission intensity over time.
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
        SetEmission(emissionIntensity);
    }

    private void SetEmission(float intensity)
    {
        Color finalColor = glowColor * intensity;
        materialInstance.SetColor("_EmissionColor", finalColor);
    }
}
