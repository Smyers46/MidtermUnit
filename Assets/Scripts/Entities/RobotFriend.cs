using UnityEngine;

namespace Midterm
{
    public class RobotFriend : MonoBehaviour, ISelectable
    {
        private UIManager _uiManager;
        private bool isSelected;
        
        private void Start()
        {
            _uiManager = FindAnyObjectByType<UIManager>();
        }
        public void OnSelect()
        {
            // Nothing needed here
        }

        public void OnHoverEnter()
        {
            _uiManager.ShowRobotMessage();
            if (!isSelected)
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.gameHelp);
                isSelected = true;
            }
            else return;
            
        }

        public void OnHoverExit()
        {
            _uiManager.HideRobotMessage();
            isSelected = false;
        }
    }
}
