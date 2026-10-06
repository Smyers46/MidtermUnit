using UnityEngine;

namespace Midterm
{
    public class KeyCard : MonoBehaviour
    {
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private GameManager _gameManager;

        private bool collected = false;

        private PooledObject pooledObject;

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

        private void OnTriggerEnter(Collider other)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.collectItem);

            if (collected)
                return;

            if (!other.CompareTag("Player"))
                return;

            if (_uiManager == null)
            {
                Debug.LogError("UIManager reference is missing!");
                return;
            }


            if (ObjectPool.Instance == null)
            {
                Debug.LogError("KeyCard: ObjectPool.Instance is missing!");
                return;
            }

            pooledObject = GetComponent<PooledObject>();

            if (pooledObject == null)
            {
                Debug.LogError("KeyCard: PooledObject component is missing!");
                return;
            }

            // Add keycard to player's inventory / UI
            _uiManager.ShowKeyCard();

            // Mark as collected
            collected = true;
            GameManager.instance.hasKeyCard = true;

            // Add physical keycard to object pool
            ObjectPool.Instance.AddKeyCardToPool(pooledObject);

            Debug.Log("KeyCard successfully collected and added to pool.");
        }

        public void UseKeyCard()
        {
            if (pooledObject == null)
            {
                Debug.LogError("KeyCard: No pooled object available.");
                return;
            }

            if (ObjectPool.Instance == null)
            {
                Debug.LogError("KeyCard: ObjectPool.Instance is missing!");
                return;
            }

            // Move the keycard from the available pool to the used pool
            if (ObjectPool.Instance.keyCardPool.Contains(pooledObject))
            {
                ObjectPool.Instance.keyCardPool.Remove(pooledObject);
                ObjectPool.Instance.usedKeyCardPool.Add(pooledObject);

                Debug.Log("Inventory keycard moved to used pool.");
            }
        }

        public void RestoreKeyCard()
        {
            if (pooledObject == null)
                return;

            ObjectPool.Instance.RestoreObject(pooledObject);
        }

    }
}
