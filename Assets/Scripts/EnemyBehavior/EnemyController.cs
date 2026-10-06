using System.Collections;
using UnityEngine;

namespace Midterm
{
    public class EnemyController : MonoBehaviour
    {
        //Enemy Properties
        public float moveSpeed = 3f;
        public float attackRange = 3f;
        public float detectionRange = 10f;
        public bool canAttack = true;

        public GameObject target = null;

        //Resetting Position
        private Vector3 startingPosition;
        private Quaternion startingRotation;

        //Combat
        public GameObject bulletPrefab = null;
        public GameObject bulletSpawnReference = null;
        public float bulletSpeed = 5f;
        public float attackCooldown = 1f;

        //Enemy States
        EnemyState currentState = null;

        public HealthScript healthScript = null;

        // On Death Spawns
        public ItemSpawner itemSpawner;
        public ParticleSystem deathAnimation;

        private void Awake()
        {
            healthScript.OnDeath += OnDeathResponse;
            startingPosition = transform.position;
            startingRotation = transform.rotation;
        }

        private void Start()
        {
            ChangeState(new EnemyIdleState(this));
        }

        private void Update()
        {
            if (GameManager.instance.currentGameState != GameManager.GameState.GamePlaying) return;
            
            if (currentState != null)
            {
                currentState.OnStateUpdate();
            }
        }

        public void ChangeState(EnemyState newState)
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
        private void OnDestroy()
        {
            currentState = null;
        }

        public void OnDeathResponse()
        {
            Instantiate(deathAnimation, transform.position + Vector3.up * 3f, Quaternion.identity);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.onDeath);

            if (itemSpawner != null)
            {
                itemSpawner.SpawnKeyCard(transform);
                
            }

            gameObject.SetActive(false);
        }

        public void ResetEnemy()
        {
            transform.position = startingPosition;
            transform.rotation = startingRotation;

            gameObject.SetActive(true);

            // Reset whatever state enemy uses
        }

    }
}
