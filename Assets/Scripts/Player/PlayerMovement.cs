using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Camera _camera;

    //************* HORIZONTAL MOVEMENT *************
    private readonly float _screenBoundaryHorizontal = 16f; //Witch patrols between -16 and +16 on the X-axis
    private readonly float _baseMoveSpeedHorizontal = 2f;
    private readonly float _bonusSpeedMultiplier = 3f;
    private float _currentMoveSpeedHorizontal;
    private bool DirectionHorizontal => (_currentMoveSpeedHorizontal > 0); //false for left, true for right

    //************* VERTICAL MOVEMENT *************
    private readonly float _screenBoundaryVerticalUpper = 4f; //Witch can move between -2 and +4 on the Y-axis
    private readonly float _screenBoundaryVerticalBottom = -2f;
    private float _moveSpeedVertical = 2f;
    private bool IsDiving => (_moveSpeedVertical < 0); //false for rising, true for diving

    private void Awake()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
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
        float newMoveSpeed = _baseMoveSpeedHorizontal + (_bonusSpeedMultiplier * heightRatio);

        //Player's speed value contains direction information with its sign
        //Before assigning the new move speed, you need to check its sign/direction
        if(DirectionHorizontal)
        {
            _currentMoveSpeedHorizontal = newMoveSpeed;
        }
        else
        {
            _currentMoveSpeedHorizontal = -newMoveSpeed;
        }
        
    }
    private void ReadUserInput()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
        {
            ChangeDirection(ref _moveSpeedVertical);
        }
    }
    private void Move()
    {
        // Move horizontal
        if (IsOutOfHorizontalBoundary())
        {
            ChangeDirection(ref _currentMoveSpeedHorizontal);
        }
        else
        {
            Vector3 moveAmount = new(_currentMoveSpeedHorizontal, 0f, 0f);
            transform.position += (moveAmount * Time.deltaTime);
        }

        // Move vertical
        if (!IsOutOfVerticalBoundary())
        {
            Vector3 moveAmount = new(0f, _moveSpeedVertical, 0f);
            transform.position += (moveAmount * Time.deltaTime);
        }
    }
    private bool IsOutOfHorizontalBoundary()
    {
        // To decide whether the witch is out of boundary
        // We will check not only the witch's position, but also its direction.
        // Because if we ignore its direction, once it is out of boundry it will continuously assume
        // that the witch reached the boundary and it will change witch's direction continuously.

        // If it is moving to the left and out of left boundary
        if ((transform.position.x < -_screenBoundaryHorizontal) && !DirectionHorizontal) return true;
        // If it is moving to the right and out of right boundary
        if ((transform.position.x > _screenBoundaryHorizontal) && DirectionHorizontal) return true;

        return false;
    }

    private bool IsOutOfVerticalBoundary()
    {
        // If it is diving and out of bottom boundary
        if ((transform.position.y < _screenBoundaryVerticalBottom) && IsDiving) return true;
        // If it is rising and out of upper boundary
        if ((transform.position.y > _screenBoundaryVerticalUpper) && !IsDiving) return true;

        return false;
    }

    private void ChangeDirection(ref float speed)
    {
        speed *= -1;
    }

    
}
