using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Midterm
{
    public class DoorTrigger : MonoBehaviour
    {

        [SerializeField] string openTag = "Player";
        [SerializeField] Animator animator;
        float delayTime = 3f;



        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(openTag))
            {
                animator.SetBool("DoorOpen", true);
            }
        }

        public void OpenDoor()
        {
            animator.SetBool("DoorOpen", true);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(openTag))
            {
                StartCoroutine(TimeDelay());
            }
        }

        IEnumerator TimeDelay()
        {
            yield return new WaitForSeconds(delayTime);
            animator.SetBool("DoorOpen", false);
        }

    }
}
