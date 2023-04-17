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
    [SerializeField] private int _currentTime;
    [SerializeField] private int _timerMinValue;
    [SerializeField] private int _timerMaxValue;

    private readonly int _timeRewardPerChild = 15;
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
        StartCoroutine(CountingAnimation(_currentTime + _timeRewardPerChild));
        if (_currentTime > _timerMaxValue) _currentTime = _timerMaxValue;
        SpawnFloatingText();
    }

    private void SpawnFloatingText()
    {
        _timeRewardImage.GetComponent<Animator>().Play(_floatingTextAnimationStateName);
    }

    IEnumerator CountingAnimation(int target)
    {
        if (target > _timerMaxValue) target = _timerMaxValue;

        while(_currentTime < target)
        {
            _currentTime = (int)Mathf.MoveTowards(_currentTime, target, 1);
            UpdateTimerUI();
            yield return null;
        }
    }
}