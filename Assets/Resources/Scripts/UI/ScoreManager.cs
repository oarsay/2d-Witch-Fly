using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private TextMeshProUGUI _timerText;

    [Header("Score Settings")]
    [SerializeField] [Range(1, 5)] private int _multiplier;

    private int _currentScore;
    private readonly int _basePoint = 1;

    private readonly int _extraScorePerChild = 1500;

    private void Update()
    {
        _currentScore += (_multiplier * _basePoint);
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        _timerText.text = _currentScore.ToString();
    }

    public void AddExtraScoreForChild()
    {
        _currentScore += _extraScorePerChild;
        UpdateScoreUI();
    }
}
