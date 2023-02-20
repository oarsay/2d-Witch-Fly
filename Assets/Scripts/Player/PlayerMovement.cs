using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //************* HORIZONTAL MOVEMENT *************
    private readonly float _screenBoundryHorizontal = 16f; //Witch patrols between -16 and +16 on the X-axis
    private float _moveSpeedHorizontal = 3f;
    private bool DirectionHorizontal => (_moveSpeedHorizontal > 0); //false for left, true for right

    //************* VERTICAL MOVEMENT *************
    private float _moveSpeedVertical = 1f;
    private bool DirectionVertical => (_moveSpeedVertical < 0); //false for rising, true for diving

    void Update()
    {
        Move();
        CheckForBoundaries();
    }
    private void Move()
    {
        // Move horizontal
        Vector3 moveAmount = new(_moveSpeedHorizontal, 0f, 0f);
        transform.position += (moveAmount * Time.deltaTime);
    }

    private void CheckForBoundaries()
    {
        if (IsOutOfHorizontalBoundary())
        {
            ChangeDirection(ref _moveSpeedHorizontal);
        }
    }
    private bool IsOutOfHorizontalBoundary()
    {
        // To decide whether the witch is out of boundary
        // We will check not only the witch's position, but also its direction.
        // Because if we ignore its direction, once it is out of boundry it will continuously assume
        // that the witch reached the boundary and it will change witch's direction continuously.

        // If moving to the left and out of left boundary
        if ((transform.position.x < -_screenBoundryHorizontal) && !DirectionHorizontal) return true;
        // If moving to the right and out of right boundary
        if ((transform.position.x > _screenBoundryHorizontal) && DirectionHorizontal) return true;

        return false;
    }

    private bool IsOutOfVerticalBoundary()
    {
        return false;
    }

    private void ChangeDirection(ref float speed)
    {
        speed *= -1;
    }
}
