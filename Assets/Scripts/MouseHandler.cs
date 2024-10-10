using UnityEngine;

public class MouseHandler
{
    private static GameObject gameObjectAtPos;

    #region Utils

    public static Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            gameObjectAtPos = raycastHit.collider.gameObject;
            return raycastHit.point;
        }

        gameObjectAtPos = null;
        return Vector3.zero;
    }

    public static GameObject GetGameObjectAtPosition()
    {
        return gameObjectAtPos;
    }

    #endregion
}
