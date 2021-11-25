using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public bool doMove;
    public Vector3 target;
    public float TargetDistance { get { return agent.remainingDistance; } }
    public float StopDistance { get { return agent.stoppingDistance; } }

    public bool CanAttack { get { return (Vector3.Distance(transform.position, target) <= 8); } }
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
  
    void Update()
    {
       if (doMove && target != null)
            Move(target);
    
    }

    void Move(Vector3 target)
    {
        agent.destination = target;     
    }

}
