using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float damage;
    [SerializeField] Rigidbody rb;
    [SerializeField] float velocity;

    private void Update()
    {
        rb.velocity = transform.forward * velocity;

    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            col.GetComponent<IBeatable>().Hit(damage);
        }
    }
}
