using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    private bool _isEmpty = true;
    public bool IsEmpty { get { return _isEmpty; } set { _isEmpty = value; } }
    private float _centerXPosition;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        ////_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        //if (_spriteRenderer)
        //{
        //    //_centerXPosition = _spriteRenderer.bounds.center.x;
        //}
        //else
        //{
        //    ExceptionHandler.Throw("HidingSpot.cs/Awake/Sprite renderer cannot be found!");
        //}
    }

    public void Hide(Transform child)
    {
        child.GetComponent<Rigidbody2D>().gravityScale = 0;
        child.position = transform.position;
        _isEmpty = false;
    }
}
