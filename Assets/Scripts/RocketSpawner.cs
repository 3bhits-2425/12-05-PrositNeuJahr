using UnityEngine;

public class RocketSpawner : MonoBehaviour
{
    public GameObject rocketPrefab; // Raketen-Prefab
    public Transform spawnPoint;    // Startpunkt der Rakete

    void Update()
    {
        // Spawne Rakete nur bei Leertaste
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Leertaste gedrückt! Rakete wird gespawnt.");
            Instantiate(rocketPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
