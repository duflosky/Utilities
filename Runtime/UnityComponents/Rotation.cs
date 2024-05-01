using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private Vector3 rotation = Vector3.zero;

    private void FixedUpdate()
    {
        transform.Rotate(rotation);
    }
}
