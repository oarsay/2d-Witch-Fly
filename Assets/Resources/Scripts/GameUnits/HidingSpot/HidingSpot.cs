using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    private bool _isEmpty = true;
    public bool IsEmpty { get { return _isEmpty; } set { _isEmpty = value; } }
    private float _yPositionWhileHiding = -5.6f;

    public void Hide(Transform child)
    {
        child.GetComponent<Rigidbody2D>().gravityScale = 0;
        child.position = new Vector3(transform.position.x, _yPositionWhileHiding, transform.position.z);
        _isEmpty = false;
    }
}
