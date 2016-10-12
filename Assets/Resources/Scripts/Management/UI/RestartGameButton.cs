using UnityEngine;
using System.Collections;

public class RestartGameButton : MonoBehaviour {

	public void restartGame(){
		GameManager.manager.restartGame ();
	}
}
