using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronDeepPoint : MonoBehaviour
{
    [SerializeField] private GameEvent _gameEventOnChildDeep;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Child") && collision.gameObject.transform.GetComponent<ChildManager>().state == ChildState.Fall)
        {
            _gameEventOnChildDeep.TriggerEvent();
        }
    }
}
