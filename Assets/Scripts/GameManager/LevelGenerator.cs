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
    private GameObject raodWaterTopPrefab;
    [SerializeField]
    private GameObject waterRoadTopPrefab;
    [SerializeField]
    private GameObject carRoamer;
    [SerializeField]
    private Transform wayPointCar1;
    [SerializeField]
    private Transform wayPointCar2;
    [SerializeField]
    private Transform wayPointCar3;
    [SerializeField]
    private Transform wayPointCar4;

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
        Vector3 secondRoadOffset = new Vector3(0, 20f, 0);
        for (int i = 0; i < 50; i++)
        {
            GameObject.Instantiate(waterRoadTopPrefab, new Vector3(roadGeneratedPoint.position.x + (i * groundMultiplayer), roadGeneratedPoint.position.y - groundMultiplayer, roadGeneratedPoint.position.z), Quaternion.identity);
            GameObject.Instantiate(roadPrefab, new Vector3(roadGeneratedPoint.position.x + (i * groundMultiplayer), roadGeneratedPoint.position.y, roadGeneratedPoint.position.z), Quaternion.identity);
            GameObject.Instantiate (raodWaterTopPrefab, new Vector3(roadGeneratedPoint.position.x + (i * groundMultiplayer), roadGeneratedPoint.position.y + groundMultiplayer, roadGeneratedPoint.position.z), Quaternion .identity);
            GenerateSecondRoad(groundMultiplayer, secondRoadOffset, i);
            for (int j = 0; j < 10; j++)
            {
                
            }
        }

        for (int i = 0; i < 30; i++)
        {
            for (int j = 0; j < 30; j++)
            {
                GameObject.Instantiate(groundPrefab, new Vector3(groundGeneratedPoint.position.x + (i * groundMultiplayer), groundGeneratedPoint.position.y + (j * groundMultiplayer), groundGeneratedPoint.position.z), Quaternion.identity);
            }
        }

        CarRoamer car1 =  GameObject.Instantiate(carRoamer, wayPointCar1.position, Quaternion.identity).GetComponent<CarRoamer>();
        car1.waypoints = new Transform[] { wayPointCar1, wayPointCar2 };
        CarRoamer car2 = GameObject.Instantiate(carRoamer, wayPointCar3.position, Quaternion.identity).GetComponent<CarRoamer>();
        car2.waypoints = new Transform[] {wayPointCar3, wayPointCar4 };

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateSecondRoad(float groundMultiplayer, Vector3 secondRoadOffset, int i)
    {
        GameObject.Instantiate(waterRoadTopPrefab, new Vector3(roadGeneratedPoint.position.x + (i * groundMultiplayer), roadGeneratedPoint.position.y - groundMultiplayer + secondRoadOffset.y, roadGeneratedPoint.position.z), Quaternion.identity);
        GameObject.Instantiate(roadPrefab, new Vector3(roadGeneratedPoint.position.x + (i * groundMultiplayer), roadGeneratedPoint.position.y + secondRoadOffset.y, roadGeneratedPoint.position.z), Quaternion.identity);
        GameObject.Instantiate(raodWaterTopPrefab, new Vector3(roadGeneratedPoint.position.x + (i * groundMultiplayer), roadGeneratedPoint.position.y + groundMultiplayer + secondRoadOffset.y, roadGeneratedPoint.position.z), Quaternion.identity);

    }
}
