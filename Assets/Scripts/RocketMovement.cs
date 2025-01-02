using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    public float thrust = 10f;                // Schubkraft
    public GameObject explosionPrefab;        // Explosionseffekt
    public AudioClip rocketSound;             // Sound für die Rakete
    private Rigidbody rb;
    public ParticleSystem rocketParticleSystem; // Partikelsystem der Rakete
    private AudioSource audioSource;           // AudioSource-Komponente

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        rocketParticleSystem = GetComponent<ParticleSystem>(); // Partikelsystem der Rakete holen

        // Gravitation deaktivieren
        rb.useGravity = false;

        // Starte den Schub (Raketensound)
        PlayRocketSound();

        // Zerstöre die Rakete nach 5 Sekunden
        Invoke("DestroyRocket", 5f);

        // Initialisiere das Partikelsystem
        InitializeRocketParticles();
    }

    void FixedUpdate()
    {
        // Füge konstant Schub nach oben hinzu
        rb.AddForce(transform.up * thrust, ForceMode.Acceleration);

        // Setze die Partikelfarbe von Rot zu Schwarz
        if (rocketParticleSystem != null)
        {
            SetRocketParticleColor();
        }

    private void DestroyRocket()
    {
        // Erzeuge Explosion an der Position der Rakete
        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            // Explosion enthält das Explosion-Skript, das die Farben setzt
            explosion.AddComponent<Explosion>();
        }

        // Zerstöre die Rakete
        Destroy(gameObject);
    }

    private void PlayRocketSound()
    {
        if (audioSource != null && rocketSound != null)
        {
            audioSource.PlayOneShot(rocketSound); // Spielt den Raketen-Sound ab
        }
    }

    private void InitializeRocketParticles()
    {
        // Konfiguriere das Partikelsystem der Rakete
        var main = rocketParticleSystem.main;
        main.startLifetime = 1f;  // Lebensdauer der Partikel
        main.startSize = 0.5f;    // Größe der Partikel
        main.startSpeed = 5f;     // Geschwindigkeit der Partikel
   

        // Form des Partikelsystems: Kegelförmige Vertei

        // Setze die Farbe der Partikel von Gelb zu Schwarz
        SetRocketParticleColor();
    }

    private void SetRocketParticleColor()
    {
        // Setze das colorOverLifetime des Partikelsystems
        var colorOverLifetime = rocketParticleSystem.colorOverLifetime;
        colorOverLifetime.enabled = true;

        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(Color.red, 0f),    // Anfangsfarbe Rot
                new GradientColorKey(Color.black, 1f)      // Endfarbe Schwarz
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(1f, 0f),   // Volle Deckkraft am Anfang
                new GradientAlphaKey(0f, 1f)    // Transparenz am Ende
            }
        );
        colorOverLifetime.color = new ParticleSystem.MinMaxGradient(gradient);

        // Setze die Geschwindigkeit der Partikel im Laufe ihrer Lebensdauer
        var velocityOverLifetime = rocketParticleSystem.velocityOverLifetime;
        velocityOverLifetime.enabled = true;
        velocityOverLifetime.y = new ParticleSystem.MinMaxCurve(0f, -2f); // Langsame Geschwindigkeit nach oben
    }
}
