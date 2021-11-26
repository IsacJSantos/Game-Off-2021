using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempScript : MonoBehaviour
{
    [SerializeField] Collider col;
    [SerializeField] Rigidbody _rb;
    [SerializeField] GameObject ong;
    private void Awake()
    {
        Events.OnBombExplode += CalculateExplosionHit;
    }
    private void OnDestroy()
    {
        Events.OnBombExplode -= CalculateExplosionHit;
    }


    void CalculateExplosionHit(Vector3 explosionPos, float force, float range,float damage)
    {
        if (Vector3.Distance(transform.position, explosionPos) > range)
        {
            print("Returning");
            return;
        }
        Vector3 forcePoint = col.ClosestPointOnBounds(explosionPos);
       
        Vector3 forceVector = transform.position - forcePoint;
     
        float x = (forceVector.x < 0 ? -1 : 1);
        float z = (forceVector.z < 0 ? -1 : 1);
        _rb.AddForce(new Vector3(x, 0, z) * force, ForceMode.Force);

    }
}
