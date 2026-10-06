using UnityEngine;

namespace Midterm
{
    public class BossDoor : MonoBehaviour
    {
        [SerializeField] string openTag = "Player";
        [SerializeField] Animator animator;
        private bool isTriggered;

        public void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag(openTag) && !isTriggered)
            {
                isTriggered = true;
                animator.SetBool("DoorOpen", true);
                AudioManager.Instance.PlaySFX(AudioManager.Instance.doorOpen);
            }
        }
    }
}
