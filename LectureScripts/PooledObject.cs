using UnityEngine;

public class PooledObject : MonoBehaviour
{
    // - Reference to the object pool that created this object.
    public ObjectPool poolReference;
    
    public void SetObjectPool(ObjectPool _pool)
    {
        // - Sets our reference to the object pool
        poolReference = _pool;
    } 

    public void DestroyWithTime(float time)
    {
        // - Calls our reset object method after a certain amount of time
        Invoke("ResetObject", time);       
    }

    public void ResetObject()
    {
        // - Returns the object to the object pool and sets it inactive.
        poolReference.RestoreObject(this);
    }
}
