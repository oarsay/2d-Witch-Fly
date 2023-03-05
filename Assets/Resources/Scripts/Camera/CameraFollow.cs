using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform _target;
    private readonly float _followSpeed = 2f;

    private void Awake()
    {
        _target = GameObject.FindGameObjectWithTag(Tags.CAMERA_TARGET).transform;
    }
    void LateUpdate()
    {
        Vector3 targetPosition = new(_target.position.x, transform.position.y, -10f);
        transform.position = Vector3.Slerp(transform.position, targetPosition, _followSpeed * Time.deltaTime);
    }
}