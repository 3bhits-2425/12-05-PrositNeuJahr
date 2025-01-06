using UnityEngine;

public class EngineEffect : MonoBehaviour
{
    public ParticleSystem engineParticleSystem; // Partikelsystem des Antriebs
    public float offsetDistance = 0.5f;         // Abstand des Partikelsystems nach unten

    private void Start()
    {
        engineParticleSystem = GetComponent<ParticleSystem>();

        if (engineParticleSystem != null)
        {
            ConfigureEngineParticleSystem();

            // Positioniere das Partikelsystem weiter unten
            AdjustPosition();

            engineParticleSystem.Play();
        }
    }

    private void ConfigureEngineParticleSystem()
    {
        var main = engineParticleSystem.main;
        var emission = engineParticleSystem.emission;
        var shape = engineParticleSystem.shape;
        var colorOverLifetime = engineParticleSystem.colorOverLifetime;

        // Hauptparameter des Antriebs-Partikelsystems
        main.startLifetime = 0.8f;  // Kürzere Lebensdauer der Partikel
        main.startSize = 0.2f;      // Kleinere Partikel
        main.startSpeed = 3f;       // Etwas langsamere Geschwindigkeit
        main.simulationSpace = ParticleSystemSimulationSpace.World; // Welt-Raum

        // Mehr Partikel erzeugen
        emission.rateOverTime = 50; // Höhere Emissionsrate

        // Form des Partikelsystems - Kegelförmig, nach unten gerichtet
        shape.shapeType = ParticleSystemShapeType.Cone;
        shape.angle = 25f;             // Breiterer Kegel
        shape.radius = 0.1f;           // Kleinere Basis
        shape.position = new Vector3(0f, -0.1f, 0f); // Leichte Verschiebung nach unten
        shape.rotation = new Vector3(180f, 0f, 0f);  // Drehung nach unten

        // Farbverlauf (Rot zu Schwarz)
        colorOverLifetime.enabled = true;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[]
            {
                new GradientColorKey(Color.red, 0f),    // Anfangsfarbe Rot
                new GradientColorKey(Color.black, 1f)  // Endfarbe Schwarz
            },
            new GradientAlphaKey[]
            {
                new GradientAlphaKey(1f, 0f),   // Volle Deckkraft am Anfang
                new GradientAlphaKey(0f, 1f)    // Transparenz am Ende
            }
        );
        colorOverLifetime.color = new ParticleSystem.MinMaxGradient(gradient);
    }

    private void AdjustPosition()
    {
        // Verschiebe das Partikelsystem leicht nach unten
        transform.localPosition += new Vector3(0f, -offsetDistance, 0f);
    }
}
