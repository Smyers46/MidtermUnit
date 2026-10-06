using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

namespace Midterm
{
    public class PushButton : MonoBehaviour, ISelectable
    {
        [SerializeField] private Material _default;
        [SerializeField] private Material _hoverColor;
        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private Animator animator;
        [SerializeField] private UIManager _uiManager;

        public UnityEvent _onPush;

        private bool hasPlayed;

        private bool isOpen = false;

        public PlayableDirector director;

        private void Start()
        {
            _renderer = GetComponent<MeshRenderer>();
            _renderer.material = _default;
            hasPlayed = false;
        }

        public void OnHoverEnter()
        {
            _renderer.material = _hoverColor;
            if (!hasPlayed)
            {
                _uiManager.ShowPrompt();
            }
            else return;
        }

        public void OnHoverExit()
        {
            _renderer.material = _default;
        }

        public void OnSelect()
        {
            isOpen = !isOpen;
            _onPush?.Invoke();

        }

        public void ToggleDoor()
        {
            animator.SetBool("DoorOpen", isOpen);
            director.Play();
            hasPlayed = true;
        }

    }
}
