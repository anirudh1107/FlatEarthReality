using UnityEngine;

[System.Serializable]
public class SpawnConfig
{
    [Tooltip("Spawn point transform for this configuration.")]
    public Transform spawnPoint;
    [Tooltip("Target point transform for the spawned roamer.")]
    public Transform targetPoint;
    [Tooltip("Time delay (in seconds) before the first spawn occurs.")]
    public float startTime = 0f;
    [Tooltip("Time interval (in seconds) between consecutive spawns.")]
    public float spawnInterval = 10f;
}
