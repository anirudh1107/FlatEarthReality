using Unity.VisualScripting;
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
        Vector3 secondRoadOffset = new Vector3(0, 10f, 0);
       
        for (int i = 0; i < 30; i++)
        {
            for (int j = 0; j < 30; j++)
            {
                GameObject.Instantiate(waterPrefab, new Vector3(waterGeneratedPoint.position.x + (i * groundMultiplayer), waterGeneratedPoint.position.y + (j * groundMultiplayer), waterGeneratedPoint.position.z), Quaternion.identity);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
