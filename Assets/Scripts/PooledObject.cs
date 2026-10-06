using UnityEngine;

namespace Midterm
{
    public class PooledObject : MonoBehaviour
    {
        private ObjectPool poolReference;

        public void SetObjectPool(ObjectPool _pool)
        {
            //Sets our reference to the object pool
            poolReference = _pool;
        }

        public void DestroyWithTime(float time)
        {
            //Calls our reset object method after a certain amount of time
            Invoke("ResetObject", time);
        }

        public void ResetObject()
        {
            //Returns object to the object pool and sets it inactive
            poolReference.RestoreObject(this);
        }

    }
}
