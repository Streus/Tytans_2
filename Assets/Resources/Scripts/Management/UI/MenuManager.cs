using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour 
{
	//public static MenuManager menuSystem;

	public Menu currentMenu;

	public void Start()
	{
		//if (menuSystem = null)
		//	menuSystem = this;
		//else
		//	Destroy (gameObject);

		showMenu(currentMenu);
	}

	void OnDestroy () {
		//if (menuSystem == this)
		//	menuSystem = null;
	}

	public void showMenu(Menu menu)
	{
		if(currentMenu != null)
		{
			currentMenu.IsOpen = false;
		}

		currentMenu = menu;
		currentMenu.IsOpen = true;
	}
}
