using UnityEngine;
using TMPro;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private int level;
    public void OpenScene()
    {
        SceneChanger.ChangeScene(level);
    }
}
