using UnityEngine;

namespace Midterm
{
    public class HealthPickup : MonoBehaviour
    {
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private GameManager _gameManager;
        public HealthScript playerHealth;

        private void Awake()
        {
            _uiManager = FindAnyObjectByType<UIManager>();
            Debug.Log($"KeyCard found UIManager: {_uiManager.gameObject.name}");

            if (_uiManager == null)
            {
                Debug.LogError("KeyCard: UIManager not found in scene!");
            }

            _gameManager = FindAnyObjectByType<GameManager>();

            if (_gameManager == null)
            {
                Debug.LogError("KeyCard: GameManager not found in scene!");
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Trigger Entered");
                CollectHealth();
                Destroy(gameObject);
            }
        }

        private void CollectHealth()
        {
            Debug.Log("Collect Health Called");
            AudioManager.Instance.PlaySFX(AudioManager.Instance.healthPickup);
            playerHealth.currentHealth = 100;
            _uiManager.UpdateHealthDisplay(100);

        }

    }
}
