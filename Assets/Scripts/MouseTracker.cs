using UnityEngine;

public class MouseTracker : MonoBehaviour
{
    [SerializeField] public Vector3 currentMouseWorldPos;
    [SerializeField] public GameObject currentHoveringGameObject;

    [SerializeField] public GameObject selectedGameObject;
    [SerializeField] public GameObject otherSelectedGameObject;

    private void Awake()
    {
        
    }

    private void Update()
    {
        currentMouseWorldPos = MouseHandler.GetMouseWorldPosition();
        currentHoveringGameObject = MouseHandler.GetGameObjectAtPosition();
    }

    public Vector3 GetCurrentMouseWorldPos() { return currentMouseWorldPos; }
    public GameObject GetHoveringGameObject() {  return currentHoveringGameObject; }
}
