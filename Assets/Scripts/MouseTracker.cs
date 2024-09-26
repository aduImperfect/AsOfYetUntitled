using UnityEngine;

public class MouseTracker : MonoBehaviour
{
    [SerializeField] public Vector3 currentMouseWorldPos;

    private void Update()
    {
        currentMouseWorldPos = MouseHandler.GetMouseWorldPosition();
    }
}
