using UnityEngine;

public enum ChildDirection
{
    Left,
    Right
}
public class ChildMovement : MonoBehaviour
{
    private ChildManager _childManager;
    private Transform _cauldronDeepPoint;

    private static readonly float _minWalkSpeed = 1f;
    private static readonly float _maxWalkSpeed = 3f;
    private static readonly float _fleeSpeedBonus = 1.5f;
    private static readonly float _fallSpeed = 4f;
    private static readonly float _fallRotationSpeed = 1.5f;

    [HideInInspector] public ChildDirection childDirection;
    private float _walkSpeed;
    private float _fleeSpeed;
    private float _targetPositionX;//Random target point on X-axis to walk or flee.
    private void Awake()
    {
        _cauldronDeepPoint = GameObject.FindGameObjectWithTag(Tags.CAULDRON_DEEP_POINT).transform;
        _childManager = GetComponent<ChildManager>();
        _walkSpeed = Random.Range(_minWalkSpeed, _maxWalkSpeed);
        _fleeSpeed = _walkSpeed + _fleeSpeedBonus;
        UpdateTargetPosition();
        UpdateDirection();
    }

    private void Update()
    {
        switch (_childManager.state)
        {
            case ChildState.Walk:

                if(IsArrivedToTarget())
                {
                    UpdateTargetPosition();
                    UpdateDirection();
                }
                MoveToTargetBy(_walkSpeed);
                break;

            case ChildState.Flee:

                if (IsArrivedToTarget())
                {
                    UpdateTargetPosition();
                    UpdateDirection();
                }
                MoveToTargetBy(_fleeSpeed);
                break;

            case ChildState.Hide: break;
            case ChildState.Hunted: break;
            case ChildState.Fall:
                OnFall();
                break;
            case ChildState.Die: break;
        }
    }

    private void UpdateDirection()
    {

        if(_targetPositionX < transform.position.x)
        {
            childDirection = ChildDirection.Left;
            transform.rotation = Quaternion.Euler(new(0,0,0));
        }
        else if(_targetPositionX > transform.position.x)
        {
            childDirection = ChildDirection.Right;
            transform.rotation = Quaternion.Euler(new(0, 180, 0));
        }
        else
        {
            //Debug.Log("The child overlays with the target!");
        }
    }

    private bool IsArrivedToTarget()
    {
        return (Mathf.Abs(transform.position.x - _targetPositionX)) < 0.5f;
    }

    private void UpdateTargetPosition()
    {
        _targetPositionX = Random.Range(BoundsManager.walkableAreaLeftBoundary, BoundsManager.walkableAreaRightBoundary);
    }

    private void MoveToTargetBy(float moveSpeed)
    {
        Vector3 moveAmount = new(moveSpeed, 0f, 0f);

        switch (childDirection)
        {
            case ChildDirection.Left:
                transform.position -= (moveAmount * Time.deltaTime);
                break;
            case ChildDirection.Right:
                transform.position += (moveAmount * Time.deltaTime);
                break;
            default:
                ExceptionHandler.Throw(childDirection);
                break;
        }
    }

    private void OnFall()
    {
        Vector3 fallDirection = (_cauldronDeepPoint.position - transform.position).normalized;
        transform.position += _fallSpeed * Time.deltaTime * fallDirection;
        transform.Rotate(Vector3.forward, _fallRotationSpeed);
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(new(_targetPositionX, -3f, 0f), 0.3f);
    }
}
