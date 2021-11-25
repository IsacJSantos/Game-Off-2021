using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class BaseEnemyController : MonoBehaviour, IBeatable
{
    [SerializeField] protected EnemyTargetType targetType;
    [SerializeField] protected float maxLife;
    [SerializeField] protected float AttackCooldown = 1;
    [SerializeField] protected float attackForce;

    protected EnemyMovement enemyMovement;

    Transform _targetTransform;
    IAgentTarget agentTarget;
    IBeatable beatableTarget;

    public float time;
    public virtual void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        _targetTransform = GameObject.FindGameObjectWithTag(targetType.ToString()).GetComponent<Transform>();

        if (AttackCooldown <= 0)
            AttackCooldown = 1;
    }
    public virtual void Start()
    {
        SetTarget(targetType);
        enemyMovement.doMove = true;
    }
    public virtual void Update()
    {      
        if (enemyMovement.CanAttack)
        {       
            Attack();
        }
       
        Move();
    }
    public virtual void Attack()
    {
        if (!CanAttack()) return;

        time = Time.time;
        beatableTarget?.Hit(attackForce);
    }

    public virtual void Hit(float value)
    {
        maxLife -= value;

        if (maxLife <= 0)
        {
            Die();
        }

    }
    void Move()
    {
        enemyMovement.target = agentTarget.GetClosestPoint(transform.position);
    }

    public virtual void Die()
    {
        enemyMovement.doMove = false;
        gameObject.SetActive(false);
    }

    public virtual void SetTarget(EnemyTargetType targetType)
    {
        agentTarget = GameObject.FindGameObjectWithTag(targetType.ToString()).GetComponent<IAgentTarget>();
        beatableTarget = GameObject.FindGameObjectWithTag(targetType.ToString()).GetComponent<IBeatable>();

    }

    bool CanAttack()
    {
        return Time.time >= (time + AttackCooldown) &&
            enemyMovement.target != null;
    }

}
[System.Serializable]
public enum EnemyTargetType { Player, Anthill };
