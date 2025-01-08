using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    public float thrust = 10f;                // Schubkraft
    public GameObject explosionPrefab;        // Explosionseffekt-Prefab
    public GameObject enginePrefab;           // Antriebseffekt-Prefab
    public float randomRange = -2f;       // Range f�r zuf�llige Richtungs�nderung

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

        // Zuf�llige Zerst�rungszeit zwischen 4 und 6 Sekunden
        float randomDestroyTime = Random.Range(4f, 6f);
        Invoke("DestroyRocket", randomDestroyTime);
    }

    void FixedUpdate()
    {
        // F�ge konstant Schub nach oben hinzu
        rb.AddForce(transform.up * thrust, ForceMode.Acceleration);

        // Zuf�llige leichte Richtungs�nderung
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

        // Zerst�re die Rakete
        Destroy(gameObject);
    }
}
