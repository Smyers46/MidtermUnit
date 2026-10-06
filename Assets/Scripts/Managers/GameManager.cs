using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace Midterm
{
    public class GameManager : MonoBehaviour
    {
        public enum GameState
        {
            Paused,
            GameIntro,
            GameStart,
            GamePlaying,
            GameOver,
            GameEnd,
            LevelStart,
            LevelEnd,
        }

        public GameState currentGameState = GameState.GameIntro;

        // Singleton Pattern
        public static GameManager instance = null;

        [Header("UI References")]
        [SerializeField] private UIManager _uiManager;

        // Game Over Sequencing
        [SerializeField] private PlayableDirector gameOverDirector;
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private GameObject gameOverCamera;

        // Victory Sequencing
        [SerializeField] private GameObject victoryScreen;

        [Header("Player References")]
        [SerializeField] private PlayerMovementBehaviour playerMovement;

        // Subscription to Player Health
        public HealthScript playerHealth;

        // Reference to Camera
        public Camera playerCamera = null;

        [Header("Items")]
        public bool hasKeyCard = false;

        [Header("Level Manager Criteria")]
        public LevelManager currentLevel;
        public LevelManager level0;
        [SerializeField] private PlayableDirector openingCutscene;
       
        private void Awake()
        {
            if(instance != null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
            }

            // Assigning Player Camera on Runtime
                playerCamera.transform.localPosition = Vector3.zero;
            }

        private void Start()
        {
            victoryScreen.SetActive(false);

            currentGameState = GameState.GameIntro;

            openingCutscene.stopped += OnOpeningCutsceneFinished;

            StartGameIntro();
        }

        private void StartGameIntro()
        {
            currentGameState = GameState.GameIntro;

            openingCutscene.Play();
        }

        private void OnOpeningCutsceneFinished(PlayableDirector director)
        {
            ChangeState(GameState.GameStart);

            // Start normal gameplay
        }

        public void Update()
        {
            if (PlayerInput.Instance.escapePressed &&
                (currentGameState == GameState.GamePlaying ||
                 currentGameState == GameState.Paused))
            {
                if (currentGameState == GameState.Paused)
                {
                    ChangeState(GameState.GamePlaying);
                    AudioManager.Instance.PlaySFX(AudioManager.Instance.pause);
                    _uiManager.pausePanel.SetActive(false);
                }
                else if (currentGameState == GameState.GamePlaying)
                {
                    ChangeState(GameState.Paused);
                    AudioManager.Instance.PlaySFX(AudioManager.Instance.pause);
                    _uiManager.pausePanel.SetActive(true);
                }
            }
        }

        public void LevelStartCinematicStarted()
        {
            Debug.Log("Cinematic Started");
            ChangeState(GameManager.GameState.LevelStart);
            AudioManager.Instance.PauseMusic();
        }

        public void LevelStartCinematicEnded()
        {
            playerCamera.transform.localPosition = Vector3.zero;
            Debug.Log("Cinematic Ended");
            ChangeState(GameManager.GameState.GamePlaying);
            _uiManager.pausePanel.SetActive(false);
            _uiManager.gameOverPanel.SetActive(false);
            AudioManager.Instance.ResumeMusic();
           
        }

        public void LevelEndCinematicStarted()
        {
            Debug.Log("Cinematic Started");
            ChangeState(GameManager.GameState.LevelEnd);
            AudioManager.Instance.StopMusic();
        }

        public void LevelEndCinematicEnded()
        {
            playerCamera.transform.localPosition = Vector3.zero;
            Debug.Log("Cinematic Ended");
            ChangeState(GameManager.GameState.GamePlaying);

        }

        public void ChangeState(GameState newState)
        {
            currentGameState = newState;

            switch (newState)
            {
                case GameState.GameIntro:
                    OnGameIntro();
                    break;
                case GameState.GameStart:
                    OnGameStart();
                    break;
                case GameState.GameOver:
                    OnGameOver();
                    break;
                case GameState.GameEnd:
                    OnGameEnd();
                    break;
                case GameState.LevelStart:
                    OnLevelStart();
                    break;
                case GameState.LevelEnd:
                    OnLevelEnd();
                    break;
                case GameState.Paused:
                    OnGamePaused();
                    break;
                case GameState.GamePlaying:
                    OnGamePlaying();
                    break;
            }
        }

        private void OnGameStart() // Starting a new scene
        {
            // Do whatever we need to do when game starts
            Debug.Log("Game Start State");

            // Initialize whatever i need to initialize for the game to be playable
            ResetCanvas();

            // Once done, begin playing
            ChangeState(GameState.GamePlaying);

            //playerCamera.enabled(true);
            //cinematicCamera.enabled(false);
        }

        public void OnGameEnd() // Handles logic pertaining to the overarching game experience: Score, won / lost, next level, next scene
        {
            Debug.Log("Game End State");

            // Stop player controls
            playerMovement.enabled = false;

            // Show victory screen
            victoryScreen.SetActive(true);

            playerCamera.transform.localPosition = Vector3.zero;

            // Pausing Music
            AudioManager.Instance.PauseMusic();

            // Make sure the game is running
            Time.timeScale = 0f;

            // Unlock mouse
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void OnGameOver() // Handles logic specific to Player dies / loses
        {
            Debug.Log("Game Over State");

            // Stop player controls
            playerMovement.enabled = false;

            // Hide Game Over UI until the cutscene finishes
            gameOverPanel.SetActive(false);

            // Make sure the Timeline can play while the game is paused
            gameOverDirector.timeUpdateMode = DirectorUpdateMode.UnscaledGameTime;

            // Play the Game Over cutscene
            gameOverDirector.Play();

            StartCoroutine(ShowGameOverPanel());
        }
        private void OnGamePaused()
        {
            Debug.Log("Game Paused State");

            Time.timeScale = 0f; // Pausing the game

            playerCamera.transform.localPosition = Vector3.zero;

            // Pausing Music
            AudioManager.Instance.PauseMusic();

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        private void OnGamePlaying()
        {
            Debug.Log("Game Playing State");

            Time.timeScale = 1f; // Unpausing the game

            playerCamera.transform.localPosition = Vector3.zero;
            gameOverCamera.SetActive(false);

            // Resume Music
            AudioManager.Instance.ResumeMusic();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        public void OnLevelStart() // Start Cinematics / Instructions / Tutorial
        {
            Debug.Log("Level Start State");

            // Play Cinematic

            currentLevel.LevelStart();
            gameOverCamera.SetActive(false);

            // StartCoroutine(ChangeStateDelay(GameState.GamePlaying, 2f));

        }
        public void OnLevelEnd() // Finish a level / hitting a cutscene
        {
            Debug.Log("Level End State");
            currentLevel.LevelEnd();
        }
        private void OnGameIntro()
        {
            Debug.Log("Game Intro State");
            gameOverCamera.SetActive(false);
            ResetCanvas();

            playerMovement.enabled = false;
        }

        // Menu Functionality
        public void RestartLevel()
        {
            Debug.Log("Restarting Current Level");

            ResetCanvas();

            if (currentLevel != null)
            {
                currentLevel.ResetLevel();
                ChangeState(GameState.LevelStart);
            }
            else
            {
                Debug.LogWarning("No Current Level Assigned!");
            }

        }

        public void RestartGame()
        {
            Debug.Log("Restarting Game");

            ResetCanvas();

            //Resetting currentLevel to Level 0
            currentLevel = level0;

            currentLevel.ResetLevel();

            ChangeState(GameState.GameIntro);

            openingCutscene.Play();
        }

        public void ResetCanvas()
        {
            Time.timeScale = 1f;

            hasKeyCard = false;

            playerMovement.enabled = true;

            // Reset Game Over Criteria
            gameOverPanel.SetActive(false);
            gameOverCamera.SetActive(false);

            // Disable pause menu
            _uiManager.pausePanel.SetActive(false);
            
            // Reset Player Health
            playerHealth.currentHealth = 100;
            _uiManager.UpdateHealthDisplay(100);
        }

        private IEnumerator ShowGameOverPanel()
        {
            while (gameOverDirector.state == PlayState.Playing)
            {
                yield return null;
            }

            gameOverPanel.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Time.timeScale = 0f;
        }

    }
}
