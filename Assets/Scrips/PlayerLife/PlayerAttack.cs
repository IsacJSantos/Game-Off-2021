using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Transform _bodyTransform;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject bulletOut;

    [SerializeField] int _magazineLength;
    [SerializeField] int _currentBullets;

    [SerializeField] float _reloadDelay;
    [SerializeField] float _fireCooldown;

    //sound
    [SerializeField] List<GameObject> _thisGameObjectSounds = new List<GameObject>();
    int soundIndex;
    AudioSource _audioSource;
    AudioClip _audioClip;

    bool _isReloading;
    float _baseReloadDelay = 2;
    float time;
    private void Awake()
    {
        Events.OnImproveMagazine += ImproveMagazine;
        Events.OnDecreaseReloadDelay += DecreaseReloadDelay;
    }
    private void Start()
    {
        _currentBullets = _magazineLength;
    }
    private void OnDestroy()
    {
        Events.OnImproveMagazine -= ImproveMagazine;
        Events.OnDecreaseReloadDelay += DecreaseReloadDelay;
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!_isReloading)
                StartCoroutine(Reload());
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Events.OnImproveMagazine?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            Events.OnDecreaseReloadDelay?.Invoke();
        }
    }

    void Fire()
    {
        if (CanFire())
        {
            time = Time.time;
            Instantiate(bulletPrefab, bulletOut.transform.position, Quaternion.Euler(0, _bodyTransform.localRotation.eulerAngles.y, 0.0f));
            //PoolingSystem.Instancia.GetObjeto("Bullet", bulletOut.transform.position, Quaternion.Euler(0, _bodyTransform.localRotation.eulerAngles.y, 0.0f));
            _currentBullets--;
            SelectSound("SHOT1");
        }
    }

    IEnumerator Reload()
    {
        _isReloading = true;
        SelectSound("RELOAD");
        yield return new WaitForSeconds(_reloadDelay);
        _currentBullets = _magazineLength;
        _isReloading = false;
    }

    void ImproveMagazine()
    {
        _magazineLength += 5;
    }

    void DecreaseReloadDelay()
    {
        _reloadDelay -= _baseReloadDelay * 0.09f;
        if (_reloadDelay <= 0.3f)
            _reloadDelay = 0.2f;
    }

    bool CanFire()
    {
        return (_fireCooldown + time) <= Time.time &&
            !_isReloading && _currentBullets > 0;
    }

    public void SelectSound(string soundSelection)
    {
        switch (soundSelection)
        {
            case "SHOT1":
                soundIndex = 0;        
                break;
            case "RELOAD":
                soundIndex = 2;
                break;
            default:
                Debug.Log("Sound not found");
                break;
        }
        PlaySound(soundIndex);
    }

    public void PlaySound(int soundIndex)
    {
        if (_thisGameObjectSounds[soundIndex].GetComponent<AudioSource>())
        {
            _audioSource = _thisGameObjectSounds[soundIndex].GetComponent<AudioSource>();
            if (_audioSource.clip)
            {
                _audioClip = _thisGameObjectSounds[soundIndex].GetComponent<AudioSource>().clip;
                _audioSource.PlayOneShot(_audioClip, 1.0f);
            }
        }
    }
}
