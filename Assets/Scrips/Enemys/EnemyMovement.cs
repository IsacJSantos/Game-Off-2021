using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public bool doMove;
    public Vector3 target;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    // Update is called once per frame
    void Update()
    {
        if (doMove && target != null)
            Move(target);
    }

    void Move(Vector3 target)
    {
        agent.SetDestination(target);
    }

}
