using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public GameObject standingOnObject;
    [SerializeField] public bool playerSelected;
    [SerializeField] public bool destinationSelected;
    [SerializeField] public GameObject destinationObject;
    [SerializeField] public Vector3 startingPos;
    [SerializeField] public Vector3 movingPos;
    [SerializeField] public Vector3 destinationPos;
    [SerializeField] public List<Vector3> finalPath;
    [SerializeField] public List<float> finalPathCost;
    [SerializeField] public FaceDirection currentDirection;
    [SerializeField] public FaceDirection newDirection;
    [SerializeField] public Quaternion oldDirectionQuat;
    [SerializeField] public Quaternion newDirectionQuat;

    [SerializeField] public float lerpSpeed = 0.01f;
    [SerializeField] public float timeCountForRotation = 0.0f;
    [SerializeField] public float timeCountForMovement = 0.0f;

    [SerializeField] public int baseDirectionRotationVal = 45;
    [SerializeField] public bool isTurning = false;
    [SerializeField] public bool isMoving = false;
    [SerializeField] public int directionChangeAmount = 0;
    [SerializeField] public Vector3 newDestination;

    private void Awake()
    {
        newDirection = currentDirection = FaceDirection.NORTH;
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
        SelectPlayerAndDestination();

        if (isTurning)
        {
            TurnOrientation();
        }
        else
        {
            currentDirection = newDirection;
        }

        if (isMoving && !isTurning)
        {
            MoveCharacter();
        }
    }

    private void SelectPlayerAndDestination()
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

        newDirection = CharacterFacingDirection.GetDirection(finalPath[0], finalPath[1]);
        SetFinalOrientation();
        SetMovement();
    }

    private void SetFinalOrientation()
    {
        //Already facing same direction
        if (newDirection == currentDirection)
        {
            isTurning = false;
            return;
        }

        directionChangeAmount = newDirection - currentDirection;

        oldDirectionQuat = this.transform.parent.rotation;
        newDirectionQuat = Quaternion.AngleAxis(baseDirectionRotationVal * directionChangeAmount, Vector3.up) * this.transform.parent.rotation;
        timeCountForRotation = 0.0f;
        isTurning = true;
    }

    private void TurnOrientation()
    {
        this.transform.parent.rotation = Quaternion.Lerp(oldDirectionQuat, newDirectionQuat, timeCountForRotation * lerpSpeed);
        timeCountForRotation += Time.deltaTime;

        if (this.transform.parent.rotation == newDirectionQuat)
        {
            timeCountForRotation = 0.0f;
            isTurning = false;
        }
    }

    private void SetMovement()
    {
        if (finalPath.Count <= 1)
        {
            return;
        }

        movingPos = finalPath[0];

        float posY = this.transform.parent.position.y;
        newDestination = finalPath[1];
        newDestination.y = posY;
        movingPos.y = posY;

        isMoving = true;
    }

    private void MoveCharacter()
    {
        Vector3 currPos = Vector3.Lerp(movingPos, newDestination, timeCountForMovement * lerpSpeed);
        this.transform.parent.position = currPos;

        timeCountForMovement += Time.deltaTime;

        if (currPos == newDestination)
        {
            timeCountForMovement = 0.0f;
            startingPos = newDestination;
            isMoving = false;
            //Remove the existing current position.
            finalPath.RemoveAt(0);
        }
    }
}
