using System.Collections.Generic;
using UnityEngine;

public abstract class MonoLoopFunctionsManager<I> : MonoBehaviour where I : class
{
    private static HashSet<I> IMonoLoopFunctions = new();

    private static HashSet<I> toAdded = new();

    private static HashSet<I> toRemoved = new();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public static void Register(I mono)
    {
        toAdded.Add(mono);
    }

    public static void Unregister(I mono)
    {
        toRemoved.Add(mono);
    }

    public void LaunchLoop()
    {
        if (toRemoved.Count != 0)
        {
            foreach (var element in toRemoved)
            {
                IMonoLoopFunctions.Remove(element);
            }

            toRemoved.Clear();
        }

        if (toAdded.Count != 0)
        {
            foreach (var element in toAdded)
            {
                IMonoLoopFunctions.Add(element);
            }

            toAdded.Clear();
        }

        HashSet<I>.Enumerator e = IMonoLoopFunctions.GetEnumerator();
        while (e.MoveNext())
        {
            UpdateElement(e);
        }

        e.Dispose();
    }

    public abstract void UpdateElement(HashSet<I>.Enumerator e);
}