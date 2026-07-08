using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuHandler : MonoBehaviour
{
	public void PlayGame(int scene)
	{
		SceneManager.LoadScene(scene);
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
