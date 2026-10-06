using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

namespace Midterm
{
    public class UIManager : MonoBehaviour
    {
        //Health text and game over panel
        [Header("Player Heads Up Display")]
        public TMP_Text healthDisplay;
        public HealthScript playerHealth;

        [Header("Hints")]
        public GameObject hintPanel;
        public TMP_Text hintText;

        [Header("Collectibles")]
        public GameObject keyCardIcon;
        public TMP_Text keyCardCollectedText;
        public bool hasKeyCard = false;
        public TMP_Text keyCardPrompt;
        public TMP_Text keyCardLocked;
        public GameObject robotFriendControls;

        [Header("Game Over")]
        public GameObject gameOverPanel;

        [Header("Pause Menu")]
        public GameObject pausePanel;

        private void Awake()
        {
            // Subscribed to the HealthScripts OnHealthChange and OnDeath events
            playerHealth.OnHealthChanged += UpdateHealthDisplay;
            playerHealth.OnDeath += ShowGameOver;
        }

        public void UpdateHealthDisplay(float currentHealth)
        {
            healthDisplay.text = "Health: " + currentHealth.ToString("F0");
        }

        public void ShowHint(string message)
        {
            hintText.text = message;
            hintPanel.SetActive(true);
        }

        public void HideHint()
        {
            hintPanel.SetActive(false);
        }

        public IEnumerator ShowKeyCardMessage()
        {
            keyCardCollectedText.gameObject.SetActive(true);

            yield return new WaitForSeconds(3f);

            keyCardCollectedText.gameObject.SetActive(false);
        }

        public IEnumerator ShowKeyCardPrompt()
        {
            keyCardPrompt.gameObject.SetActive(true);

            yield return new WaitForSeconds(3f);

            keyCardPrompt.gameObject.SetActive(false);
        }

        public void ShowKeyCard()
        {
            hasKeyCard = true;
            if (keyCardIcon == null)
            {
                Debug.LogError("UIManager: Key Card Icon has not been assigned");
                return;
            }
            
            keyCardIcon.SetActive(true);
            
            StartCoroutine(ShowKeyCardMessage());
        }

        public void ShowPrompt()
        {
            StartCoroutine(ShowKeyCardPrompt());
        }

        public void UseKeyCard()
        {
            hasKeyCard = false;
            keyCardIcon.SetActive(false);
            GameManager.instance.hasKeyCard = false;
        }

        public void ShowRobotMessage()
        {
            robotFriendControls.SetActive(true);
        }

        public void HideRobotMessage()
        {
            robotFriendControls.SetActive(false);
        }

        public void ShowGameOver()
        {
            gameOverPanel.SetActive(true);
        }
    }
}
