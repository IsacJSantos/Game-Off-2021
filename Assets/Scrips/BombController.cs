using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [SerializeField] float _explosionForce;
    [SerializeField] float _range;
    [SerializeField] float _timeToExplode;

    private void Start()
    {
        StartCoroutine(CountDown());
    }
    IEnumerator CountDown() 
    {
        yield return new WaitForSeconds(_timeToExplode);
        Explode();
        
    }
    void Explode() 
    {
        Events.OnBombExplode?.Invoke(transform.position, _explosionForce, _range);
        gameObject.SetActive(false);
    }
}
