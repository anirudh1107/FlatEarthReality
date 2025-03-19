using UnityEngine;
using UnityEngine.UIElements;

public class LevelGenerator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private GameObject groundPrefab;
    [SerializeField]
    private GameObject roadPrefab;
    [SerializeField]
    private GameObject waterPrefab;

    [SerializeField]
    private Transform groundGeneratedPoint;
    [SerializeField]
    private Transform roadGeneratedPoint;
    [SerializeField]
    private Transform waterGeneratedPoint;

    void Start()
    {
        float groundMultiplayer = groundPrefab.transform.localScale.x;
        float roadMultiplayer = roadPrefab.transform.localScale.x;
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                GameObject.Instantiate(groundPrefab, new Vector3(groundGeneratedPoint.position.x + (i* groundMultiplayer), groundGeneratedPoint.position.y + (j*groundMultiplayer), groundGeneratedPoint.position.z), Quaternion.identity);
            }
        }

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                GameObject.Instantiate(roadPrefab, new Vector3(roadGeneratedPoint.position.x + (i * groundMultiplayer), roadGeneratedPoint.position.y + (j * groundMultiplayer), roadGeneratedPoint.position.z), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
