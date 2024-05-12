using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This is the movement controller for NPC which utilize NavMeshAgent
/// </summary>
public class NPCMovementController : MonoBehaviour
{
    public Transform destination;
    public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }

    private void Update()
    {
        agent.destination = destination.position;
    }

}
