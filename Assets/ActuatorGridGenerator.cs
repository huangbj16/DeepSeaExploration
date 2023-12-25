using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActuatorGridGenerator : MonoBehaviour
{
    public GameObject actuatorPrefab;
    public Transform anchorPoint;
    private List<GameObject> actuatorGrid;

    private int col = 4; // Number of columns (horizontal)
    private int row = 5; // Number of rows (vertical)
    private float radius = 40f; // Radius of the hemisphere
    private float padding = Mathf.PI / 6.0f;
    private int[] actuatorIds = new int[] { 10, 9, 8, 7, 6, 1, 2, 3, 4, 5, 40, 39, 38, 37, 36, 31, 32, 33, 34, 35 };

    // Start is called before the first frame update
    void Start()
    {
        // initiate the actuator grid evenly on a hemisphere.
        actuatorGrid = new List<GameObject>();
        for (int i = 0; i < col; i++)
        {
            // Calculate theta for this column
            float theta = padding + (float)i * (Mathf.PI - 2 * padding) / (float)(col - 1);

            for (int j = 0; j < row; j++)
            {
                // Calculate phi for this row
                float phi = padding + (float)j * (Mathf.PI - 2 * padding) / (float)(row - 1); // Only half pi for hemisphere

                // Convert spherical coordinates to Cartesian coordinates for 3D position
                float x = radius * Mathf.Sin(phi) * Mathf.Cos(theta);
                float z = radius * Mathf.Sin(phi) * Mathf.Sin(theta);
                float y = radius * Mathf.Cos(phi);

                // Instantiate or place your object at the calculated position
                Vector3 position = new Vector3(x, y, z);
                //Quaternion rotation = Quaternion.Euler(x, y, z);
                Debug.Log("Point Position " + i + " " + j + " : " + position); // For demonstration, log the position

                // Here you might Instantiate a prefab or mark the position, etc.
                GameObject actuatorObject = Instantiate(actuatorPrefab, transform);
                actuatorObject.transform.localPosition = position;
                actuatorObject.transform.localScale = new Vector3(200, 200, 200);
                actuatorObject.name = "actuator" + actuatorIds[i * row + j];
                actuatorObject.GetComponentInChildren<ActuatorRaycast>().motor_id = actuatorIds[i * row + j];
                actuatorObject.GetComponentInChildren<ActuatorRaycast>().anchorPoint = anchorPoint;
                actuatorGrid.Add(actuatorObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
