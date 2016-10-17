using UnityEngine;
using System.Collections;

public class DieOnAnimationEnd : MonoBehaviour {

	void Awake () {
		Animator animator = transform.GetComponent<Animator> ();
		Destroy (gameObject, animator.GetCurrentAnimatorClipInfo (0).Length);
	}
}
