using UnityEngine;

public class EngineEffect : MonoBehaviour
{
    public ParticleSystem engineParticleSystem;

    private void Start()
    {
        engineParticleSystem = GetComponent<ParticleSystem>();

        if (engineParticleSystem != null)
        {
            ConfigureEngineParticleSystem();
            engineParticleSystem.Play();
        }
    }

    private void ConfigureEngineParticleSystem()
    {
        var main = engineParticleSystem.main;
        var colorOverLifetime = engineParticleSystem.colorOverLifetime;

        // Hauptparameter des Antriebs-Partikelsystems
        main.startLifetime = 1f;  // Lebensdauer der Partikel
        main.startSize = 0.5f;    // Größe der Partikel
        main.startSpeed = 5f;     // Geschwindigkeit der Partikel

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
}
