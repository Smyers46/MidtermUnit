using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

namespace Midterm
{
    public class BossEnemyController : MonoBehaviour
    {
        //Enemy Properties
        public float moveSpeed = 1f;
        public float attackRange = 15f;
        public float detectionRange = 20f;
        public bool canAttack = true;

        public GameObject target = null;

        //Resetting Position
        private Vector3 startingPosition;
        private Quaternion startingRotation;

        //Combat
        public BossLaserAttack laserAttack = null;
        public float attackCooldown = 5f;

        //Boss States
        BossState currentState = null;
        [SerializeField] private NavMeshAgent agent;

        public HealthScript healthScript = null;

        [SerializeField] private PlayableDirector cutscene;

        // On Death Behavior
        public ParticleSystem bossDeathAnimation;

        private void Awake()
        {
            healthScript.OnDeath += OnDeathResponse;

            agent = GetComponent<NavMeshAgent>();

            startingPosition = transform.position;
            startingRotation = transform.rotation;
        }

        private void Start()
        {
            if (cutscene != null)
            {
                cutscene.stopped += OnCutsceneFinished;
            }
        }

        private void OnCutsceneFinished(PlayableDirector director)
        {
            StartBoss();
        }

        public void StartBoss()
        {
            ChangeState(new BossFollowState(this));
        }

        private void Update()
        {
            if (GameManager.instance.currentGameState != GameManager.GameState.GamePlaying) return;

            if (currentState != null)
            {
                currentState.OnStateUpdate();
            }
        }

        public void ChangeState(BossState newState)
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

        public void LaserAttackFinished()
        {
            ChangeState(new BossFollowState(this));
        }

        private void OnDestroy()
        {
            if (cutscene != null)
            {
                cutscene.stopped -= OnCutsceneFinished;
            }
            currentState = null;
        }

        public void OnDeathResponse()
        {

            BossLaserAttack laser = FindAnyObjectByType<BossLaserAttack>();

            if (laser != null)
            {
                laser.ResetLaser();
            }
            
            ParticleSystem explosion = Instantiate(bossDeathAnimation, transform.position + Vector3.up * 4f, Quaternion.identity);

            explosion.Play();

            AudioManager.Instance.PlaySFX(AudioManager.Instance.bossDeath);

            gameObject.SetActive(false);

            GameManager.instance.ChangeState(GameManager.GameState.LevelEnd);
        }

        public void ResetBoss()
        {
            agent.Warp(startingPosition);
            transform.rotation = startingRotation;

            gameObject.SetActive(true);

            healthScript.currentHealth = 2000;

            // Reset whatever state enemy uses
        }

    }
}
