using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject Sphere; // Einzelnes Objekt (Kugel)
    public ParticleSystem ParticleSystem;
    private ParticleSystemRenderer particleRenderer;
    public AudioSource audio;

    private void Start()
    {
        // Zugriff auf das Partikelsystem und seinen Renderer
        ParticleSystem = GetComponent<ParticleSystem>();
        particleRenderer = GetComponent<ParticleSystemRenderer>();
        audio = GetComponent<AudioSource>();

        // Initiale Konfiguration des Partikelsystems
        ConfigureParticleSystem();
        StartExplosion();
    }

    private void ConfigureParticleSystem()
    {
        // Stoppe das Partikelsystem vor der Konfiguration
        if (ParticleSystem.isPlaying)
        {
            ParticleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        // Hole die Einstellungen des Partikelsystems
        var main = ParticleSystem.main;
        var emission = ParticleSystem.emission;
        var shape = ParticleSystem.shape;

        // Hauptparameter des Partikelsystems (Explosionseffekt)
        main.duration = 2f; // Dauer des Effekts
        main.startLifetime = 1f; // Lebensdauer der Partikel
        main.startSpeed = 10f; // Geschwindigkeit
        main.startSize = 0.3f; // Größe
        main.startColor = Color.white; // Basisfarbe
        main.loop = false; // Einmalige Explosion
        main.playOnAwake = false; // Nicht automatisch abspielen

        // Emission - Burst (explosionsartige Partikelausgabe)
        emission.rateOverTime = 0; // Keine kontinuierliche Emission
        emission.SetBursts(new ParticleSystem.Burst[] {
            new ParticleSystem.Burst(0f, 50, 100) // 50-100 Partikel auf einmal
        });

        // Form - Kugelförmige Verteilung der Partikel
        shape.shapeType = ParticleSystemShapeType.Sphere;
        shape.radius = 1f;

        // Renderer - Mesh aktivieren
        particleRenderer.renderMode = ParticleSystemRenderMode.Mesh;
        particleRenderer.mesh = Sphere.GetComponent<MeshFilter>().sharedMesh; // Setze Kugel als Mesh
        particleRenderer.material = new Material(Shader.Find("Particles/Standard Unlit"));
    }

    private void StartExplosion()
    {
        // Liste von typischen Feuerwerksfarben
        Color[] fireworkColors = {
        Color.red, Color.blue, Color.green, Color.yellow, Color.magenta, Color.cyan, new Color(1f, 0.5f, 0f) // Orange
    };

        // Zufällige Farben aus der Liste auswählen
        Color startColor = fireworkColors[Random.Range(0, fireworkColors.Length)];
        Color endColor = fireworkColors[Random.Range(0, fireworkColors.Length)];

        // Setze das ColorOverLifetime-Modul
        var colorOverLifetime = ParticleSystem.colorOverLifetime;
        colorOverLifetime.enabled = true;

        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] {
            new GradientColorKey(startColor, 0f), // Anfangsfarbe
            new GradientColorKey(endColor, 1f)    // Endfarbe
            },
            new GradientAlphaKey[] {
            new GradientAlphaKey(1f, 0f),   // Volle Deckkraft am Anfang
            new GradientAlphaKey(0f, 1f)    // Transparenz am Ende
            }
        );
        colorOverLifetime.color = new ParticleSystem.MinMaxGradient(gradient);

        // Starte die Explosion
        ParticleSystem.Play();
        // starte audio
        audio.Play();
        
        
    }

}
