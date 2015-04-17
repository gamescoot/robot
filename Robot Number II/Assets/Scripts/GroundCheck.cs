using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour {

	private Player parent;

	void Start()
	{
		parent = gameObject.GetComponentInParent<Player> ();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		parent.grounded = true;
	}

	void OnTriggerStay2D(Collider2D col)
	{
		parent.grounded = true;
	}

	void OnTriggerExit2D(Collider2D col)
	{
		parent.grounded = false;
	}
}
