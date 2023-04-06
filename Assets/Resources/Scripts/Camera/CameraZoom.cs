using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private Camera _mainCamera;
    private void Start()
    {
        _mainCamera = GetComponent<Camera>();
    }

    /// <summary>
    /// Sets camera's projection size to the new value with a smooth transition
    /// </summary>
    /// <param name="targetProjectionSize">The new value for projection size</param>
    /// <param name="transitionDuration">Transition duration in seconds</param>
    public void ZoomEffect(float targetProjectionSize, float transitionDuration)
    {
        float currentProjectionSize = _mainCamera.orthographicSize;
        StartCoroutine(ZoomTransition(currentProjectionSize, targetProjectionSize, transitionDuration));
    }

    IEnumerator ZoomTransition(float startPoint, float endPoint, float transitionDuration)
    {
        float elapsedTime = 0;

        while (elapsedTime < transitionDuration)
        {
            _mainCamera.orthographicSize = Mathf.Lerp(startPoint, endPoint, (elapsedTime / transitionDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Make sure we got there
        _mainCamera.orthographicSize = endPoint;
        yield return null;
    }
}
