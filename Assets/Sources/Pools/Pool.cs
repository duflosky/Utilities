using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : Object
{
    private readonly Queue<T> pool = new Queue<T>();
    private readonly T defaultItem;

    public Pool(T defaultItem)
    {
        this.defaultItem = defaultItem;
    }

    public void AddToPool(T toAdd)
    {
        if (toAdd is GameObject t)
        {
            t.SetActive(false);
        }
        pool.Enqueue(toAdd);
    }

    public T GetFromPool()
    {
        return pool.Count > 0 ? pool.Dequeue() : Object.Instantiate(defaultItem);
    }

    public IEnumerator AddToPoolLater(T item, float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        pool.Enqueue(item);
        
    }
}
