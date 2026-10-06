using UnityEngine;
using System;

namespace Midterm
{
    public class HealthScript : MonoBehaviour
    {
        // Health Properties
        public float currentHealth = 100f;

        // Two events for our Observer Pattern
        public event Action<float> OnHealthChanged;
        public event Action OnDeath;

        // Boolean for is Player
        [SerializeField] private bool isPlayer;

        void Start()
        {
            bool isSubscribed = (OnHealthChanged != null);
            if (isSubscribed)
            {
                OnHealthChanged.Invoke(currentHealth);
            }
        }

        public void Test(float health)
        {
            Debug.Log(health);
        }

        public void TakeDamage(float amount)
        {

            // Reducing Health
            currentHealth -= amount;

            // Invoke OnHealthChanged event to notify subscribers of the health change
            OnHealthChanged?.Invoke(currentHealth);

            AudioManager.Instance.PlaySFX(AudioManager.Instance.takeDamage);

            //Death
            if (currentHealth <= 0f)
            {
                Die();
            }
        }

        public void Die()
        {

            OnDeath?.Invoke();

            if (isPlayer)
            {
                GameManager.instance.ChangeState(GameManager.GameState.GameOver);
            }
        }

    }
}
