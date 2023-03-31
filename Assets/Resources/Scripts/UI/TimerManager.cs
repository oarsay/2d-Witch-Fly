using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private GameEvent _gameEventOnTimeOut;

    [Header("Component")]
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _timeRewardText;
    private string _floatingTextAnimationStateName = "FloatingText";

    [Header("Timer Settings")]
    [SerializeField] private float _currentTime;
    [SerializeField] private float _timerLimit;

    private readonly float _timeRewardPerChild = 15f;
    public float TimeRewardPerChild { get { return _timeRewardPerChild; } }
    private readonly WaitForSeconds _timeUpdateDelay = new(1f);
    private void Start()
    {
        _timeRewardText.text = "+" + (int)_timeRewardPerChild + " s";
        StartCoroutine(UpdateTimer());
    }

    IEnumerator UpdateTimer()
    {
        while(_currentTime > _timerLimit)
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
        UpdateTimerUI();
        SpawnFloatingText();
    }

    private void SpawnFloatingText()
    {
        _timeRewardText.GetComponent<Animator>().Play(_floatingTextAnimationStateName);
    }
}
