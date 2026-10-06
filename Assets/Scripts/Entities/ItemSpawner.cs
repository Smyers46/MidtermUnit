using UnityEngine;

namespace Midterm
{
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject keyCardPrefab;

        [Header("Spawn Position Offset")]
        [SerializeField] private Vector3 positionOffset = new Vector3(0f, 0.5f, 0f);

        [Header("Spawn Rotation Offset")]
        [SerializeField] private Vector3 rotationOffset = new Vector3(0f, 90f, 0f);

        public void SpawnKeyCard(Transform transform)
        {
            // Spawn relative to the game object's position and orientation
            Vector3 spawnPosition = transform.TransformPoint(positionOffset);

            // Apply a rotation offset relative to the game object
            Quaternion spawnRotation = transform.rotation * Quaternion.Euler(rotationOffset);

            Instantiate(keyCardPrefab, spawnPosition, spawnRotation);
        }
        
    }
}
