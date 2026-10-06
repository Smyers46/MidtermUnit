using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Midterm
{
    public class LevelManager : MonoBehaviour
    {
        [Header("Player")]
        [SerializeField] private Transform playerSpawnTransform;
        [SerializeField] private Transform player;

        [Header("Enemies")]
        [SerializeField] private EnemyController[] enemies;
        [SerializeField] private BossEnemyController boss;

        [Header("Events")]
        [SerializeField] UnityEvent OnLevelStart;
        [SerializeField] UnityEvent OnLevelEnd;
        [SerializeField] UnityEvent OnLevelReset;

        [Header("Other")]
        [SerializeField] private AudioClip levelMusic;
        public int currentLevel;

        //private bool isFinalLevel = false;

        public void LevelStart()
        {
            OnLevelStart?.Invoke();
            Debug.Log("Level Start");
            AudioManager.Instance.PlayMusic(levelMusic);
        }

        public void LevelEnd()
        {
            OnLevelEnd?.Invoke();
            Debug.Log("Level End");
            AudioManager.Instance.StopMusic();
        }

        public void ResetLevel()
        {
            ResetPlayer();
            ResetEnemies();
            ResetBoss();

            OnLevelReset?.Invoke();

            // Reset anything else connected in inspector
            Debug.Log("Level Reset");
        }

        private void ResetPlayer()
        {
            if (player == null || playerSpawnTransform == null)
            {
                Debug.LogWarning("Player or Player Spawn is not assigned.");
                return;
            }

            CharacterController controller = player.GetComponent<CharacterController>();

            if (controller != null)
                controller.enabled = false;

            player.position = playerSpawnTransform.position;
            player.rotation = playerSpawnTransform.rotation;

            if (controller != null)
                controller.enabled = true;
        }

        private void ResetEnemies()
        {
            foreach (EnemyController enemy in enemies)
            {
                if (enemy != null)
                    enemy.ResetEnemy();
            }
        }

        private void ResetBoss()
        {
            if (boss != null)
                boss.ResetBoss();
        }

    }
}
