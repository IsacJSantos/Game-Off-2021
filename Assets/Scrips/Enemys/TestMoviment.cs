using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestMoviment : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform destination;
    // Start is called before the first frame update
    void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.SetDestination(destination.position);
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = destination.position;
        //transform.LookAt(agent.steeringTarget, Vector3.up);
       // transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z);
    }
}
