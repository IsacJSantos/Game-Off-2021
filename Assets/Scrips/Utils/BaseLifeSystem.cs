using UnityEngine;

public class BaseLifeSystem : MonoBehaviour
{
    public float MaxLife { get { return _maxLife; } }
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


    public virtual void ImproveMaxLife(float amount)
    {
        print("Improve");
        float percent = amount / 100;
        _maxLife += _baseLife * percent;
    }

}
