using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class BaseEnemyController : MonoBehaviour
{
    [SerializeField] protected EnemyTargetType targetType;
    [SerializeField] protected float maxLife;
    protected EnemyMovement enemyMovement;

    Transform _playerTransform;
    Transform _enemyTransform;

    private void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _enemyTransform = GameObject.FindGameObjectWithTag("Anthill").GetComponent<Transform>();
    }
    private void Start()
    {
        SetTarget(targetType);
        enemyMovement.doMove = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            targetType = targetType == EnemyTargetType.Anthill ? EnemyTargetType.Player : EnemyTargetType.Anthill;
            SetTarget(targetType);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Hit(1);
        }
    }
    public virtual void Attack()
    {
        Debug.LogWarning($"Attack not implemented in enemy {gameObject.name}. Or is calling base.Attack");
    }
    public virtual void Hit(float value) 
    {
        maxLife -= value;

        if (maxLife <= 0) 
        {
            Die();
        }
      
    }

    public virtual void Die() 
    {
        enemyMovement.doMove = false;
        gameObject.SetActive(false);
    }

    public virtual void SetTarget(EnemyTargetType targetType)
    {
        switch (targetType)
        {
            case EnemyTargetType.Player:
                enemyMovement.target = _playerTransform.position;
                break;
            case EnemyTargetType.Anthill:
                enemyMovement.target = _enemyTransform.position;
                break;
            default:
                break;
        }
    }

  
}
[System.Serializable]
public enum EnemyTargetType { Player, Anthill };
