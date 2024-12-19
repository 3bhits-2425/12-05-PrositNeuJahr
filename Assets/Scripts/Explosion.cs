using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworkScript : MonoBehaviour
{
    public GameObject[] Particles; // Array von Mesh-Partikeln
    public ParticleSystem ParticleSystem;
    private ParticleSystemRenderer particleRenderer;

    private void Start()
    {
        // Zugriff auf das Partikelsystem und seinen Renderer
        ParticleSystem = GetComponent<ParticleSystem>();
        particleRenderer = GetComponent<ParticleSystemRenderer>();

        // Initiale Konfiguration des Partikelsystems
        ConfigureParticleSystem();
    }

    private void Update()
    {
        if (ParticleSystem != null && Particles.Length > 0)
        {
            // Wähle ein zufälliges Mesh aus dem Array
            int randomIndex = Random.Range(0, Particles.Length);
            Mesh mesh = Particles[randomIndex].GetComponent<MeshFilter>().sharedMesh;

            // Setze das Mesh im Partikelrenderer
            if (mesh != null)
            {
                particleRenderer.mesh = mesh;
                Debug.Log("Selected Mesh: " + mesh.name);
            }

            // Optional: Starten des Partikelsystems, falls es nicht läuft
            if (!ParticleSystem.isPlaying)
            {
                ParticleSystem.Play();
            }
        }
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
        main.startColor = Color.white; // Farbe der Partikel
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

        // Renderer - Mesh aktivieren
        particleRenderer.renderMode = ParticleSystemRenderMode.Mesh;
        particleRenderer.material = new Material(Shader.Find("Particles/Standard Unlit"));
    }

}
