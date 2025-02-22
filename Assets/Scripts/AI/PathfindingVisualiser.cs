using System.Collections.Generic;
using UnityEngine;

public class PathfindingVisualiser : MonoBehaviour
{
    [SerializeField] public Vector3 startPos;
    [SerializeField] public Vector3 goalPos;

    [SerializeField] public float unitSize;
    [SerializeField] public uint timeoutCounterMax;

    private int randLimit;

    [SerializeField] public List<Vector3> pathToGoal;
    [SerializeField] public List<float> costToGoal;

    [SerializeField] public Color linesColor;

    //[SerializeField] public float closenessToPositionDelta;

    [SerializeField] public bool canShowPath;

    [SerializeField] public PathfindingAlgorithmType algorithmType;

    [SerializeField] public PathfindingSearchSpace searchSpace;

    [SerializeField] public HeuristicType heuristicType;

    public void CalculatePath()
    {
        PathfindingHandler.CalculatePath(startPos, goalPos, algorithmType, searchSpace, heuristicType);
        pathToGoal = PathfindingHandler.GetCurrentPath();
        costToGoal = PathfindingHandler.GetCurrentPathCost();
    }

    public void GetRandomStartAndGoalPositionsAndCalculate()
    {
        pathToGoal.Clear();
        costToGoal.Clear();

        randLimit = 300;

        startPos.x = Random.Range(-(randLimit + 1), randLimit) + 0.5000f;
        startPos.y = Random.Range(-(randLimit + 1), randLimit) + 0.5000f;
        goalPos.x = Random.Range(-(randLimit + 1), randLimit) + 0.5000f;
        goalPos.y = Random.Range(-(randLimit + 1), randLimit) + 0.5000f;

        PathfindingHandler.CalculatePath(startPos, goalPos, algorithmType, searchSpace, heuristicType);
        pathToGoal.AddRange(PathfindingHandler.GetCurrentPath());
        costToGoal = PathfindingHandler.GetCurrentPathCost();
    }

    public void ShowPath()
    {
        if (!canShowPath)
        {
            return;
        }

        DebugManager.SetDebugDrawMode(true);
        DebugManager.DrawLines(pathToGoal, linesColor);
    }

    public void SetUnitSize()
    {
        PathfindingHandler.SetUnitSize(unitSize);
    }

    public void SetMaxTimeoutCounter()
    {
        PathfindingHandler.SetMaxTimeoutCounter(timeoutCounterMax);
    }

    public void ClearPath()
    {
        pathToGoal.Clear();
        costToGoal.Clear();
    }

    public void OnDrawGizmos()
    {
        ShowPath();
    }
}
