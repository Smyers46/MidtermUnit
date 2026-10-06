using UnityEngine;
using UnityEngine.AI;

namespace Midterm
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Transform _targetPosition;

        private NavMeshAgent _agent;

        void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            _agent.destination = _targetPosition.position;
        }
    }
}
