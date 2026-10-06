using Unity.VisualScripting;
using UnityEngine;

namespace Midterm
{
    public class HintSign : MonoBehaviour, ISelectable
    {
        [TextArea(2, 5)]
        [SerializeField] private string hintMessage;
        private UIManager uiManager;

        private void Awake()
        {
            uiManager = FindAnyObjectByType<UIManager>();
        }

        public void OnSelect()
        {
            // Nothing needed here
        }
        
        public void OnHoverEnter()
        {
            uiManager.ShowHint(hintMessage);
        }

        public void OnHoverExit()
        {
            uiManager.HideHint();
        }

    }
}
