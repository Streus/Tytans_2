using UnityEngine;
using System.Collections;

public class Boss : ControlScript
{
	public int bossIndex;

	public DoorControl[] roomDoors;
	protected string[] bulletDrops;
	protected Ability[] abilityDrops;

	public override void Awake()
	{
		base.Awake ();

		if (GameManager.manager.completedBosses [bossIndex])
			Destroy (gameObject);
	}
	public void Start()
	{
		target = GameManager.player;
	}

	// Add a boss health bar to the GUI and lock up the room
	public void OnEnable()
	{
		BossHealthDisplay.pool.createNewHealthBar (self);

		if (roomDoors.Length != 0)
		{
			for (int i = 0; i < roomDoors.Length; i++)
			{
				roomDoors [i].setDoor (false);
			}
		}
	}

	// Drop items
	public virtual void OnDestroy() 
	{
		//remove health bar
		BossHealthDisplay.pool.removeHealthBar (self);

		//open doors
		if (roomDoors.Length != 0)
		{
			for (int i = 0; i < roomDoors.Length; i++)
			{
				roomDoors [i].setDoor (true);
			}
		}

		if (self.health <= 0) {
			GameManager.manager.completedBosses [bossIndex] = true;
			dropItems ();
		}
	}

	// Drop pickups with the values in bulletDrops and abilityDrops
	private void dropItems()
	{
		//bullets
		for (int i = 0; i < bulletDrops.Length; i++)
		{
			GameObject drop = (GameObject)Instantiate (Resources.Load<GameObject> ("Prefabs/World/Interactable/BulletPickUp"), transform.position, Quaternion.identity);
			drop.GetComponent<BulletPickUp> ().assignBullet (bulletDrops [i]);
		}

		//abilities
		for (int i = 0; i < abilityDrops.Length; i++)
		{
			GameObject drop = (GameObject)Instantiate (Resources.Load<GameObject> ("Prefabs/World/Interactable/AbilityPickUp"), transform.position, Quaternion.identity);
			drop.GetComponent<AbilityPickUp> ().assignAbility (abilityDrops [i].Copy ());
		}
	}
}
