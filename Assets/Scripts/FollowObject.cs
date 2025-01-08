using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public GameObject spawnPointObject;  // Der Spawnpunkt, den die Kamera zuerst zeigen soll
    public GameObject rocketObject;      // Die Rakete, die später startet
    public float smoothSpeed = 0.125f;   // Wie sanft die Kamera sich bewegt
    public Vector3 spawnOffset;          // Offset der Kamera beim Spawnpunkt
    public Vector3 zoomOutOffset;        // Offset der Kamera beim Herauszoomen
    public float lookAtAngle = 15f;      // Der Winkel, in dem die Kamera nach oben schaut

    private bool isFollowingRocket = false; // Ob die Kamera der Rakete folgt

    void Start()
    {
        if (spawnPointObject != null)
        {
            // Setze die Kamera an die Startposition, mit dem Versatz nach hinten und oben
            Vector3 startPosition = spawnPointObject.transform.position + spawnOffset;
            transform.position = startPosition;

            // Richte die Kamera leicht nach oben aus, um den Spawnpunkt und die Umgebung zu sehen
            transform.LookAt(spawnPointObject.transform.position + new Vector3(0, lookAtAngle, 0));
        }
        else
        {
            Debug.LogWarning("SpawnPointObject ist nicht zugewiesen!");
        }
    }

    void Update()
    {
        if (isFollowingRocket && rocketObject != null)
        {
            // Berechne die Zielposition, während die Kamera herauszoomt und nach oben schaut
            Vector3 targetPosition = rocketObject.transform.position + zoomOutOffset;

            // SmoothDamp für sanfte Bewegung zur Zielposition
            Vector3 velocity = Vector3.zero; // Temporäre Variable für SmoothDamp
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothSpeed);

            // Kamera auf die Rakete ausrichten
            transform.LookAt(rocketObject.transform.position + new Vector3(0, lookAtAngle, 0));
        }
    }

    // Diese Methode sollte aufgerufen werden, wenn eine Rakete gestartet wird
    public void StartFollowingRocket()
    {
        if (rocketObject != null)
        {
            isFollowingRocket = true;
        }
        else
        {
            Debug.LogWarning("RocketObject ist nicht zugewiesen!");
        }
    }
}
