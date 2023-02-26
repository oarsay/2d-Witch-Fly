using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildMovement : MonoBehaviour
{
    private ChildManager _childManager;

    private static readonly float _minWalkSpeed = 1f;
    private static readonly float _maxWalkSpeed = 3f;
    private static readonly float _fleeSpeedBonus = 1.5f;
    private static float _walkableAreaLeftBoundary;
    private static float _walkableAreaRightBoundary;

    private float _walkSpeed;
    private float _fleeSpeed;
    private float _targetPositionX;//Random target point on X-axis to walk or flee.
    private void Awake()
    {
        _childManager = GetComponent<ChildManager>();
        _walkSpeed = GenerateRandomBetween(_minWalkSpeed, _maxWalkSpeed);
        _fleeSpeed = _walkSpeed + _fleeSpeedBonus;
        var walkableAreaSR = GameObject.FindGameObjectWithTag("WalkableArea").GetComponent<SpriteRenderer>();
        _walkableAreaLeftBoundary = walkableAreaSR.bounds.min.x;
        _walkableAreaRightBoundary = walkableAreaSR.bounds.max.x;
    }

    private static float GenerateRandomBetween(float min, float max)
    {
        return Random.Range(min, max);
    }

    private void Update()
    {
        switch(_childManager.state)
        {
            case ChildState.Walk:
                Move(_walkSpeed);
                break;
            case ChildState.Flee:
                Move(_fleeSpeed);
                break;
            case ChildState.Hide: break;
            case ChildState.Hunted: break;
            case ChildState.Fall: break;
            case ChildState.Die: break;
        }
    }

    private void Move(float moveSpeed)
    {
        throw new System.NotImplementedException();
    }
}
