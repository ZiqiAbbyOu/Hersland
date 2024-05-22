using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This is the movement controller for NPC which utilize NavMeshAgent
/// </summary>
public class NPCMovementController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Terrain terrain;
    public float timer;
    public float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        terrain = GameObject.Find("Terrain").GetComponent<Terrain>();
        agent = GetComponent<NavMeshAgent>();
        SetNewDestination();
        timer = waitTime = GetRandomWaitTime();
        
    }

    private void Update()
    {
        //agent.destination = destination.position;
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if(!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                timer -= Time.deltaTime;

                if (timer <= 0)
                {
                    SetNewDestination();
                    timer = waitTime = GetRandomWaitTime();
                }
            }
        }
    }

    private void SetNewDestination()
    {
        Vector3 newDestination = GetRandomPositionOnTerrain();

        //Make sure it is in the map. Else reset it.
        NavMeshHit hit;

        if (NavMesh.SamplePosition(newDestination, out hit, 1.0f, NavMesh.AllAreas) )
        {
            agent.SetDestination(hit.position);
        }
        else
        {
            SetNewDestination();
        }

    }

    private Vector3 GetRandomPositionOnTerrain()
    {
        float terrainWidth = terrain.terrainData.size.x;
        float terrainLength = terrain.terrainData.size.z;

        float randomX = Random.Range(0,terrainWidth);
        float randomZ = Random.Range(0,terrainLength);
        float y = terrain.SampleHeight(new Vector3(randomX,0,randomZ));

        return new Vector3(randomX, y, randomZ);
    }

    private float GetRandomWaitTime()
    {
        return Random.Range(0f, 20f);
    }

}
