using UnityEngine;

public class BaseLifeSystem : MonoBehaviour
{
    public float MaxLife { get { return _maxLife; } }
    [SerializeField] public GameObject takeDamageParticle;
    [SerializeField] public GameObject dieParticle;
    int counter = 0;
    [SerializeField] AudioSource takeTamageSound;
    [SerializeField] AudioSource dieSound;

    public float Life
    {
        get
        {
            return _currentLife;
        }
        set
        {
            _currentLife = value;

            if (_currentLife <= 0)
            {
                _currentLife = 0;
                _isAlive = false;
                Die();
            } else if (_currentLife > 0 && _isAlive)
            {
                TakeDamage();
            }
            else 
            {
                if (_currentLife > _maxLife)
                    _currentLife = _maxLife;
            }
        }
    }

    public bool IsAlive { get { return _isAlive; } }

    [SerializeField] float _baseLife;
    [SerializeField] float _currentLife;
    [SerializeField] float _maxLife;
    [SerializeField] bool _isAlive;

    public virtual void Awake()
    {
       
    }

    public virtual void Start()
    {
        _currentLife = _maxLife;
        _isAlive = true;
    }

    public virtual void OnDestroy()
    {
      
    }

    public void TakeDamage()
    {
        if (IsAlive)
        {
            if (takeDamageParticle)
            {
                PlayTakeDamageParticle();
            }

            if (takeTamageSound)
            {
                takeTamageSound.PlayOneShot(takeTamageSound.clip, 1.0f);
            }
        }
    }

    public void Die()
    {
        if (!IsAlive && counter < 1)
        {
            counter += 1; //tocar a particula e som de morte apenas uma vez
      
            if (dieParticle)
            {
                PlayDieParticle();
            }

            if (dieSound.clip)
            {
                dieSound.PlayOneShot(dieSound.clip, 1.0f);
            }
        }
    }

    //configurar particulas
    //  colocar o prefab da particula como objeto filho e alocar pelo inspetor
    //  posicionar particula pela hierarquia
    public void PlayTakeDamageParticle()
    {
        ParticleSystem particle = takeDamageParticle.GetComponent<ParticleSystem>();
        particle.Play();
    }

    public void PlayDieParticle()
    {
        ParticleSystem particle = dieParticle.GetComponent<ParticleSystem>();
        particle.Play();
    }

    public virtual void ImproveMaxLife(float amount)
    {
        float percent = amount / 100;
        _maxLife += _baseLife * percent;
    }

}
