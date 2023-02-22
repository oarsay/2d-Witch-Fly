using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private float _followSpeed = 4f;
    private Transform _target;

    private void Awake()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        Vector3 targetPosition = new(_target.position.x, transform.position.y, -10f);
        transform.position = Vector3.Slerp(transform.position, targetPosition, _followSpeed * Time.deltaTime);
    }
}
