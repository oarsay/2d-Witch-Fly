using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronFallPoint : MonoBehaviour
{
    [SerializeField] private GameEvent _gameEventOnChildFall;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.CHILD))
        {
            _gameEventOnChildFall.TriggerEvent();
        }
    }
}
