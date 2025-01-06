using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public GameObject target;  // Das Zielobjekt, dem die Kamera folgen soll
    public float smoothSpeed = 0.125f;  // Wie sanft die Kamera folgt
    public Vector3 offset;  // Abstand zur Kamera

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.transform.position + offset;  // Wunschposition der Kamera
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);  // Sanfte Bewegung
            transform.position = smoothedPosition;  // Kamera auf die neue Position setzen

            transform.LookAt(target.transform);  // Kamera auf das Zielobjekt ausrichten
        }
    }
}
