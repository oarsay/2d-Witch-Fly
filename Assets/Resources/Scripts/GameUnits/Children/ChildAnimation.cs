using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildAnimation : MonoBehaviour
{
    private ChildManager _childManager;
    private Animator _animator;
    private string _currentState;

    // Animation states
    const string CHILD_WALK = "Child_Walk";
    const string CHILD_FLEE = "Child_Run";
    const string CHILD_HUNTED = "Child_Hunted";
    const string CHILD_HIDE = "Child_Hide";
    const string CHILD_FALL = "Child_Fall";


    private void Start()
    {
        _childManager = GetComponent<ChildManager>();
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        switch(_childManager.state)
        {
            case ChildState.Walk: UpdateAnimationState(CHILD_WALK); break;
            case ChildState.Flee: UpdateAnimationState(CHILD_FLEE); break;
            case ChildState.Hunted: UpdateAnimationState(CHILD_HUNTED); break;
            case ChildState.Hide: UpdateAnimationState(CHILD_HIDE); break;
            case ChildState.Fall: UpdateAnimationState(CHILD_FALL); break;
        }
    }

    private void UpdateAnimationState(string newState)
    {
        if (_currentState == newState) return;

        _animator.Play(newState);
        _currentState = newState;
    }
}
