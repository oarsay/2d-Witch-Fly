using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool IsHookEmpty { get; private set; }
    private void Start()
    {
        IsHookEmpty = true;
    }    
    public void OnHuntedAChild()
    {
        IsHookEmpty = false;
    }
    public void OnChildFall()
    {
        IsHookEmpty = true;
    }
}
