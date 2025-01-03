using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    public float thrust = 10f;                // Schubkraft
    public GameObject explosionPrefab;        // Explosionseffekt-Prefab
    public GameObject enginePrefab;           // Antriebseffekt-Prefab

    private Rigidbody rb;                     // Rigidbody-Komponente

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Gravitation deaktivieren
        rb.useGravity = false;

        // Starte den Antriebseffekt
        if (enginePrefab != null)
        {
            Instantiate(enginePrefab, transform.position, transform.rotation, transform);
        }

        // Zerstöre die Rakete nach 5 Sekunden
        Invoke("DestroyRocket", 5f);
    }

    void FixedUpdate()
    {
        // Füge konstant Schub nach oben hinzu
        rb.AddForce(transform.up * thrust, ForceMode.Acceleration);
    }

    private void DestroyRocket()
    {
        // Erzeuge Explosion an der Position der Rakete
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        // Zerstöre die Rakete
        Destroy(gameObject);
    }
}
