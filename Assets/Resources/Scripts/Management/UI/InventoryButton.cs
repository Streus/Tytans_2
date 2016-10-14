using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InventoryButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	private Animator invAnim;
	private AbilityList list;

	// Use this for initialization
	void Start () {
		invAnim = transform.parent.GetComponent<Animator>();
		list = transform.parent.GetChild (1).GetChild (0).GetChild (0).GetComponent<AbilityList> ();
	}

	void Update () {
		if (Input.GetKeyDown (Bindings.toggleInventory))
			openInventory ();
	}

	public void OnPointerEnter(PointerEventData eventData){
		invAnim.SetBool("Peek", true);
	}

	public void OnPointerExit(PointerEventData eventData){
		invAnim.SetBool("Peek", false);
	}

	public void openInventory(){
		invAnim.SetBool("IsOpen", !invAnim.GetBool("IsOpen"));
		list.toggleList ();
		Player plyscr = GameManager.player.GetComponent<Player> ();
		plyscr.acceptInput = !plyscr.acceptInput;
	}
}
