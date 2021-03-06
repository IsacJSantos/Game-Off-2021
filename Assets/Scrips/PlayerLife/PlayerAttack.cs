using System.Collections;
using TMPro;
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
    [SerializeField] float _fireDamage;

    [SerializeField] TextMeshProUGUI _bulletsHUDText;

    //sound
    [SerializeField] AudioSource _shot1;
    [SerializeField] AudioSource _reload1;

    bool _isReloading;
    float _baseReloadDelay = 2;
    float time;
    private void Awake()
    {
        Events.OnImproveMagazine += ImproveMagazine;
        Events.OnDecreaseReloadDelay += DecreaseReloadDelay;
        Events.OnImproveDamage += ImproveDamage;
    }
    private void Start()
    {
        _currentBullets = _magazineLength;
    }
    private void OnDestroy()
    {
        Events.OnImproveMagazine -= ImproveMagazine;
        Events.OnDecreaseReloadDelay -= DecreaseReloadDelay;
        Events.OnImproveDamage -= ImproveDamage;
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
            time = Time.time + _fireCooldown;
            BulletController bullet = Instantiate(
                                                    bulletPrefab, bulletOut.transform.position, Quaternion.Euler(
                                                             0, _bodyTransform.localRotation.eulerAngles.y, 0.0f)
                                                 ).GetComponent<BulletController>();

            bullet.damage = _fireDamage;
            _currentBullets--;

            //sound
            PlaySound("SHOT1");

            _bulletsHUDText.text = $"{_currentBullets}/{_magazineLength}";
        }
       
       
    }

    IEnumerator Reload()
    {
        _isReloading = true;

        //sound
        PlaySound("RELOAD1");

        _bulletsHUDText.text = "Reloading...";

        yield return new WaitForSeconds(_reloadDelay);
        _currentBullets = _magazineLength;
        _bulletsHUDText.text = $"{_currentBullets}/{_magazineLength}";
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

    void ImproveDamage() 
    {
        _fireDamage += 1.5f;
    }
    bool CanFire()
    {
        if (_currentBullets <= 0)
        {
            if (!_isReloading)
                StartCoroutine(Reload());
        }

        return time <= Time.time &&
            !_isReloading && _currentBullets > 0;
    }

    public void PlaySound(string soundSelection)
    {
        switch (soundSelection)
        {
            case "SHOT1":
                if (_shot1.clip)
                {
                    _shot1.PlayOneShot(_shot1.clip, 1.0f);
                }
                break;
            case "RELOAD1":
                if (_reload1.clip)
                {
                    _reload1.PlayOneShot(_reload1.clip, 1.0f);
                }
                break;
            default:
                Debug.Log("Sound not found");
                break;
        }
    }
}
