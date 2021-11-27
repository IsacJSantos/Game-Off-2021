using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [SerializeField] float _explosionForce;
    [SerializeField] float _range;
    [SerializeField] float _timeToExplode;
    [SerializeField] float _totalDamage;
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _initialForce;
    [SerializeField] AudioSource _explodeBombSound;

    private void Start()
    {
        _rb.AddForce(transform.forward * _initialForce, ForceMode.Force);
        StartCoroutine(CountDown());
    }
    IEnumerator CountDown() 
    {
        yield return new WaitForSeconds(_timeToExplode);
        Explode();
        
    }
    void Explode() 
    {
        Events.OnBombExplode?.Invoke(transform.position, _explosionForce, _range, _totalDamage);
        
        //sound
        if (_explodeBombSound.clip)
        {
        _explodeBombSound.PlayOneShot(_explodeBombSound.clip, 1.0f);
        }

        gameObject.SetActive(false);
    }
}
