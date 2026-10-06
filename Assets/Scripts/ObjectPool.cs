using UnityEngine;
using System.Collections.Generic;

namespace Midterm
{
    public class ObjectPool : MonoBehaviour
    {
        public static ObjectPool Instance;

        // Two object pools for rockets and bullets, one for keycards
        public List<PooledObject> rocketPool = new List<PooledObject>();
        public List<PooledObject> bulletPool = new List<PooledObject>();
        public List<PooledObject> keyCardPool = new List<PooledObject>();

        // Used pools
        public List<PooledObject> usedRocketPool = new List<PooledObject>();
        public List<PooledObject> usedBulletPool = new List<PooledObject>();
        public List<PooledObject> usedKeyCardPool = new List<PooledObject>();

        // Object Pool Count
        [SerializeField] int numberOfObjects = 5;

        // Objects to create
        public GameObject rocketPrefab;
        public GameObject bulletPrefab;
        public GameObject keyCardPrefab;

        void Awake()
        {
            // Singleton Pattern
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            // Initialize our object pools
            Initialize();
        }

        public void Initialize()
        {
            // Create X rockets and bullets and add them to their pools
            for (int i = 0; i < numberOfObjects; i++)
            {
                AddNewObject(rocketPrefab, rocketPool);
                AddNewObject(bulletPrefab, bulletPool);
            }
        }

        public void AddNewObject(GameObject prefab, List<PooledObject> pool)
        {
            // Create new object
            GameObject newObject = Instantiate(
                prefab,
                transform.position,
                Quaternion.identity
            );

            // Set object pool reference in the pooled object
            newObject.GetComponent<PooledObject>().SetObjectPool(this);

            // Hide object on creation
            newObject.SetActive(false);

            // Add object to appropriate pool
            pool.Add(newObject.GetComponent<PooledObject>());
        }

        // KEYCARDS
        // Register an existing keycard pickup into the pool
        // Adds an existing keycard pickup to the keycard pool
        public void AddKeyCardToPool(PooledObject keyCard)
        {
            if (keyCard == null)
                return;

            // Don't add the same keycard twice
            if (keyCardPool.Contains(keyCard) ||
                usedKeyCardPool.Contains(keyCard))
            {
                return;
            }

            // Give keycard a reference to this pool
            keyCard.SetObjectPool(this);

            // Hide the keycard
            keyCard.gameObject.SetActive(false);

            // Add it to available keycards
            keyCardPool.Add(keyCard);

            Debug.Log("Keycard added to object pool.");
        }

        public void UseKeyCard()
        {
            if (keyCardPool.Count > 0)
            {
                PooledObject keyCard = keyCardPool[0];

                keyCardPool.Remove(keyCard);
                usedKeyCardPool.Add(keyCard);

                Debug.Log("Keycard moved from available pool to used pool.");
            }
            else
            {
                Debug.LogWarning("No keycard found in the available pool.");
            }
        }

        // ROCKETS AND BULLETS
        public PooledObject GetPooledObject(bool isRocket)
        {
            List<PooledObject> availablePool;
            List<PooledObject> usedPool;

            // Choose which pool to use
            if (isRocket)
            {
                availablePool = rocketPool;
                usedPool = usedRocketPool;
            }
            else
            {
                availablePool = bulletPool;
                usedPool = usedBulletPool;
            }

            // Check if there are available objects
            if (availablePool.Count > 0)
            {
                // Grab first available object
                PooledObject pooledObject = availablePool[0];

                // Remove from available pool
                availablePool.RemoveAt(0);

                // Add to used pool
                usedPool.Add(pooledObject);

                // Activate object
                pooledObject.gameObject.SetActive(true);

                return pooledObject;
            }
            else
            {
                Debug.Log("No pooled object available");
                return null;
            }
        }

        public void RestoreObject(PooledObject pooledObject)
        {
            if (pooledObject == null) return;

            // Determine which pool this object belongs to
            // If Rocket

            if (usedRocketPool.Contains(pooledObject))
            {
                usedRocketPool.Remove(pooledObject);
                rocketPool.Add(pooledObject);
            }

            // If Bullet
            else if (usedBulletPool.Contains(pooledObject))
            {
                usedBulletPool.Remove(pooledObject);
                bulletPool.Add(pooledObject);
            }

            // If KeyCard
            else if (usedKeyCardPool.Contains(pooledObject))
            {
                usedKeyCardPool.Remove(pooledObject);
                keyCardPool.Add(pooledObject);
            }

            // Hide object
            pooledObject.gameObject.SetActive(false);
        }
    }
}