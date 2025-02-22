using UnityEngine;

public enum FaceDirection
{
    NORTH = 0,
    NORTHEAST,
    EAST,
    SOUTHEAST,
    SOUTH,
    SOUTHWEST,
    WEST,
    NORTHWEST
}

public static class CharacterFacingDirection
{
    public static FaceDirection GetDirection(Vector3 startingPos, Vector3 endingPos)
    {
        float xDiff = endingPos.x - startingPos.x;
        float zDiff = endingPos.z - startingPos.z;

        bool xMajor = Mathf.Abs(xDiff) > Mathf.Abs(zDiff);
        bool xzEqual = Mathf.Abs(xDiff) == Mathf.Abs(zDiff);

        if (xMajor)
        {
            if (xDiff >= 0)
            {
                return FaceDirection.EAST;
            }

            if (xDiff < 0)
            {
                return FaceDirection.WEST;
            }
        }
        else
        {
            if (zDiff >= 0)
            {
                return FaceDirection.NORTH;
            }

            if (zDiff < 0)
            {
                return FaceDirection.SOUTH;
            }
        }

        if (xzEqual)
        {
            if ((xDiff >= 0) && (zDiff >= 0))
            {
                return FaceDirection.NORTHEAST;
            }

            if ((xDiff < 0) && (zDiff >= 0))
            {
                return FaceDirection.NORTHWEST;
            }

            if ((xDiff >= 0) && (zDiff < 0))
            {
                return FaceDirection.SOUTHEAST;
            }

            if ((xDiff < 0) && (zDiff < 0))
            {
                return FaceDirection.SOUTHWEST;
            }
        }

        return FaceDirection.NORTH;
    }
}
