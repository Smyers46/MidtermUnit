using UnityEngine;

namespace Midterm
{
    public class DoorReset : MonoBehaviour
    {
        private Vector3 startingPosition;
        private Quaternion startingRotation;

            private void Awake()
            {
                startingPosition = transform.localPosition;
                startingRotation = transform.localRotation;
            }

            public void ResetDoor()
            {
            Animator animator = GetComponent<Animator>();

            if (animator != null)
            {
                animator.enabled = false;
            }

            transform.localPosition = startingPosition;
            transform.localRotation = startingRotation;

            if (animator != null)
            {
                animator.Rebind();
                animator.Update(0f);
                animator.enabled = true;
            }

            Debug.Log("Door reset.");
        }
        
    }
}
