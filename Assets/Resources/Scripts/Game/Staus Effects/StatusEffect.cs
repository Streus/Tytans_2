using UnityEngine;
using System.Collections;

public abstract class StatusEffect{
	public string name;
	public Sprite icon;
	public float duration;

	public StatusEffect()
	{
		name = "DEFAULT";
		icon = null;
		duration = 1f;
	}

	public abstract void apply();
	public abstract void revert();
}
