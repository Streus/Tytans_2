using UnityEngine;
using System.Collections;
using UnityEditor.SceneManagement;

public class ExitButton : MonoBehaviour {

	public void exitToMain()
	{
		EditorSceneManager.LoadScene ("MainMenu");
	}

	public void exitGame()
	{
		Application.Quit();
	}
}
