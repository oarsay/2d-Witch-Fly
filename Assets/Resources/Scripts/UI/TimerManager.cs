using System.Collections;
using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private GameEvent _gameEventOnTimeOut;

    [Header("Component")]
    [SerializeField] private TextMeshProUGUI _timerText;

    [Header("Timer Settings")]
    [SerializeField] private float _currentTime;
    [SerializeField] private float _timerLimit;

    private readonly float _extraTimePerChild = 15f;
    private readonly WaitForSeconds _timeUpdateDelay = new(1f);
    private void Start()
    {
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

    public void AddExtraTimeForChild()
    {
        _currentTime += _extraTimePerChild;
        UpdateTimerUI();
    }
}
