using UnityEngine;

public class LifeManager : MonoBehaviour
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

    private void Awake()
    {
        Events.OnImprovePlayerLife += ImproveMaxLife;
    }

    private void Start()
    {
        _currentLife = _maxLife;
        _isAlive = true;
    }

    private void OnDestroy()
    {
        Events.OnImprovePlayerLife -= ImproveMaxLife;
    }


    void ImproveMaxLife(float amount)
    {
        float percent = amount / 100;
        _maxLife += _baseLife * percent;
    }
   
}
