using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool IsHookEmpty { get; private set; }
    public bool IsInvisible { get; set; }
    private void Start()
    {
        IsHookEmpty = true;
        IsInvisible = false;
    }
    public void OnHuntedAChild()
    {
        IsHookEmpty = false;
        AudioManager.Instance.PlayLaughSound();
    }
    public void OnChildFall()
    {
        IsHookEmpty = true;
    }
}
