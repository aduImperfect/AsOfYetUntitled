using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using Utils;

public static class PathfindingHandler
{
    public static PriorityQueue<Vector3, float> pathfindingFrontier;
    public static Dictionary<Vector3, Vector3> cameFromDictionary;
    public static Dictionary<Vector3, float> costSoFarDictionary;

    private static List<Vector3> currentPath;
    private static List<float> currentPathCost;

    public static List<Vector3> GetCurrentPath()
    {
        return currentPath;
    }

    public static List<float> GetCurrentPathCost()
    {
        return currentPathCost;
    }

    public static void ClearCurrentPath()
    {
        currentPath.Clear();
    }
    public static void ClearCurrentPathCost()
    {
        currentPathCost.Clear();
    }

    public static void CalculatePath(Vector3 posStart, Vector3 posGoal, PathfindingAlgorithmType algorithmType)
    {
        switch (algorithmType)
        {
            case PathfindingAlgorithmType.ASTAR:
                CalculatePathUsingAStar(posStart, posGoal);
                break;
            case PathfindingAlgorithmType.JPS:
                CalculatePathUsingJPS(posStart, posGoal);
                break;
            case PathfindingAlgorithmType.BLOCKASTAR:
                CalculatePathUsingBlockAStar(posStart, posGoal);
                break;
        }
    }

    private static void CalculatePathUsingAStar(Vector3 posStart, Vector3 posGoal)
    {
        if (pathfindingFrontier == null)
        {
            pathfindingFrontier = new PriorityQueue<Vector3, float>();
        }

        if (cameFromDictionary == null)
        {
            cameFromDictionary = new Dictionary<Vector3, Vector3> ();
        }

        if (costSoFarDictionary == null)
        {
            costSoFarDictionary = new Dictionary<Vector3, float>();
        }

        if (currentPath == null)
        {
            currentPath = new List<Vector3>();
        }

        if (currentPathCost == null)
        {
            currentPathCost = new List<float>();
        }

        pathfindingFrontier.Clear();
        cameFromDictionary.Clear();
        costSoFarDictionary.Clear();
        currentPath.Clear();
        currentPathCost.Clear();

        //frontier.put (start, 0).
        pathfindingFrontier.Enqueue(posStart, 0);

        //cameFrom[start] = none/start (self).
        //costSoFar[start] = 0.
        cameFromDictionary[posStart] = posStart;
        costSoFarDictionary[posStart] = 0;

        // While frontier is not empty.
        while (pathfindingFrontier.Count != 0)
        {
            //current = frontier.get().
            Vector3 currentPos = pathfindingFrontier.Dequeue();

            //if (current == goal).
            if (currentPos == posGoal)
            {
                break;
            }

            //Get the neighbours.
            List<Vector3> neighbourPositions = new List<Vector3>();
            Neighbours.FindNeighbours(currentPos, ref neighbourPositions, PathfindingSearchSpace.SQUARENONDIAGONAL);

            foreach (Vector3 neighbourPos in neighbourPositions)
            {
                //newCost = costSoFar[current] + graph.cost(current, next).
                float newCost = costSoFarDictionary[currentPos] + Heuristics.CalculateActualCost(currentPos, neighbourPos);

                //If value does not exist.. Set it to maximum floating value.
                if (costSoFarDictionary.ContainsKey(neighbourPos) == false)
                {
                    costSoFarDictionary[neighbourPos] = float.MaxValue;
                }

                //if next not in costSoFar or newCost < costSoFar[next].
                if (newCost < costSoFarDictionary[neighbourPos])
                {
                    //costSoFar[next] = newCost.
                    costSoFarDictionary[neighbourPos] = newCost;

                    //priority = newCost + heuristic(next, goal).
                    float priorityVal = newCost + Heuristics.CalculateHeuristic(neighbourPos, posGoal, HeuristicsEnum.MANHATTAN_DISTANCE);

                    //frontier.put(next, priority).
                    pathfindingFrontier.Enqueue(neighbourPos, priorityVal);

                    //cameFrom[next] = current.
                    cameFromDictionary[neighbourPos] = currentPos;
                }
            }
        }

        Vector3 currPos = posGoal;
        currentPath.Add(currPos);
        currentPathCost.Add(costSoFarDictionary[currPos]);

        while (cameFromDictionary[currPos] != currPos)
        {
            currPos = cameFromDictionary[currPos];
            currentPath.Add(currPos);
            currentPathCost.Add(costSoFarDictionary[currPos]);
        }

        currentPath.Reverse();
        currentPathCost.Reverse();
    }

    private static void CalculatePathUsingJPS(Vector3 posStart, Vector3 posGoal)
    {

    }

    private static void CalculatePathUsingBlockAStar(Vector3 posStart, Vector3 posGoal)
    {

    }
}
