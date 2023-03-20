using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;

    // Animation parameters
    const string ON_SPEED = "onSpeed";
    const string LAUGH = "laugh";

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnChildHunt()
    {
        _animator.SetTrigger(LAUGH);
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
