using UnityEngine;

public class MouseTracker : MonoBehaviour
{
    [SerializeField] public Vector3 currentMouseWorldPos;
    [SerializeField] public Vector3 selectedMouseWorldPos;
    [SerializeField] public GameObject currentHoveringGameObject;
    [SerializeField] public GameObject currentSelectedGameObject;

    public static MouseTracker instance;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        currentMouseWorldPos = MouseHandler.GetMouseWorldPosition();
        currentHoveringGameObject = MouseHandler.GetGameObjectAtPosition();

        if (Input.GetMouseButtonDown(0))
        {
            currentSelectedGameObject = currentHoveringGameObject;
            selectedMouseWorldPos = currentMouseWorldPos;
        }
    }

    public Vector3 GetCurrentMouseWorldPos() { return currentMouseWorldPos; }
    public Vector3 GetSelectedMouseWorldPos() { return selectedMouseWorldPos; }
    public GameObject GetHoveringGameObject() {  return currentHoveringGameObject; }
    public GameObject GetSelectedGameObject() { return currentSelectedGameObject; }
}
