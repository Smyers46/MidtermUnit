using System;
using UnityEngine;

namespace Midterm
{
    public class KeyCardBox : MonoBehaviour, IBlockDetectorTarget
    {
        [SerializeField] BlockDetection[] blockDetectors;
        [SerializeField] Renderer[] unlockLights;

        Animator anim;
        bool[] unlocks;

        public bool isOpen { get; private set; }

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

            int index = Array.IndexOf(blockDetectors, blockDetection);

            if (index == -1)
            {
                return;
            }

            unlocks[index] = true;

            unlockLights[index].material.SetColor("_EmissionColor",Color.green * 20f
            );

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
                anim.Play("CloseOpen", 0, 0f);
                isOpen = true;
            }
            else
            {
                anim.Play("OpenClose", 0, 0f);
                isOpen = false;
            }

        }
    }
}
