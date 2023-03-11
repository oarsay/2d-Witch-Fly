using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp: MonoBehaviour
{
    protected float effectDuration;
    public abstract void Apply();
}
