using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    public bool doMove;
    public Vector3 target;

    [SerializeField] NavMeshAgent agent;
    [SerializeField] bool _isStopped;
    [SerializeField] float attackDistance;
    public float TargetDistance { get { return agent.remainingDistance; } }
    public float StopDistance { get { return agent.stoppingDistance; } }

    public bool CanAttack
    {
        get
        {
            if (_isStopped) return false;

            return (Vector3.Distance(transform.position, target) <= attackDistance);
        }
    }
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void StopEnemy() 
    {
        agent.isStopped = true;
        _isStopped = true;
        agent.speed = 0;
    }
    void Update()
    {
        if (_isStopped) return;

        if (doMove && target != null)
            Move(target);

    }

    void Move(Vector3 target)
    {
        agent.destination = target;
    }

}
