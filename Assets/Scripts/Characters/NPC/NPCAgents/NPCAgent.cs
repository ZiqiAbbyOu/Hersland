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
            PersonalityPropertyInfo personality = GetComponent<PersonalityPropertyInfo>();
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
            sensor.AddObservation(personality);
            sensor.AddObservation(transform.position);
        }

        /// <summary>
        /// Index 0: moveProbability
        /// </summary>
        /// <param name="actions"></param>
        public override void OnActionReceived(ActionBuffers actions)
        {
            float moveProbablity = actions.ContinuousActions[0];

            if (Random.Range(0f,1f) < moveProbablity)
            {
                SetRandomDestination();
                AddReward(0.1f);
            }
            else
            {
                AddReward(-0.1f);
            }

            personality.SetRandomPropertyStats();
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
            if (NavMesh.SamplePosition(randomDirection, out hit, 10f, 1))
            {
                navMeshAgent.SetDestination(hit.position);
            }
        }
    }
}