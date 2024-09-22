using UnityEngine;
using System.Collections.Generic;

public class TrackGenerator : MonoBehaviour
{
    public List<Transform> controlPoints; // Track control points
    public GameObject trackSegmentPrefab; // Prefab for track segments
    public GameObject[] buildingPrefabs; // Array of building prefabs
    public float buildingOffset = 15f; // Distance from the track
    public float buildingSpacing = 20f; // Distance between buildings

    void Start()
    {
        GenerateTrack();
        GenerateBuildings();
    }

    void GenerateTrack()
    {
        for (int i = 0; i < controlPoints.Count - 1; i++)
        {
            Vector3 pointA = controlPoints[i].position;
            Vector3 pointB = controlPoints[i + 1].position;
            CreateTrackSegment(pointA, pointB);
        }
    }

    void CreateTrackSegment(Vector3 pointA, Vector3 pointB)
    {
        GameObject trackSegment = Instantiate(trackSegmentPrefab, (pointA + pointB) / 2, Quaternion.identity);
        trackSegment.transform.LookAt(pointB);
        float segmentLength = Vector3.Distance(pointA, pointB);
        trackSegment.transform.localScale = new Vector3(1, 1, segmentLength);
    }

    void GenerateBuildings()
    {
        for (int i = 0; i < controlPoints.Count - 1; i++)
        {
            Vector3 pointA = controlPoints[i].position;
            Vector3 pointB = controlPoints[i + 1].position;
            Vector3 direction = (pointB - pointA).normalized;

            // Place buildings along the track segment
            float segmentLength = Vector3.Distance(pointA, pointB);
            int numBuildings = Mathf.FloorToInt(segmentLength / buildingSpacing);

            for (int j = 0; j < numBuildings; j++)
            {
                float t = (j * buildingSpacing) / segmentLength; // Interpolate between points
                Vector3 buildingPosition = Vector3.Lerp(pointA, pointB, t);

                // Offset the buildings to the left and right of the track
                Vector3 leftOffset = Vector3.Cross(direction, Vector3.up) * buildingOffset;
                Vector3 rightOffset = -leftOffset;

                // Place buildings on both sides
                PlaceBuilding(buildingPosition + leftOffset);
                PlaceBuilding(buildingPosition + rightOffset);
            }
        }
    }

    void PlaceBuilding(Vector3 position)
    {
        // Randomly choose a building prefab
        GameObject buildingPrefab = buildingPrefabs[Random.Range(0, buildingPrefabs.Length)];
        GameObject building = Instantiate(buildingPrefab, position, Quaternion.identity);

        // Optional: Randomize the building's scale for variety
        float randomScale = Random.Range(0.9f, 1.1f);
        building.transform.localScale *= randomScale;

        // Rotate building to face the track
        building.transform.LookAt(position - transform.position); // Rotate towards the center of the track
    }
}
