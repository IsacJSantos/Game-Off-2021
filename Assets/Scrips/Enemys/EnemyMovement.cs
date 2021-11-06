using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public bool doMove;
    public Transform target;

    public float TargetDistance { get { return agent.remainingDistance; } }
    public float StopDistance { get { return agent.stoppingDistance; } }
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
  
    void Update()
    {
        if (doMove && target != null)
            Move(target.position);
    }

    void Move(Vector3 target)
    {
        agent.SetDestination(target);
    }

}
