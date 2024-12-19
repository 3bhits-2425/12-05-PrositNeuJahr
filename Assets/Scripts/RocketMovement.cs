using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    public float thrust = 10f;      // Schubkraft
    private Rigidbody rb;
    private bool isThrusting = false; // Bewegung aktivieren/deaktivieren

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 5f);

        // Rakete startet mit Schub
        isThrusting = true; 
    }


    void Update()
    {
        // Bewegung nur bei Tastendruck aktivieren
        if (Input.GetKey(KeyCode.W))
        {
            isThrusting = true;
        }
        else
        {
            isThrusting = false;
        }
    }

    void FixedUpdate()
    {
        // Füge Schub hinzu, wenn isThrusting aktiviert ist
        if (isThrusting)
        {
            rb.AddForce(Vector3.up * thrust, ForceMode.Acceleration);
        }
    }
}
