using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class LoadGameElement : MonoBehaviour {

	private Text saveGameName;
	private GameObject infoWindow;

	private Image difficultyIndicator;
	private Image classIndicator;

	private GameObject[] bossDefeatedIndicators;

	private bool loaded;

	void Awake () {
		saveGameName = transform.GetChild(0).GetChild(0).GetComponent<Text>();
		infoWindow = transform.GetChild (1).gameObject;
		difficultyIndicator = infoWindow.transform.GetChild (0).GetComponent<Image> ();
		classIndicator = infoWindow.transform.GetChild (0).GetChild (0).GetComponent<Image> ();

		bossDefeatedIndicators = new GameObject[14];
		for (int i = 0; i < 14; i++) {
			bossDefeatedIndicators [i] = infoWindow.transform.GetChild (1).GetChild (i).gameObject;
		}

		infoWindow.SetActive (false);
		saveGameName.text = "";
		loaded = false;
	}

	public void setGameName(string name)
	{
		saveGameName.text = name;
	}

	public void toggleGameInfo()
	{
		//reveal window
		infoWindow.SetActive (!infoWindow.activeSelf);

		//set content size
		RectTransform contentRect = transform.parent.GetComponent<RectTransform> ();
		if (infoWindow.activeSelf) 
		{
			contentRect.sizeDelta = new Vector2 (contentRect.sizeDelta.x, contentRect.sizeDelta.y + 55f);

			if (!loaded) 
			{
				//load data and set GUI element values
				GameManager.manager.setSaveName (saveGameName.text);
				GameManager.manager.loadGame ();

				//difficulty
				switch (GameManager.manager.difficulty) 
				{
				case Difficulty.Easy:
					difficultyIndicator.color = Color.green;
					break;
				case Difficulty.Normal:
					difficultyIndicator.color = Color.yellow;
					break;
				case Difficulty.Hard:
					difficultyIndicator.color = Color.red;
					break;
				}

				//class
				switch (GameManager.manager.playerClass) 
				{
				case PlayerClass.defender:
					classIndicator.sprite = (Sprite)Resources.Load<Sprite> ("Sprites/Game/Entities/PlayerDefender");
					break;
				case PlayerClass.skirmisher:
					classIndicator.sprite = (Sprite)Resources.Load<Sprite> ("Sprites/Game/Entities/PlayerSkirmisher");
					break;
				case PlayerClass.caster:
					classIndicator.sprite = (Sprite)Resources.Load<Sprite> ("Sprites/Game/Entities/PlayerCaster");
					break;
				}

				//boss progress
				bool[] temp = GameManager.manager.completedBosses;
				for (int i = 0; i < 14; i++) 
					bossDefeatedIndicators [i].SetActive (temp [i]);

				loaded = true;
			}
		} 
		else
		{
			contentRect.sizeDelta = new Vector2 (contentRect.sizeDelta.x, contentRect.sizeDelta.y - 55f);
		}
	}
	
	public void loadGame()
	{
		GameManager.manager.setSaveName(saveGameName.text);
		GameManager.manager.loadGame();
		EditorSceneManager.LoadScene ("Overworld");
	}
}
