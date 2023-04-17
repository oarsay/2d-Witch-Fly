using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Transform _scoreRewardImage;
    [SerializeField] private TextMeshProUGUI _gameOverScoreText;
    private readonly string _floatingTextAnimationStateName = "FloatingText";

    [Header("Score Settings")]
    private int _currentScore;
    private readonly int _scoreRewardPerChild = 1;

    private void Awake()
    {
        _currentScore = 0;
        UpdateScoreUI();
    }

    //Called by event listener
    public void UpdateScore()
    {
        _currentScore += _scoreRewardPerChild;
        UpdateScoreUI();
        SpawnFloatingText();
    }

    private void UpdateScoreUI()
    {
        _scoreText.text = _currentScore.ToString();
    }

    private void SpawnFloatingText()
    {
        _scoreRewardImage.GetComponent<Animator>().Play(_floatingTextAnimationStateName);
    }

    //Called by event listener
    public void DisableScore()
    {
        enabled = false;
        _gameOverScoreText.text = _currentScore.ToString();
    }
}
