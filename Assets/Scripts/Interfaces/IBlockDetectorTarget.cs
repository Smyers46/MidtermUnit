using UnityEngine;

namespace Midterm
{
    public interface IBlockDetectorTarget
    {
        void UnlockDoor(BlockDetection blockDetection);
        void LockDoor(BlockDetection blockDetection);
    }
}
