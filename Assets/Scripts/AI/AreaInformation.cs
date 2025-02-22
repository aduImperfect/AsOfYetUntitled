using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public static class AreaInformation
{
    public static bool IsColliderExisting(Vector2 position, Vector2 size, ref string resultTag)
    {
        Collider2D resultCollider;

        resultCollider = Physics2D.OverlapBox(position, size, 0.0f);

        if (resultCollider != null)
        {
            resultTag = resultCollider.tag;
            return true;
        }
        return false;
    }

    public static Collider2D GetColliderFromPosition(Vector2 position, Vector2 size, ref string resultTag)
    {
        Collider2D resultCollider;

        resultCollider = Physics2D.OverlapBox(position, size, 0.0f);
        if (resultCollider != null)
        {
            resultTag = resultCollider.tag;
        }

        return resultCollider;
    }

    public static void GetCollidersFromPosition(Vector2 position, Vector2 size, ref Dictionary<Collider2D, string> colliderTags)
    {
        Collider2D[] resultColliders;

        resultColliders = Physics2D.OverlapBoxAll(position, size, 0.0f);

        for (int i = 0; i < resultColliders.Length; ++i)
        {
            colliderTags.Add(resultColliders[i], resultColliders[i].tag);
        }
    }
}
