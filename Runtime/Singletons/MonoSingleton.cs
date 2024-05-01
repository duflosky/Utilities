using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : Component
{
    public static T instance;

    private void Awake()
    {
        if (instance is not null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        instance = this as T;
    }
}
