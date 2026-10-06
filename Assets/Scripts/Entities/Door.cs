using System;
using UnityEngine;

namespace Midterm
{
    public class Door : MonoBehaviour, IBlockDetectorTarget
    {
        [SerializeField] BlockDetection[] blockDetectors;
        [SerializeField] Renderer[] unlockLights;

        Animator anim;
        bool[] unlocks;

        private void Awake()
        {

            anim = GetComponent<Animator>();

            foreach (BlockDetection blockDetector in blockDetectors)
            {
                blockDetector.door = this;
            }

            foreach (Renderer light in unlockLights)
            {
                light.material.SetColor("_EmissionColor", Color.red * 20f);
            }

            unlocks = new bool[blockDetectors.Length];

        }

        public void UnlockDoor(BlockDetection blockDetection)
        {
            unlocks[Array.IndexOf(blockDetectors, blockDetection)] = true;
            unlockLights[Array.IndexOf(blockDetectors, blockDetection)].material.SetColor("_EmissionColor", Color.green * 20f);
            DoorOpenClose();
        }

        public void LockDoor(BlockDetection blockDetection)
        {
            unlocks[Array.IndexOf(blockDetectors, blockDetection)] = false;
            unlockLights[Array.IndexOf(blockDetectors, blockDetection)].material.SetColor("_EmissionColor", Color.red * 20f);
            DoorOpenClose();
        }

        void DoorOpenClose()
        {
            bool openDoor = true;
            foreach (bool unlock in unlocks)
            {
                if (!unlock)
                {
                    openDoor = false;
                }
            }

            if (openDoor)
            {
                anim.Play("OpenClose", 0, Mathf.Clamp(anim.GetCurrentAnimatorStateInfo(0).normalizedTime, 0f, 1f));
                anim.SetFloat("Speed", 1f);

                GameManager.instance.ChangeState(GameManager.GameState.LevelEnd);
            }

            else
            {
                anim.Play("OpenClose", 0, Mathf.Clamp(anim.GetCurrentAnimatorStateInfo(0).normalizedTime, 0f, 1f));
                anim.SetFloat("Speed", -1f);
            }

        }

    }
}
