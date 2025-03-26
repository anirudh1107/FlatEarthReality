using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RoamerSpawnerBase : MonoBehaviour
{
    [Header("Spawn Configurations")]
    [Tooltip("List of spawn configurations for this spawner.")]
    public List<SpawnConfig> spawnConfigs = new List<SpawnConfig>();

    [Header("Prefab References")]
    [Tooltip("Prefab for horizontal orientation roamer.")]
    public GameObject horizontalPrefab;
    [Tooltip("Prefab for vertical orientation roamer.")]
    public GameObject verticalPrefab;

    protected virtual void Start()
    {
        // Start a coroutine for each spawn configuration.
        foreach (var config in spawnConfigs)
        {
            StartCoroutine(HandleSpawn(config));
        }
    }

    protected virtual IEnumerator HandleSpawn(SpawnConfig config)
    {
        // Wait until the start time.
        yield return new WaitForSeconds(config.startTime);

        while (true)
        {
            SpawnRoamer(config);
            yield return new WaitForSeconds(config.spawnInterval);
        }
    }

    protected virtual void SpawnRoamer(SpawnConfig config)
    {
        // Determine which prefab to use based on spawn and target point positions.
        GameObject prefabToSpawn = null;

        // If spawn and target share the same X coordinate (within tolerance), use vertical prefab.
        if (Mathf.Approximately(config.spawnPoint.position.x, config.targetPoint.position.x))
        {
            prefabToSpawn = verticalPrefab;
        }
        // If spawn and target share the same Y coordinate (within tolerance), use horizontal prefab.
        else if (Mathf.Approximately(config.spawnPoint.position.y, config.targetPoint.position.y))
        {
            prefabToSpawn = horizontalPrefab;
        }

        if (prefabToSpawn != null)
        {
            // Instantiate the selected prefab at the spawn point.
            GameObject roamer = Instantiate(prefabToSpawn, config.spawnPoint.position, Quaternion.identity);

            // Optionally, adjust rotation based on the direction from spawn to target.
            //Vector2 direction = config.targetPoint.position - config.spawnPoint.position;
            //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //roamer.transform.rotation = Quaternion.Euler(0, 0, angle);

            // If the roamer has a RoamerAI script, assign its target.
            RoamerBase roamerAI = roamer.GetComponent<RoamerBase>();
            if (roamerAI != null)
            {
                roamerAI.waypoints = new Transform[] {config.spawnPoint, config.targetPoint };
            }
        }
    }
}
