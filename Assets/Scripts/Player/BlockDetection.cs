using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Playables;

namespace Midterm
{
    public class BlockDetection : MonoBehaviour
    {
        [SerializeField] private PlayableDirector levelEndTimeline;
        public MonoBehaviour door;

        List<Collider> blocks = new List<Collider>();

        private void OnTriggerEnter(Collider other)
        {
            // Regular Pressure Blocks OR the robot friend
            bool isRobot = other.GetComponentInParent<PressurePlateActivator>();


            if (!isRobot)
            {
                blocks.Add(other);
            }

            else
            {
                // Add the robot collider
                if (!blocks.Contains(other))
                {
                    blocks.Add(other);
                }
            }

            // if something is on the pressure plate, unlock the door
            if (blocks.Count > 0)
            {
                if (door is IBlockDetectorTarget target)
                {
                    target.UnlockDoor(this);
                }
                else if (door is KeyCardBox keyCardBox)
                {
                    keyCardBox.UnlockDoor(this);
                }

                AudioManager.Instance.PlaySFX(AudioManager.Instance.lightIndicator);

                // Play Level 3 Ending Timeline
                if (levelEndTimeline != null)
                {
                    levelEndTimeline.Play();
                }

            }
        }

        private void OnTriggerExit(Collider other)
        {
            blocks.Remove(other);

            if (blocks.Count == 0)
            {
                if (door is IBlockDetectorTarget target)
                {
                    target.LockDoor(this);
                }
                else if (door is KeyCardBox keyCardBox)
                {
                    keyCardBox.LockDoor(this);
                }
            }
        }

    }
}
