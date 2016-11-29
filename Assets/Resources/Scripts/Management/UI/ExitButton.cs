using UnityEngine;
using System.Collections;
using UnityEditor.SceneManagement;

public class ExitButton : MonoBehaviour {

	public void exitToMain()
	{
		Destroy (GameManager.player);
		EditorSceneManager.LoadScene ("MainMenu");
	}

	public void exitGame()
	{
		Application.Quit();
	}
}
