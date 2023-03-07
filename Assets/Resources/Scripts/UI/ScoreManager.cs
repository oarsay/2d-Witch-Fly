using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreManager : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private TextMeshProUGUI _timerText;

    [Header("Score Settings")]
    [SerializeField] [Range(MIN_MULTIPLIER, MAX_MULTIPLIER)] private float _multiplier;
    private const float MIN_MULTIPLIER = 1;
    private const float MAX_MULTIPLIER = 5;
    private float _currentScore;
    private readonly float _basePoint = 1;
    private readonly float _extraScorePerChild = 1500;

    private Transform _player;
    private float _playerLowestPosition;
    private float _playerHighestPosition;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
        _playerLowestPosition = _player.GetComponent<PlayerMovement>().ScreenBoundaryVerticalBottom;
        _playerHighestPosition = _player.GetComponent<PlayerMovement>().ScreenBoundaryVerticalUpper;
    }

    private void Update()
    {
        UpdateMultiplier();
        UpdateScore();
        UpdateScoreUI();
    }

    private void UpdateScore()
    {
        _currentScore += (_multiplier * _basePoint);
    }

    private void UpdateMultiplier()
    {
        _multiplier = Remap(_player.position.y, _playerLowestPosition, _playerHighestPosition, MIN_MULTIPLIER, MAX_MULTIPLIER);
    }

    private void UpdateScoreUI()
    {
        _timerText.text = ((int)_currentScore).ToString();
    }

    public void AddExtraScoreForChild()
    {
        _currentScore += _extraScorePerChild;
        UpdateScoreUI();
    }

    private static float Remap(float input, float oldLow, float oldHigh, float newLow, float newHigh)
    {
        // https://forum.unity.com/threads/re-map-a-number-from-one-range-to-another.119437/
        float t = Mathf.InverseLerp(oldLow, oldHigh, input);
        return Mathf.Lerp(newLow, newHigh, t);
    }
}
