using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    private float _centerXPosition;
    private bool _isEmpty = true;

    private void Awake()
    {
        if(TryGetComponent<SpriteRenderer>(out var spriteRenderer))
        {
            _centerXPosition = spriteRenderer.bounds.center.x;
        }
        else
        {
            ExceptionHandler.Throw(spriteRenderer);
        }
    }
}
