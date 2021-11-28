using System.Collections;
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
    [SerializeField] public GameObject takeDamageParticle;
    [SerializeField] public GameObject dieParticle;
    [SerializeField] AudioSource takeTamageSound;
    [SerializeField] AudioSource dieSound;


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
        if (maxLife <= 0) return;

        maxLife -= value;

        if (maxLife <= 0)
        {
            StartCoroutine(Die());
        }

        //particle
        if (takeDamageParticle)
        {
            PlayTakeDamageParticle();
        }

        //sound
        if (takeTamageSound)
        {
            takeTamageSound.PlayOneShot(takeTamageSound.clip, 1.0f);
        }

    }
    void Move()
    {
        enemyMovement.target = agentTarget.GetClosestPoint(transform.position);
    }

    public virtual IEnumerator Die() 
    {  
        enemyMovement.StopEnemy();

        //particle
        if (dieParticle)
        {
            PlayDieParticle();
        }

        //sound
        if (dieSound.clip)
        {
            dieSound.PlayOneShot(dieSound.clip, 1.0f);
        }

        yield return new WaitForSeconds(0.7f);
        Events.OnEnemyDie?.Invoke();
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
    void CalculateExplosionHit(Vector3 explosionPos, float force, float range, float damage)
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
        Hit(damage);
    }

    //configurar particulas
    //  colocar o prefab da particula como objeto filho e alocar pelo inspetor
    //  posicionar particula pela hierarquia
    public void PlayTakeDamageParticle()
    {
        ParticleSystem particle = takeDamageParticle.GetComponent<ParticleSystem>();
        particle.Play();
    }

    void PlayDieParticle()
    {
        ParticleSystem particle = dieParticle.GetComponent<ParticleSystem>();
        particle.Play();
    }
}
[System.Serializable]
public enum EnemyTargetType { Player, Anthill };
