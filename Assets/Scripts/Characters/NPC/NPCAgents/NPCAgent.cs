using CrashKonijn.Goap.Interfaces;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using UnityEngine.AI;


namespace HL.Character
{
    public class NPCAgent : Agent
    {
        public NavMeshAgent navMeshAgent;
        private Vector3 initialPosition;
        public PersonalityPropertyInfo personality;
        public bool trainingMode;

        public override void Initialize()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            initialPosition = transform.position;
            personality = GetComponent<PersonalityPropertyInfo>();
            if (!trainingMode) MaxStep = 0;
        }

        /// <summary>
        /// Reset the agent when an episode begins
        /// </summary>
        public override void OnEpisodeBegin()
        {
            navMeshAgent.Warp(initialPosition);
            SetRandomDestination();
        }

        public override void CollectObservations(VectorSensor sensor)
        {
            sensor.AddObservation(personality.GetPropertyStat(WuXingProperty.Huo)); // 1 observation
            sensor.AddObservation(transform.position.x); // 1 float observation
            sensor.AddObservation(transform.position.y); // 1 float observation
            sensor.AddObservation(transform.position.z); // 1 float observation
        }

        /// <summary>
        /// Index 0: moveProbability
        /// </summary>
        /// <param name="actions"></param>
        public override void OnActionReceived(ActionBuffers actions)
        {
            float moveProbability = Mathf.Clamp(actions.ContinuousActions[0], 0f, 1f);
            float huoPersonality = personality.GetPropertyStat(WuXingProperty.Huo);

            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    // Adjust moveProbability by huoPersonality
                    // Ensure huoPersonality from -1 to 1 is mapped to 0 to 1 range
                    float adjustedMoveProbability = moveProbability * (huoPersonality + 1) / 2;
                    bool willMove = Random.Range(0f, 1f) < adjustedMoveProbability;
                    bool shouldMove = huoPersonality > 0f;

                    if (willMove)
                    {
                        SetRandomDestination();
                        Debug.Log("Moving to new destination.");
                    }
                    else
                    {
                        StopMoving();
                        StartCoroutine(WaitForSeconds(1));
                        Debug.Log("Staying in place.");
                    }

                    // Provide clearer and more consistent reward signals
                    if ((willMove && shouldMove) || (!willMove && !shouldMove))
                    {
                        AddReward(1.0f);
                        Debug.Log("Reward for matching behavior with Huo property.");
                    }
                    else
                    {
                        AddReward(-1.0f);
                        Debug.Log("Penalty for not matching behavior with Huo property.");
                    }
                }
            }

            if (trainingMode)
            {
                personality.SetRandomPropertyStats();
            }
        }


        public override void Heuristic(in ActionBuffers actionsOut)
        {
            var continuousActionsOut = actionsOut.ContinuousActions;
            continuousActionsOut[0] = personality.GetPropertyStat(WuXingProperty.Huo);
        }

        private void SetRandomDestination()
        {
            Vector3 randomDirection = Random.insideUnitSphere * 10f;
            randomDirection += transform.position;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, 10f, NavMesh.AllAreas))
            {
                navMeshAgent.ResetPath();
                navMeshAgent.SetDestination(hit.position);
                navMeshAgent.isStopped = false; 
                navMeshAgent.updateRotation = true;
                navMeshAgent.updateUpAxis = true;
                Debug.Log("Set destination: " + hit.position);
            }
            else
            {
                Debug.Log("Failed to find NavMesh position for: " + randomDirection);
            }
        }

        private void StopMoving()
        {
            navMeshAgent.ResetPath();
            navMeshAgent.isStopped = true;
            navMeshAgent.velocity = Vector3.zero; // Explicitly set velocity to zero to stop any residual movement
        }

        private void Update()
        {
            //Debug.Log("NavMeshAgent Velocity: " + navMeshAgent.velocity);
            //Debug.Log("NavMeshAgent Path Pending: " + navMeshAgent.pathPending);
            //Debug.Log("NavMeshAgent Has Path: " + navMeshAgent.hasPath);
            //Debug.Log("NavMeshAgent Is Path Stale: " + navMeshAgent.isPathStale);
            //Debug.Log("NavMeshAgent Path Status: " + navMeshAgent.pathStatus);
            //Debug.Log("NavMeshAgent Destination: " + navMeshAgent.destination);
            //Debug.Log("NavMeshAgent Remaining Distance: " + navMeshAgent.remainingDistance);
            //Debug.Log("NavMeshAgent Stopping Distance: " + navMeshAgent.stoppingDistance);
        }

        private IEnumerator WaitForSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
        }


    }
}