using UnityEngine;

public class Explosion : MonoBehaviour
{
    public ParticleSystem ParticleSystem;
    public AudioSource audio;

    private void Start()
    {
        ParticleSystem = GetComponent<ParticleSystem>();
        audio = GetComponent<AudioSource>();

        // Konfiguriere das Partikelsystem
        ConfigureParticleSystem();

        // Starte den Explosionseffekt
        if (ParticleSystem != null)
        {
            ParticleSystem.Play();
        }

        // Starte den Explosionssound
        if (audio != null)
        {
            audio.Play();
        }

        // Zerstöre die Explosion nach der Effekt-Dauer
        Destroy(gameObject, ParticleSystem.main.duration + ParticleSystem.main.startLifetime.constant);
    }

    private void ConfigureParticleSystem()
    {
        if (ParticleSystem == null) return;

        // Hole die Module des Partikelsystems
        var main = ParticleSystem.main;
        var emission = ParticleSystem.emission;
        var shape = ParticleSystem.shape;
        var colorOverLifetime = ParticleSystem.colorOverLifetime;

        // Hauptparameter des Partikelsystems (Explosionseffekt)
      // Dauer des Effekts
        main.startLifetime = 1f; // Lebensdauer der Partikel
        main.startSpeed = 10f; // Geschwindigkeit
        main.startSize = 1.6f; // Größe
        main.startColor = Color.white; // Basisfarbe
        main.loop = false; // Einmalige Explosion
        main.playOnAwake = false; // Nicht automatisch abspielen

        // Emission - Burst (explosionsartige Partikelausgabe)
        emission.rateOverTime = 0; // Keine kontinuierliche Emission
        emission.SetBursts(new ParticleSystem.Burst[]
        {
            new ParticleSystem.Burst(0f, 50, 100) // 50-100 Partikel auf einmal
        });

        // Form - Kugelförmige Verteilung der Partikel
        shape.shapeType = ParticleSystemShapeType.Sphere;
        shape.radius = 1f;

        // Farbe - Dynamische Farbänderung während der Explosion
        colorOverLifetime.enabled = true;

        // Liste von typischen Feuerwerksfarben
        Color[] fireworkColors =
        {
            Color.red, Color.blue, Color.green, Color.yellow, Color.magenta, Color.cyan, new Color(1f, 0.5f, 0f) // Orange
        };

        // Zufällige Farben aus der Liste auswählen
        Color startColor = fireworkColors[Random.Range(0, fireworkColors.Length)];
        Color endColor = fireworkColors[Random.Range(0, fireworkColors.Length)];

        // Setze den Farbverlauf
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[]
            {
                new GradientColorKey(startColor, 0f), // Anfangsfarbe
                new GradientColorKey(endColor, 1f)    // Endfarbe
            },
            new GradientAlphaKey[]
            {
                new GradientAlphaKey(1f, 0f),   // Volle Deckkraft am Anfang
                new GradientAlphaKey(0f, 1f)    // Transparenz am Ende
            }
        );
        colorOverLifetime.color = new ParticleSystem.MinMaxGradient(gradient);
    }
}
