﻿using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour 
{
	public static MenuManager menuSystem;

	public Menu currentMenu;
	private Menu[] menus;

	public void Start()
	{
		menuSystem = this;

		menus = FindObjectsOfType<Menu> ();

		showMenu(currentMenu);
	}

	public Menu getMenu(string menuName)
	{
		foreach (Menu menu in menus) {
			if (menu.gameObject.name == menuName)
				return menu;
		}
		return null;
	}

	public Menu[] getMenus()
	{
		return menus;
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
