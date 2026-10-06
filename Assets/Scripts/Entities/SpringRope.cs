using UnityEngine;

namespace Midterm
{
    [RequireComponent(typeof(SpringJoint))]
    [RequireComponent(typeof(LineRenderer))]
    public class SpringRope : MonoBehaviour
    {
        private SpringJoint springJoint;
        private LineRenderer lineRenderer;
        private bool ropeBroken = false;

        private void Awake()
        {
            springJoint = GetComponent<SpringJoint>();
            lineRenderer = GetComponent<LineRenderer>();

            lineRenderer.positionCount = 2;
            lineRenderer.useWorldSpace = true;
        }

        private void LateUpdate()
        {
            // If the joint has been broken or destroyed,
            // play the sound once and hide the rope.
            if (springJoint == null)
            {
                if (!ropeBroken)
                {
                    ropeBroken = true;

                    AudioManager.Instance.PlaySFX(AudioManager.Instance.ropeBroken);
                    lineRenderer.enabled = false;
                }

                return;
            }

            if (springJoint.connectedBody == null)
            {
                lineRenderer.enabled = false;
                return;
            }

            // Ceiling anchor
            Vector3 startPoint = transform.position;

            // Pressure cube
            Vector3 endPoint =
                springJoint.connectedBody.transform.position;

            lineRenderer.SetPosition(0, startPoint);
            lineRenderer.SetPosition(1, endPoint);
        }
    }
}
