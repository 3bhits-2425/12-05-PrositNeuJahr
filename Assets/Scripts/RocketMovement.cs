using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    public float thrust = 10f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(Vector3.up * thrust, ForceMode.Force);
    }
}
