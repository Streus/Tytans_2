using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class LoadGameElement : MonoBehaviour {

	//private Image header;
	private Text saveGameName;

	void Awake () {
		//header = transform.GetChild(0).GetComponent<Image>();
		saveGameName = transform.GetChild(0).GetChild(0).GetComponent<Text>();

		//header.color = Color.green;
		saveGameName.text = "";
	}

	public void setGameName(string name)
	{
		saveGameName.text = name;
	}
	
	public void loadGame()
	{
		GameManager.manager.setSaveName(saveGameName.text);
		GameManager.manager.loadGame();
		EditorSceneManager.LoadScene ("Overworld");
	}
}
