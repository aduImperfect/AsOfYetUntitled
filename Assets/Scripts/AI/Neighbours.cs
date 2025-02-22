using System.Collections.Generic;
using UnityEngine;

public static class Neighbours
{
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
        float unitSize = PathfindingHandler.GetUnitSize();

        for (int i = -1; i < 2; ++i)
        {
            for (int k = -1; k < 2; ++k)
            {
                //Refers to current position that is not needed in the list of neighbours.
                if ((i == 0) && (k == 0))
                {
                    //Debug.Log("Ignore Self!!");
                    continue;
                }

                Vector3 newPos = new Vector3(position.x + (i * unitSize), position.y + (k * unitSize), position.z);

                //Debug.Log("Neighbour Pos: " + newPos);

                Dictionary<Collider2D, string> objectColliderTags = new Dictionary<Collider2D, string>();
                AreaInformation.GetCollidersFromPosition(newPos, new Vector2(unitSize, unitSize), ref objectColliderTags);

                bool foundCollider = false;

                foreach (Collider2D collider in objectColliderTags.Keys)
                {
                    //Debug.Log("Collider Object: " + collider.gameObject);
                    if (!collider.isTrigger)
                    {
                        //Debug.Log("This Object Is Not A TRIGGER!!! Breaking from Loop.");
                        foundCollider = true;
                        break;
                    }
                }

                if (foundCollider)
                {
                    //Debug.Log("Found A Collider In The Neighbour Area: " + newPos);
                    continue;
                }

                bool foundAdjacentCollider1 = false;
                bool foundAdjacentCollider2 = false;

                if (Mathf.Abs(i) == Mathf.Abs(k))
                {
                    //Debug.Log("This Is A Diagonal.");

                    Vector3 newPosNeigh1 = new Vector3(position.x + (i * unitSize), position.y, position.z);

                    //Debug.Log("Adjacent To Neighbour Pos 1: " + newPosNeigh1);

                    Dictionary<Collider2D, string> objectColliderTags1 = new Dictionary<Collider2D, string>();
                    AreaInformation.GetCollidersFromPosition(newPosNeigh1, new Vector2(unitSize, unitSize), ref objectColliderTags1);

                    foreach (Collider2D collider in objectColliderTags1.Keys)
                    {
                        //Debug.Log("Collider Object: " + collider.gameObject);
                        if (!collider.isTrigger)
                        {
                            //Debug.Log("This Object Is Not A TRIGGER!!! Breaking from Loop.");
                            foundAdjacentCollider1 = true;
                            break;
                        }
                    }

                    if (foundAdjacentCollider1)
                    {
                        //Debug.Log("Found A Collider In The Adjacent To Neighbour Areas 1: " + newPosNeigh1);
                        continue;
                    }

                    Vector3 newPosNeigh2 = new Vector3(position.x, position.y + (k * unitSize), position.z);

                    //Debug.Log("Adjacent To Neighbour Pos 2: " + newPosNeigh2);

                    Dictionary<Collider2D, string> objectColliderTags2 = new Dictionary<Collider2D, string>();
                    AreaInformation.GetCollidersFromPosition(newPosNeigh2, new Vector2(unitSize, unitSize), ref objectColliderTags2);

                    foreach (Collider2D collider in objectColliderTags2.Keys)
                    {
                        //Debug.Log("Collider Object: " + collider.gameObject);
                        if (!collider.isTrigger)
                        {
                            //Debug.Log("This Object Is Not A TRIGGER!!! Breaking from Loop.");
                            foundAdjacentCollider2 = true;
                            break;
                        }
                    }

                    if (foundAdjacentCollider2)
                    {
                        //Debug.Log("Found A Collider In The Adjacent To Neighbour Areas 2: " + newPosNeigh2);
                        continue;
                    }
                }
                else
                {
                    //Debug.Log("This Is NOT A Diagonal.");
                }

                neighbours.Add(newPos);
                //Debug.Log("Valid Neighbour Added To Neighbour List: " + newPos);
            }
        }
    }

    private static void FindNeighboursSquareNonDiagonal(Vector3 position, ref List<Vector3> neighbours)
    {
        neighbours.Clear();
        float unitSize = PathfindingHandler.GetUnitSize();

        for (int i = -1; i < 2; ++i)
        {
            for (int k = -1; k < 2; ++k)
            {
                //Refers to current position that is not needed in the list of neighbours.
                if ((i == 0) && (k == 0))
                {
                    //Debug.Log("Ignore Self!!");
                    continue;
                }

                //Diagonals
                if (Mathf.Abs(i) == Mathf.Abs(k))
                {
                    //Debug.Log("Ignore Diagonals!!");
                    continue;
                }

                Vector3 newPos = new Vector3(position.x + (i * unitSize), position.y + (k * unitSize), position.z);

                Dictionary<Collider2D, string> objectColliderTags = new Dictionary<Collider2D, string>();
                AreaInformation.GetCollidersFromPosition(newPos, new Vector2(unitSize, unitSize), ref objectColliderTags);

                bool foundCollider = false;

                foreach (Collider2D collider in objectColliderTags.Keys)
                {
                    //Debug.Log("Collider Object: " + collider.gameObject);
                    if (!collider.isTrigger)
                    {
                        //Debug.Log("This Object Is Not A TRIGGER!!! Breaking from Loop.");
                        foundCollider = true;
                        break;
                    }
                }

                if (foundCollider)
                {
                    //Debug.Log("Found A Collider In The Neighbour Areas: " + newPos);
                    continue;
                }

                neighbours.Add(newPos);
            }
        }
    }

    private static void FindNeighboursHexagon(Vector3 position, ref List<Vector3> neighbours)
    {

    }
}
