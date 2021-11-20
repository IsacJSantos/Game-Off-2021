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

    float time;
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
        /*  if (Input.GetKeyDown(KeyCode.K))
          {
              targetType = targetType == EnemyTargetType.Anthill ? EnemyTargetType.Player : EnemyTargetType.Anthill;
              SetTarget(targetType);
          }
          if (Input.GetKeyDown(KeyCode.J))
          {
              Hit(1);
          }
        */
        if (enemyMovement.TargetDistance <= enemyMovement.StopDistance + 0.01) 
        {
            print($"{enemyMovement.TargetDistance} {enemyMovement.StopDistance}");
            Attack();
        }
           
    }
    public virtual void Attack()
    {    
        if (!CanAttack()) return;

        time = Time.time;
        _targetTransform.gameObject.GetComponent<IBeatable>()?.Hit(attackForce);
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
        Vector3 pos = GameObject.FindGameObjectWithTag(targetType.ToString()).GetComponent<IAgentTarget>().GetClosestPoint(transform.position);
        enemyMovement.target = pos;
    }

    bool CanAttack()
    {
        return Time.time >= time + AttackCooldown &&
            enemyMovement.target != null &&
            enemyMovement.doMove == true;
    }

}
[System.Serializable]
public enum EnemyTargetType { Player, Anthill };
