using UnityEngine;
using System.Collections.Generic;


//Code adapted from https://learn.unity.com/tutorial/introduction-to-object-pooling#
public class ObjectPool : MonoBehaviour
{
   // public static ObjectPool SharedInstance;
    public List<Projectile> pooledObjects;
    public Projectile objectToPool;
    [SerializeField]
    public int amountToPool;
/*
    void Awake()
    {
        SharedInstance = this;
    }
*/
    void Start()
    {
        pooledObjects = new List<Projectile>();
        Projectile tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.gameObject.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    public Projectile GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].gameObject.activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

    
}

