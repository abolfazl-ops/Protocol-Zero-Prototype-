using UnityEngine;
using System.Collections;
public class Bullet : MonoBehaviour
{
    public float speed = 15f;
    public float lifetime = 2f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
// The bullet moves backward relative to its transform to match the barrel orientation
        rb.linearVelocity = -transform.right * speed;

// Destroy the bullet after its lifetime to save memory
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
// Destroy the bullet on impact
        Destroy(gameObject);
    }
}