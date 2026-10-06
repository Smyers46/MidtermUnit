using UnityEngine;
using System;
using UnityEngine.Playables;

namespace Midterm
{
    public class DoorRoom4 : MonoBehaviour, IBlockDetectorTarget
    {
        [SerializeField] BlockDetection[] blockDetectors;
        [SerializeField] Renderer[] unlockLights;

        [SerializeField] PlayableDirector level4EndCutscene;
        bool[] unlocks;

        private void Awake()
        {
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
            Debug.Log("UNLOCK CALLED: " + blockDetection.name);

            int index = Array.IndexOf(blockDetectors, blockDetection);

            Debug.Log("Index: " + index);

            unlocks[Array.IndexOf(blockDetectors, blockDetection)] = true;
            unlockLights[Array.IndexOf(blockDetectors, blockDetection)].material.SetColor("_EmissionColor", Color.green * 20f);
            OpenDoor4();
        }

        public void LockDoor(BlockDetection blockDetection)
        {
            unlocks[Array.IndexOf(blockDetectors, blockDetection)] = false;
            unlockLights[Array.IndexOf(blockDetectors, blockDetection)].material.SetColor("_EmissionColor", Color.red * 20f);
            OpenDoor4();
        }

        void OpenDoor4()
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
                level4EndCutscene.Play();
                GameManager.instance.ChangeState(GameManager.GameState.LevelEnd);
            }

            else return;

        }

    }
}
