using UnityEngine;
using UnityEngine.AI;

public class TestMoviment : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform destination;
    
    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(destination.position);    
    }

    
}
