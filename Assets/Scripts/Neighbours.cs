using System.Collections.Generic;
using UnityEngine;

public static class Neighbours
{
    private static float unitSize = 25.0f;

    public static void FindNeighbours(Vector3 position, ref List<Vector3> neighbours, PathfindingSearchSpace searchSpace)
    {
        switch(searchSpace)
        {
            case PathfindingSearchSpace.SQUARE:
                FindNeighboursSquare(position, ref neighbours);
                break;
            case PathfindingSearchSpace.SQUARENONDIAGONAL:
                FindNeighboursSquareNonDiagonal(position, ref neighbours);
                break;
            case PathfindingSearchSpace.HEXAGON:
                FindNeighboursHexagon(position, ref neighbours);
                break;
            default:
                break;
        }
    }

    private static void FindNeighboursSquare(Vector3 position, ref List<Vector3> neighbours)
    {
        neighbours.Clear();

        for (int i = -1; i < 2; ++i)
        {
            //for (int j = -1; j < 2; ++j)
            //{
                for (int k = -1; k < 2; ++k)
                {
                    //Refers to current position that is not needed in the list of neighbours.
                    if ((i == 0) && (i == k))
                    {
                        continue;
                    }

                    neighbours.Add(new Vector3(position.x + (i * unitSize), position.y, position.z + (k * unitSize)));
                }
            //}
        }
    }

    private static void FindNeighboursSquareNonDiagonal(Vector3 position, ref List<Vector3> neighbours)
    {
        neighbours.Clear();

        for (int i = -1; i < 2; ++i)
        {
            //for (int j = -1; j < 2; ++j)
            //{
                for (int k = -1; k < 2; ++k)
                {
                    //Refers to current position that is not needed in the list of neighbours.
                    if ((i == 0) && (k == 0))
                    {
                        continue;
                    }

                    //Diagonals
                    if (Mathf.Abs(i) == Mathf.Abs(k))
                    {
                        continue;
                    }

                    neighbours.Add(new Vector3(position.x + (i * unitSize), position.y, position.z + (k * unitSize)));
                }
            //}
        }

    }

    private static void FindNeighboursHexagon(Vector3 position, ref List<Vector3> neighbours)
    {

    }
}
