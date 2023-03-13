using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] private TextMeshProUGUI levelText;

    private void Start()
    {
        levelText.text = level.ToString(); // Auto update button visuals with corresponding level numbers
    }
    public void OpenScene()
    {
        PlayerPrefs.SetInt("activeLevel", level);
        SceneChanger.ChangeScene("Level " + level.ToString());
    }
}
