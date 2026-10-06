using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    // - Two Lists for Object Pooling:  1 in use, 1 available
    public List<PooledObject> objectPool = new List<PooledObject>();
    public List<PooledObject> usedPool = new List<PooledObject>();

    // - Object Pool Count
    [SerializeField] int numberOfObjects = 5;

    // - OBJECT POOLING CHALLENGE:
    // - Create a two object pools specifically for rockets and bullets.  Modify your GetPooledObjects method to grab from either using a parameter.

    // - Reference Object to create
    public GameObject objectToCreate;

    private void Awake()
    {
        // - Singleton pattern
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }

        else if (Instance == null)
        {
            Instance = this;
        }

        // - Initialize our object pool
        Initialize();
    }

    public void Initialize()
    {
        // - Create X objects and add them to the pool
        for (int i = 0; i < numberOfObjects; i++)
        {
            AddNewObject();
        }        
    }

    public void AddNewObject()
    {
        // - Create a new object for our object pooling
        GameObject newObject = Instantiate(objectToCreate, transform.position, Quaternion.identity);
        
        // - Set the object pool reference in the pooled object so it can return itself when "destroyed"
        newObject.GetComponent<PooledObject>().SetObjectPool(this);
        
        // - Hide the object on creation, we are not using it.  It is available for use
        newObject.SetActive(false);

        // - Add the new object to the object pool list (available)
        objectPool.Add(newObject.GetComponent<PooledObject>());       
    }

    public PooledObject GetPooledObject()
    {       
        // - If there are any objects that we can grab from out available pool
        if (objectPool.Count > 0)
        {
            // - Grab the first available object from the pool and add it to the in use pool
            usedPool.Add(objectPool[0]);
            
            //- Remove the pooled object from the availablity pool so we don't grab it again
            objectPool.RemoveAt(0);

            // - Activate the object and return it to the caller
            usedPool[usedPool.Count - 1].gameObject.SetActive(true);
            return usedPool[usedPool.Count - 1];
        }

        else 
        {
            Debug.Log("NO POOLED OBJECT");
            return null;
        } 
    }

    public void RestoreObject(PooledObject _pooledObject)
    {
        // - Return the object to available pool
        objectPool.Add(_pooledObject);

        // - Remove the object from the used pool
        usedPool.Remove(_pooledObject);

        // - Set the object back to inactive
        _pooledObject.gameObject.SetActive(false);        
    }



}
