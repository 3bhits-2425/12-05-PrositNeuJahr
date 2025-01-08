using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public GameObject spawnPointObject;  // Der Spawnpunkt, den die Kamera zuerst zeigen soll
    public GameObject rocketObject;      // Die Rakete, die später explodiert
    public float smoothSpeed = 0.125f;    // Wie sanft die Kamera sich bewegt
    public Vector3 cameraOffset;         // Abstand der Kamera vom Ziel
    public Vector3 spawnOffset;          // Offset, um die Kamera weiter vom Spawnpunkt zu platzieren
    public float delayBeforeStart = 2f;  // Verzögerung bevor die Kamerafahrt beginnt
    public float moveDuration = 3f;      // Dauer der Kamerafahrt
    public float zoomOutDistance = 10f;  // Wie viel die Kamera herauszoomt
    public float lookAtAngle = 15f;      // Der Winkel, in dem die Kamera nach oben schaut

    private bool isMovingToRocket = false; // Ob die Kamera zu einer weiteren Position bewegt wird
    private float startMoveTime;           // Startzeit der Bewegung

    // Start wird nur einmal beim Start des Spiels aufgerufen
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

        // Starte die Kamerafahrt nach der Verzögerung
        Invoke("BeginCameraMovement", delayBeforeStart);
    }

    // Update wird in jedem Frame aufgerufen
    void Update()
    {
        if (isMovingToRocket)
        {
            // Berechne den Fortschritt der Kamerafahrt
            float t = (Time.time - startMoveTime) / moveDuration;
            if (t > 1f) t = 1f;

            // Berechne die neue Position während der Kamerafahrt (Wegzoom)
            Vector3 startPosition = spawnPointObject.transform.position + spawnOffset;
            Vector3 endPosition = rocketObject.transform.position - new Vector3(0, 0, zoomOutDistance); // Wegzoomen
            transform.position = Vector3.Lerp(startPosition, endPosition, t);

            // Richte die Kamera auf den Mittelpunkt der Szene aus
            transform.LookAt(rocketObject.transform);

            if (t == 1f)
            {
                isMovingToRocket = false;
            }
        }
    }

    // Startet die Kamerafahrt
    private void BeginCameraMovement()
    {
        if (rocketObject != null)
        {
            isMovingToRocket = true;
            startMoveTime = Time.time; // Setze die Startzeit der Bewegung
        }
    }
}
