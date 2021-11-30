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
    [SerializeField] float destroyAfterTimeDelay;
    [SerializeField] GameObject hideBomb;
    [SerializeField] AudioSource _bombWickFizz;
    [SerializeField] AudioSource _explodeBombSound;

    private void Start()
    {
        _rb.AddForce(transform.forward * _initialForce, ForceMode.Force);
        StartCoroutine(CountDown());
    }
    IEnumerator CountDown() 
    {
        _bombWickFizz.PlayOneShot(_bombWickFizz.clip, 1.0f);
        yield return new WaitForSeconds(_timeToExplode);
        Explode(); 
    }

    private IEnumerator DestroyGameObject(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    void Explode() 
    {
        Events.OnBombExplode?.Invoke(transform.position, _explosionForce, _range, _totalDamage);
        
        //sound
        if (_bombWickFizz.isPlaying)
        {
            _bombWickFizz.Stop();
        } 
        
        if (_explodeBombSound.clip)
        {
        _explodeBombSound.PlayOneShot(_explodeBombSound.clip, 1.0f);
        }

        //esconder o objeto antes de destrui-lo
        GetComponent<Rigidbody>().isKinematic = true;
        hideBomb.SetActive(false);

        //desabilitar o gameobject apos execucao do som
        IEnumerator destroyGameObject = DestroyGameObject(destroyAfterTimeDelay);
        StartCoroutine(destroyGameObject);
    }
}
