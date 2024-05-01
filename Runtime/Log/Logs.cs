using System.Diagnostics;
using UnityEngine;

public class Logs : MonoBehaviour
{
    [Conditional("DEBUG")]
    public static void Log(string message)
    {
        UnityEngine.Debug.Log(message);
    }
}
