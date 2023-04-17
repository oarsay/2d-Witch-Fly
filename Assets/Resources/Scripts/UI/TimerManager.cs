using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private GameEvent _gameEventOnTimeOut;

    [Header("Component")]
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private Transform _timeRewardImage;
    private string _floatingTextAnimationStateName = "FloatingText";

    [Header("Timer Settings")]
    [SerializeField] private float _currentTime;
    [SerializeField] private float _timerMinValue;
    [SerializeField] private float _timerMaxValue;

    private readonly float _timeRewardPerChild = 15f;
    public float TimeRewardPerChild { get { return _timeRewardPerChild; } }
    private readonly WaitForSeconds _timeUpdateDelay = new(1f);
    private void Start()
    {
        StartCoroutine(UpdateTimer());
    }

    IEnumerator UpdateTimer()
    {
        while(_currentTime > _timerMinValue)
        {
            UpdateTimerUI();
            yield return _timeUpdateDelay;
            _currentTime--;
        }

        UpdateTimerUI();
        _gameEventOnTimeOut.TriggerEvent();
        enabled = false;
    }

    private void UpdateTimerUI()
    {
        _timerText.text = _currentTime.ToString("0");
    }

    // Called by game event
    public void AddExtraTimeForChild()
    {
        _currentTime += _timeRewardPerChild;
        if (_currentTime > _timerMaxValue) _currentTime = _timerMaxValue;
        UpdateTimerUI();
        SpawnFloatingText();
    }

    private void SpawnFloatingText()
    {
        _timeRewardImage.GetComponent<Animator>().Play(_floatingTextAnimationStateName);
    }
}
