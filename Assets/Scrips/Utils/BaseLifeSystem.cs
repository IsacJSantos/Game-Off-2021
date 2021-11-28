using UnityEngine;

public class BaseLifeSystem : MonoBehaviour
{
    public float MaxLife { get { return _maxLife; } }
    public GameObject dieParticle;
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


    public void PlayDieParticle()
    //config:   colocar o prefab da particula como objeto filho e alocar pelo inspetor
    //          posicionar particula pela hierarquia
    {
        ParticleSystem particle = dieParticle.GetComponent<ParticleSystem>();
        particle.Play();
        //Debug.Log(transform + "die particle");
    }

    public void PlayTakeDamageParticle()
    {
        //GameObject clone = Instantiate(dieParticle, dieParticle.transform.position, Quaternion.identity);
        //ParticleSystem particle = clone.GetComponent<ParticleSystem>();
    }


    public virtual void ImproveMaxLife(float amount)
    {
        float percent = amount / 100;
        _maxLife += _baseLife * percent;
    }

}
