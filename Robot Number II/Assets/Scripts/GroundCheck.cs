using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour {

	private ICharacter parent;

	void Start()
	{
		parent = gameObject.GetComponentInParent<ICharacter> ();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Ground" || col.tag == "Player") {
			parent.SetGrounded (true);
		}
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.tag == "Ground" || col.tag == "Player") {
			parent.SetGrounded (true);
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.tag == "Ground" || col.tag == "Player") {
			parent.SetGrounded (false);
		}
	}
}
