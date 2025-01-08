using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    public float thrust = 10f;                // Schubkraft
    public GameObject explosionPrefab;        // Explosionseffekt-Prefab
    public GameObject enginePrefab;           // Antriebseffekt-Prefab
    public float randomRange = -2f;       // Range für zufällige Richtungsänderung

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

        // Zufällige Zerstörungszeit zwischen 4 und 6 Sekunden
        float randomDestroyTime = Random.Range(4f, 6f);
        Invoke("DestroyRocket", randomDestroyTime);
    }

    void FixedUpdate()
    {
        // Füge konstant Schub nach oben hinzu
        rb.AddForce(transform.up * thrust, ForceMode.Acceleration);

        // Zufällige leichte Richtungsänderung
        Vector3 randomDirection = new Vector3(
            Random.Range(-randomRange, randomRange),
            Random.Range(-randomRange, randomRange),
            Random.Range(-randomRange, randomRange)
        );

        rb.AddForce(randomDirection, ForceMode.Acceleration);
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
