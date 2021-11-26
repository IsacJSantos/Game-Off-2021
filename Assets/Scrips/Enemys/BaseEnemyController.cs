using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class BaseEnemyController : MonoBehaviour, IBeatable
{
    [SerializeField] Collider _col;
    [SerializeField] Rigidbody _rb;
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

        Events.OnBombExplode += CalculateExplosionHit;
    }
    public virtual void Start()
    {
        SetTarget(targetType);
        enemyMovement.doMove = true;
    }
    private void OnDestroy()
    {
        Events.OnBombExplode -= CalculateExplosionHit;
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
    void CalculateExplosionHit(Vector3 explosionPos, float force, float range)
    {
        if (Vector3.Distance(transform.position, explosionPos) > range)
        {
            print("Returning");
            return;
        }
        Vector3 forcePoint = _col.ClosestPointOnBounds(explosionPos);

        Vector3 forceVector = transform.position - forcePoint;

        float x = (forceVector.x < 0 ? -1 : 1);
        float z = (forceVector.z < 0 ? -1 : 1);
        _rb.AddForce(new Vector3(x, 0, z) * force, ForceMode.Force);

    }
}
[System.Serializable]
public enum EnemyTargetType { Player, Anthill };
