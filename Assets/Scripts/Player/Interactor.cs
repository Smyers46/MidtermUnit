using UnityEngine;

namespace Midterm
{
    public abstract class Interactor : MonoBehaviour
    {
        
        //[SerializeField] protected PlayerInput _input;

        private void Update()
        {
            Interact();
        }

        public abstract void Interact();

    }
}
