using UnityEngine;
using UnityEngine.UIElements;

namespace Midterm
{
    public class PickupCube : MonoBehaviour, IPickable
    {
        FixedJoint _joint;
        Rigidbody _cubeRb;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _cubeRb = GetComponent<Rigidbody>();
        }

        public void OnPicked(Transform attachPoint)
        {
            transform.position = attachPoint.position;
            transform.rotation = attachPoint.rotation;
            transform.SetParent(attachPoint);

            _cubeRb.isKinematic = true;
            _cubeRb.useGravity = false;
        }

        public void OnDropped()
        {
            Destroy(_joint);
            _cubeRb.isKinematic = false;
            _cubeRb.useGravity = true;
            transform.SetParent(null); // detaches from player
        }

    }
}
