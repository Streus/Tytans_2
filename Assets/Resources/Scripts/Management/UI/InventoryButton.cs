using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InventoryButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	private Animator invAnim;

	// Use this for initialization
	void Start () {
		invAnim = transform.parent.GetComponent<Animator>();
	}

	public void OnPointerEnter(PointerEventData eventData){
		invAnim.SetBool("Peek", true);
	}

	public void OnPointerExit(PointerEventData eventData){
		invAnim.SetBool("Peek", false);
	}

	public void openInventory(){
		invAnim.SetBool("IsOpen", !invAnim.GetBool("IsOpen"));
	}
}
