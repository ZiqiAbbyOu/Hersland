using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace HL.Character.GOAP {
    [RequireComponent(requiredComponent: typeof(NavMeshAgent), requiredComponent2: typeof(Animator), requiredComponent3: typeof(AgentBehaviour))]
    public class AgentMoveBehavior : MonoBehaviour
    {
        private NavMeshAgent agent;
        private Animator animator;
        private AgentBehaviour agentBehaviour;
        private ITarget currentTarget;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            agentBehaviour = GetComponent<AgentBehaviour>();
        }

        private void OnEnable()
        {
            agentBehaviour.Events.OnTargetInRange += EventsOnTargetInRange;
            agentBehaviour.Events.OnTargetChanged += EventsOnTargetChanged;
            agentBehaviour.Events.OnTargetOutOfRange += EventsOnTargetOutOfRange;
        }

        private void OnDisable()
        {
            agentBehaviour.Events.OnTargetInRange -= EventsOnTargetInRange;
            agentBehaviour.Events.OnTargetChanged -= EventsOnTargetChanged;
            agentBehaviour.Events.OnTargetOutOfRange -= EventsOnTargetOutOfRange;

        }

        private void EventsOnTargetInRange(ITarget target)
        {
            currentTarget = target;

        }

        private void EventsOnTargetOutOfRange(ITarget target)
        {

        }

        private void EventsOnTargetChanged(ITarget target, bool inRange) 
        {

        }
    }
}