using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
	public static void ChangeScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}

	public static void ChangeScene(int sceneIndex)
	{
		SceneManager.LoadScene(sceneIndex);
	}

	public static void ReloadCurrentScene()
	{
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.buildIndex);
	}

	public static void Exit()
	{
		#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
		#endif
		Application.Quit();
	}
}
