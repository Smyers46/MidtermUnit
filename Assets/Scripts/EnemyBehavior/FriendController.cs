using UnityEngine;
using UnityEngine.AI;

namespace Midterm
{
    public class FriendController : MonoBehaviour
    {
        //Properties
        public float moveSpeed = 3f;
        public float detectionRange = 10f;
        public float stoppingDistance = 3f;

        public GameObject target = null;

        //States
        FriendState currentState = null;

        //NavMeshAgent
        public NavMeshAgent agent;

        // On Death Spawns
        public ParticleSystem deathAnimation;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.speed = moveSpeed;

            if (target != null)
            {
                ChangeState(new FriendFollowState(this));
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (currentState != null)
            {
                currentState.OnStateUpdate();
            }
        }

        public void ChangeState(FriendState newState)
        {
            //Call current state's OnStateExit method before changing state
            if (currentState != null)
            {
                currentState.OnStateExit();
            }

            //Setting the new state
            currentState = newState;

            //Call new state's OnStateEntered method
            currentState.OnStateEntered();

        }

        public void MoveTo(Vector3 destination)
        {
            ChangeState(new FriendMoveState(this, destination));
        }

        public void StartFollowing()
        {
            if (target != null)
            {
                ChangeState(new FriendFollowState(this));
            }
        }

        private void OnDestroy()
        {
            currentState = null;
        }

        public void OnDeathResponse()
        {
            Instantiate(deathAnimation, transform.position + Vector3.up * 3f, Quaternion.identity);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.onDeath);

            Destroy(this.gameObject);
        }

    }
}
