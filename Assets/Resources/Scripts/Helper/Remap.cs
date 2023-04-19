using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Remap
{
    public static float RemapClamped(float input, float oldLow, float oldHigh, float newLow, float newHigh)
    {
        float t = Mathf.InverseLerp(oldLow, oldHigh, input);
        return Mathf.Lerp(newLow, newHigh, t);
    }
}
