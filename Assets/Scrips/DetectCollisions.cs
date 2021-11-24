using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
   [SerializeField] Rigidbody rb;
   [SerializeField] float velocity;
    private void Update()
    {
        rb.velocity = transform.forward * velocity;
    
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform);
    }
}
