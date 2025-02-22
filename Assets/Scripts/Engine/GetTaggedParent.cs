using JetBrains.Annotations;
using UnityEngine;

public static class GetTaggedParent
{
    public static void GetTaggedParentObject(ref GameObject obj)
    {
        if (obj != null)
        {
            while (obj.CompareTag("Untagged"))
            {
                obj = obj.transform.parent.gameObject;
            }
        }
    }
}
