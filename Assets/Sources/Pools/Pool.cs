using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class Pool<T> where T : Object
{
    private readonly Queue<T> pool = new();
    private readonly T defaultItem;

    public Pool(T defaultItem, int count = 0, Action<T> callback = null)
    {
        this.defaultItem = defaultItem;
        for (int i = 0; i < count; i++)
        {
            var element = Object.Instantiate(defaultItem);
            AddToPool(element, callback);
        }
    }

    public void AddToPool(T toAdd, Action<T> callback = null)
    {
        if (toAdd is GameObject t)
        {
            t.SetActive(false);
        }

        pool.Enqueue(toAdd);
    }

    public T GetFromPool(Action<T> callback = null)
    {
        T element = pool.Count > 0 ? pool.Dequeue() : Object.Instantiate(defaultItem);

        if (element is GameObject t)
        {
            t.SetActive(true);
        }
        else if (element is Component mono)
        {
            mono.gameObject.SetActive(true);
        }

        callback?.Invoke(element);
        return element;
    }

    public IEnumerator AddToPoolLater(T item, float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        pool.Enqueue(item);
    }
}