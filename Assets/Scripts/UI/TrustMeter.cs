using UnityEngine;
using UnityEngine.UI;

public class TrustMeter : MonoBehaviour
{
    [Header("Trust Settings")]
    [Tooltip("Maximum trust value.")]
    public float maxTrust = 100f;
    [Tooltip("Current trust value.")]
    public float currentTrust = 100f;
    [Tooltip("Amount to decrease per roamer destroyed.")]
    public float decreaseAmount = 10f;

    [Header("UI Reference")]
    [Tooltip("UI Slider representing the trust meter.")]
    public Slider trustSlider;

    private void Start()
    {
        // Initialize the slider.
        if (trustSlider != null)
        {
            trustSlider.maxValue = maxTrust;
            trustSlider.value = currentTrust;
        }
    }

    /// <summary>
    /// Call this method when a roamer is destroyed (falls off correct tile).
    /// </summary>
    public void DecreaseTrust()
    {
        currentTrust = Mathf.Clamp(currentTrust - decreaseAmount, 0f, maxTrust);
        if (trustSlider != null)
        {
            trustSlider.value = currentTrust;
        }
        // Optional: Add additional visual or audio feedback here.
    }

    /// <summary>
    /// Optionally, call this method to increase trust when conditions are met.
    /// </summary>
    public void IncreaseTrust(float amount)
    {
        currentTrust = Mathf.Clamp(currentTrust + amount, 0f, maxTrust);
        if (trustSlider != null)
        {
            trustSlider.value = currentTrust;
        }
    }
}
