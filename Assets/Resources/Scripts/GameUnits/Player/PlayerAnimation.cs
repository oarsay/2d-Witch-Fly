using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;

    // Animation states
    const string IDLE = "Idle";
    const string ON_SPEED_BUFF = "Speedy";
    const string LAUGH = "Laugh";

    // Animation parameters
    const string ON_SPEED = "onSpeed";

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnChildHunt()
    {
        _animator.Play(LAUGH);
    }

    public void StartSpeedBuffAnimation()
    {
        _animator.SetBool(ON_SPEED, true);
    }

    public void EndSpeedBuffAnimation()
    {
        _animator.SetBool(ON_SPEED, false);
    }
}
