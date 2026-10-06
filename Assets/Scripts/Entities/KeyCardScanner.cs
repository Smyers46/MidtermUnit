using UnityEngine;
using UnityEngine.Playables;

namespace Midterm
{
    public class KeyCardScanner : MonoBehaviour, ISelectable
    {
        [SerializeField] private KeyCardBox keyCardBox;
        [SerializeField] private PlayableDirector director;
        [SerializeField] private UIManager _uiManager;

        private void Start()
        {
            _uiManager = FindAnyObjectByType<UIManager>();

            if (_uiManager == null)
            {
                Debug.LogError("KeyCard: UIManager not found in scene!");
            }
        }
        public void OnSelect()
        {
            // Don't do anything if the KeyCardBox isn't open
            if (!keyCardBox.isOpen)
            {
                Debug.Log("Keycard scanner is locked.");
                return;
            }

            if (GameManager.instance.hasKeyCard)
            {
                // Moving physical keycard from the inventory pool to used pool
                ObjectPool.Instance.UseKeyCard();

                //Remove the keycard from the player's UI Inventory
                _uiManager.UseKeyCard();

                // Play Cutscene
                director.Play();
            }
            else return;
        }

        public void OnHoverEnter()
        {
            if (!keyCardBox.isOpen)
            {
                Debug.Log("Keycard scanner is locked");
                return;
            }

            if (GameManager.instance.hasKeyCard)
            {
                _uiManager.ShowPrompt();
            }
            else return;
        }

        public void OnHoverExit()
        {
            // Optional hover behavior
        }
    }
}
