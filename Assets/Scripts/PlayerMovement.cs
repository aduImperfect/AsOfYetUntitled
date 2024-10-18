using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public GameObject standingOnObject;
    [SerializeField] public bool playerSelected;
    [SerializeField] public bool destinationSelected;
    [SerializeField] public GameObject destinationObject;
    [SerializeField] public Vector3 startingPos;
    [SerializeField] public Vector3 destinationPos;
    [SerializeField] public List<Vector3> finalPath;
    [SerializeField] public List<float> finalPathCost;
    [SerializeField] public FaceDirection currentDirection;

    private void Awake()
    {
        currentDirection = FaceDirection.NORTH;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider != null)
        {
            standingOnObject = collision.collider.gameObject;
            GetTaggedParent.GetTaggedParentObject(ref standingOnObject);
        }
    }

    private void Update()
    {
        GameObject selectedObj = MouseTracker.instance.GetSelectedGameObject();

        bool isPlayerGameObject = selectedObj == this.transform.parent.gameObject;

        if (!playerSelected && isPlayerGameObject)
        {
            playerSelected = true;
            destinationSelected = false;
            startingPos = standingOnObject.transform.position;
        }

        if (playerSelected && !destinationSelected && !isPlayerGameObject)
        {
            destinationSelected = true;
            destinationObject = selectedObj;
            destinationPos = MouseTracker.instance.GetSelectedMouseWorldPos();
        }

        if (playerSelected && destinationSelected)
        {
            startingPos.y = destinationPos.y;
            destinationPos.x = destinationObject.transform.position.x;
            destinationPos.z = destinationObject.transform.position.z;

            playerSelected = destinationSelected = false;
        }
    }

    public void GetPath()
    {
        PathfindingHandler.CalculatePath(startingPos, destinationPos, PathfindingAlgorithmType.ASTAR);
        finalPath = PathfindingHandler.GetCurrentPath();
        finalPathCost = PathfindingHandler.GetCurrentPathCost();
    }

    public void MoveStepByStep()
    {
        if (finalPath.Count <= 1)
        {
            return;
        }

        FaceDirection charDirection = CharacterFacingDirection.GetDirection(finalPath[0], finalPath[1]);

        this.transform.parent.rotation = Quaternion.identity;

        switch (charDirection)
        {
            case FaceDirection.NORTH:
                this.transform.parent.rotation = Quaternion.identity;
                break;
            case FaceDirection.NORTHEAST:
                this.transform.parent.rotation = Quaternion.AngleAxis(45, Vector3.up) * this.transform.parent.rotation;
                break;
            case FaceDirection.EAST:
                this.transform.parent.rotation = Quaternion.AngleAxis(90, Vector3.up) * this.transform.parent.rotation;
                break;
            case FaceDirection.SOUTHEAST:
                this.transform.parent.rotation = Quaternion.AngleAxis(135, Vector3.up) * this.transform.parent.rotation;
                break;
            case FaceDirection.SOUTH:
                this.transform.parent.rotation = Quaternion.AngleAxis(180, Vector3.up) * this.transform.parent.rotation;
                break;
            case FaceDirection.SOUTHWEST:
                this.transform.parent.rotation = Quaternion.AngleAxis(225, Vector3.up) * this.transform.parent.rotation;
                break;
            case FaceDirection.WEST:
                this.transform.parent.rotation = Quaternion.AngleAxis(270, Vector3.up) * this.transform.parent.rotation;
                break;
            case FaceDirection.NORTHWEST:
                this.transform.parent.rotation = Quaternion.AngleAxis(315, Vector3.up) * this.transform.parent.rotation;
                break;
        }

        currentDirection = charDirection;

        float posY = this.transform.parent.position.y;
        Vector3 newDest = finalPath[1];
        newDest.y = posY;

        this.transform.parent.position = newDest;

        //Remove the existing current position.
        finalPath.RemoveAt(0);
    }
}

