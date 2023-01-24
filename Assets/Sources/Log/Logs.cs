using System.Diagnostics;
using UnityEngine;

public class Logs : MonoBehaviour
{
    [Conditional("Debug")]
    public static void Log(string message)
    {
        UnityEngine.Debug.Log(message);
    }
}
