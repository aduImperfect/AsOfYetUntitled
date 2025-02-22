using System.Collections.Generic;
using UnityEngine;
using Utils;

public static class PathfindingHandler
{
    public static PriorityQueue<Vector3, float> pathfindingFrontier;
    public static Dictionary<Vector3, Vector3> cameFromDictionary;
    public static Dictionary<Vector3, float> costSoFarDictionary;
    public static float unitSize;
    public static uint timeoutCounter;
    public static uint timeoutCounterMax;

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

    public static float GetUnitSize()
    {
        return unitSize;
    }

    public static void SetUnitSize(float uSize)
    {
        unitSize = uSize;
    }

    public static uint GetMaxTimeoutCounter()
    {
        return timeoutCounterMax;
    }

    public static void SetMaxTimeoutCounter(uint tc)
    {
        timeoutCounterMax = tc;
    }

    public static void CalculatePath(Vector3 posStart, Vector3 posGoal, PathfindingAlgorithmType algorithmType, PathfindingSearchSpace searchSpace, HeuristicType heuristicType)
    {
        switch (algorithmType)
        {
            case PathfindingAlgorithmType.ASTAR:
                CalculatePathUsingAStar(posStart, posGoal, searchSpace, heuristicType);
                break;
            case PathfindingAlgorithmType.JPS:
                CalculatePathUsingJPS(posStart, posGoal);
                break;
            case PathfindingAlgorithmType.BLOCKASTAR:
                CalculatePathUsingBlockAStar(posStart, posGoal);
                break;
        }
    }

    private static void CalculatePathUsingAStar(Vector3 posStart, Vector3 posGoal, PathfindingSearchSpace searchSpace, HeuristicType heuristicType)
    {
        Debug.Log("____________________________Pathfinding Function Entered!!!!____________________________");

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

        Debug.Log("Starting Position: " + posStart + " entered into the PathfindingFrontier with PriorityVal of: 0");

        //cameFrom[start] = none/start (self).
        //costSoFar[start] = 0.
        cameFromDictionary[posStart] = posStart;
        costSoFarDictionary[posStart] = 0;

        timeoutCounter = 0;

        Debug.Log("<size=14><b>____________________________Begin Pathfinding!!!!____________________________</b></size>");

        // While frontier is not empty.
        while ((pathfindingFrontier.Count != 0) && (timeoutCounter++ < timeoutCounterMax))
        {
            Debug.Log("<size=16>TimeoutCounter = " + timeoutCounter + "</size>");

            //current = frontier.get().
            Vector3 currentPos = pathfindingFrontier.Dequeue();

            //Debug.Log("Current Position: " + currentPos);

            //if (current == goal).
            if ((Mathf.Abs(currentPos.x - posGoal.x) < unitSize) && (Mathf.Abs(currentPos.y - posGoal.y) < unitSize) && (Mathf.Abs(currentPos.z - posGoal.z) < unitSize))
            {
                float actualCostCurrentToGoal = Heuristics.CalculateActualCost(currentPos, posGoal);

                if (actualCostCurrentToGoal != 0)
                {
                    float newCost = costSoFarDictionary[currentPos] + actualCostCurrentToGoal;

                    costSoFarDictionary[posGoal] = newCost;
                    cameFromDictionary[posGoal] = currentPos;
                }

                Debug.Log("Goal Found!! It is within " + unitSize + " range of distance.");
                break;
            }

            //Debug.Log("<size=14><color=orange>____________________________Neighbours____________________________</color></size>");

            //Get the neighbors.
            List<Vector3> neighbourPositions = new List<Vector3>();

            Neighbours.FindNeighbours(currentPos, ref neighbourPositions, searchSpace);

            foreach (Vector3 neighbourPos in neighbourPositions)
            {
                //newCost = costSoFar[current] + graph.cost(current, next).
                float actualCostCurrentToNeighbor = Heuristics.CalculateActualCost(currentPos, neighbourPos);
                float newCost = costSoFarDictionary[currentPos] + actualCostCurrentToNeighbor;

                //Debug.Log("CostSoFarDictionary[" + currentPos + "] = " + costSoFarDictionary[currentPos]);
                //Debug.Log("ActualCostCurrentToNeighbor = " + actualCostCurrentToNeighbor);
                //Debug.Log("NewCost = " + newCost + " which is equal to costSoFarDictionary[" + currentPos + "] + ActualCostCurrentToNeighbor");

                //If value does not exist.. Set it to maximum floating value.
                if (costSoFarDictionary.ContainsKey(neighbourPos) == false)
                {
                    costSoFarDictionary[neighbourPos] = float.MaxValue;
                    //Debug.Log("CostSoFarDictionary[" + neighbourPos + "] = " + float.MaxValue);
                }

                //if next not in costSoFar or newCost < costSoFar[next].
                if (newCost < costSoFarDictionary[neighbourPos])
                {
                    //costSoFar[next] = newCost.
                    costSoFarDictionary[neighbourPos] = newCost;
                    //Debug.Log("CostSoFarDictionary[" + neighbourPos + "] = " + newCost);

                    //priority = newCost + heuristic(next, goal).
                    float neighborToGoalHeuristic = Heuristics.CalculateHeuristic(neighbourPos, posGoal, heuristicType);
                    float priorityVal = newCost + neighborToGoalHeuristic;

                    //Debug.Log("NewCost = " + newCost);
                    //Debug.Log("NeighborToGoalHeuristic = " + neighborToGoalHeuristic);
                    //Debug.Log("PriorityVal = " + priorityVal + " which is equal to NewCost + NeighborToGoalHeuristic");

                    //frontier.put(next, priority).
                    pathfindingFrontier.Enqueue(neighbourPos, priorityVal);
                    //Debug.Log("Position: " + neighbourPos + " entered into the PathfindingFrontier with PriorityVal of: " + priorityVal);

                    //cameFrom[next] = current.
                    cameFromDictionary[neighbourPos] = currentPos;
                    //Debug.Log("CameFromDictionary[" + neighbourPos + "] = " + currentPos);
                }
            }
        }

        Debug.Log("<size=24><color=yellow><b>End Pathfinding!!!!</b></color></size>");

        if (timeoutCounter >= timeoutCounterMax)
        {
            Debug.Log("<color=red><b>Timeout!!! Took too long!! Above " + timeoutCounter + " iterations</b></color>");
        }
        else
        {
            Vector3 currPos = posGoal;
            currentPath.Add(currPos);
            //Debug.Log("Adding Current Position: " + currPos + " to currentPath.");

            currentPathCost.Add(costSoFarDictionary[currPos]);
            //Debug.Log("Adding CostSoFarDictionary[: " + currPos + "] to currentPathCost.");

            while (cameFromDictionary[currPos] != currPos)
            {
                //Debug.Log("CameFromDictionary[" + currPos + "] != " + currPos);

                currPos = cameFromDictionary[currPos];
                //Debug.Log("New Current Position = " + currPos);

                currentPath.Add(currPos);
                //Debug.Log("Adding Current Position: " + currPos + " to currentPath.");

                currentPathCost.Add(costSoFarDictionary[currPos]);
                //Debug.Log("Adding CostSoFarDictionary[: " + currPos + "] to currentPathCost.");
            }

            currentPath.Reverse();
            //Debug.Log("Reverse Current Path.");

            currentPathCost.Reverse();
            //Debug.Log("Reverse Current Path Cost.");
        }

        Debug.Log("____________________________Pathfinding Function Exited!!!!____________________________");
    }

    private static void CalculatePathUsingJPS(Vector3 posStart, Vector3 posGoal)
    {

    }

    private static void CalculatePathUsingBlockAStar(Vector3 posStart, Vector3 posGoal)
    {

    }
}
