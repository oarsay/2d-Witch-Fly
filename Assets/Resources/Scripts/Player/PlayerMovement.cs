using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private GameEvent _gameEventPlayerOnBoundary;
    public enum Direction
    {
        Horizontal,
        Vertical,
        Right,
        Left,
        Up,
        Down
    }

    //************* HORIZONTAL MOVEMENT *************
    private float _screenBoundaryHorizontal; //Witch patrols between -boundary and +boundary on the X-axis
    private readonly float _baseMoveSpeedHorizontal = 1f;
    private readonly float _bonusSpeedMultiplier = 5f;
    private float _currentMoveSpeedHorizontal;
    private Direction _directionHorizontal = Direction.Left;
    public Direction DirectionHorizontal { get { return _directionHorizontal; } }

    //************* VERTICAL MOVEMENT *************
    private readonly float _screenBoundaryVerticalUpper = 4f; //Witch can move between -3.2 and +4 on the Y-axis
    private readonly float _screenBoundaryVerticalBottom = -3.2f;
    private float _moveSpeedVertical = 3f;
    private Direction _directionVertical = Direction.Up;

    private void Awake()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _screenBoundaryHorizontal = Mathf.Abs(BoundsManager.walkableAreaRightBoundary);
    }

    private void Update()
    {
        CalculateSpeedBasedOnHeight();
        ReadUserInput();
        Move();
    }

    private void CalculateSpeedBasedOnHeight()
    {
        //GENERAL FORMULA
        //Move speed = Base speed + (bonusMultiplier x heightRatio[0-1])

        //Get player's y position
        Vector3 playerScreenPos = _camera.WorldToScreenPoint(transform.position);
        //And normalize it
        float heightRatio = playerScreenPos.y / Screen.height;

        //Use general formula for calculating the new speed
        _currentMoveSpeedHorizontal = _baseMoveSpeedHorizontal + (_bonusSpeedMultiplier * heightRatio);
    }
    private void ReadUserInput()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
        {
            ChangeDirection(Direction.Vertical);
        }
    }
    private void Move()
    {
        // Move horizontal
        if (IsOutOfHorizontalBoundary())
        {
            ChangeDirection(Direction.Horizontal);
            _gameEventPlayerOnBoundary.TriggerEvent();
        }
        else
        {
            Vector3 moveAmount = new(_currentMoveSpeedHorizontal, 0f, 0f);
            if (_directionHorizontal == Direction.Right)
            {
                transform.position += (moveAmount * Time.deltaTime);
            }
            else if (_directionHorizontal == Direction.Left)
            {
                transform.position -= (moveAmount * Time.deltaTime);
            }
            else ExceptionHandler.Throw(_directionHorizontal);
        }

        // Move vertical
        if (!IsOutOfVerticalBoundary())
        {
            Vector3 moveAmount = new(0f, _moveSpeedVertical, 0f);
            if (_directionVertical == Direction.Up)
            {
                transform.position += (moveAmount * Time.deltaTime);
            }
            else if (_directionVertical == Direction.Down)
            {
                transform.position -= (moveAmount * Time.deltaTime);
            }
            else ExceptionHandler.Throw(_directionVertical);
        }
    }
    private bool IsOutOfHorizontalBoundary()
    {
        // To decide whether the witch is out of boundary
        // We will check not only the witch's position, but also its direction.
        // Because if we ignore its direction, once it is out of boundry it will continuously assume
        // that the witch reached the boundary and it will change witch's direction continuously.

        // If it is moving to the left and out of left boundary
        if ((transform.position.x < -_screenBoundaryHorizontal) && _directionHorizontal == Direction.Left) return true;
        // If it is moving to the right and out of right boundary
        if ((transform.position.x > _screenBoundaryHorizontal) && _directionHorizontal == Direction.Right) return true;

        return false;
    }
    private bool IsOutOfVerticalBoundary()
    {
        // If it is diving and out of bottom boundary
        if ((transform.position.y < _screenBoundaryVerticalBottom) && _directionVertical == Direction.Down) return true;
        // If it is rising and out of upper boundary
        if ((transform.position.y > _screenBoundaryVerticalUpper) && _directionVertical == Direction.Up) return true;

        return false;
    }
    private void ChangeDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.Horizontal:
                transform.Rotate(Vector3.up, 180);
                _directionHorizontal = (_directionHorizontal == Direction.Left) ? Direction.Right : Direction.Left;
                break;
            case Direction.Vertical:
                _directionVertical = (_directionVertical == Direction.Up) ? Direction.Down : Direction.Up;
                break;
            default:
                ExceptionHandler.Throw(direction);
                break;
        }
    }
}
