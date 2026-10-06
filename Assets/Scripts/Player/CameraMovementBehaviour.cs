using Unity.VisualScripting;
using UnityEngine;

namespace Midterm
{

    [RequireComponent (typeof (Camera))]
    public class CameraMovementBehaviour : MonoBehaviour
    {

        [SerializeField] private PlayerInput _input;
        [Header("Player Turn")]
        [SerializeField] private float _turnSpeed;
        [SerializeField] private bool _invertedMouse;

        private float _camXRotation;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            _camXRotation = transform.localEulerAngles.x;

            if (_camXRotation > 180f)
                _camXRotation -= 360f;
        }

        void Update()
        {
            RotateCamera();
        }

        void RotateCamera()
        {
            _camXRotation += Time.deltaTime * _input.mouseY * _turnSpeed * (_invertedMouse ? 1 : -1);
            _camXRotation = Mathf.Clamp(_camXRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(_camXRotation, 0, 0);
        }

    }
}
