using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    public void MenuButton()
    {
        SceneChanger.ChangeScene(Tags.MENU_SCENE);
    }

    public void RestartButton()
    {
        SceneChanger.ReloadCurrentScene();
    }
}
