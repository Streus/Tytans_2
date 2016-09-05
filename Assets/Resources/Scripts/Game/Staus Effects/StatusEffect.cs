using UnityEngine;
using System.Collections;

public abstract class StatusEffect{
	
	public string name;
	public string desc;
	public Sprite icon;
	public float duration;

	public Transform invoker;
	private ArrayList statusList;
	public Entity invokerVars;

	public StatusEffect(float dur, Transform t)
	{
		name = "DEFAULT";
		desc = "This status effect is not defined.";
		icon = null;
		duration = dur;

		invoker = t;
		invokerVars = invoker.GetComponent<Entity>();
		statusList = invokerVars.statuses;
	}

	public abstract void apply();
	public abstract void revert();

	public void setStatusList(ArrayList al)
	{
		statusList = al;
	}

	private void removeFromList()
	{
		statusList.Remove(this);
	}
}
