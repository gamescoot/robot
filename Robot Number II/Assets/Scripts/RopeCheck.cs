using UnityEngine;
using System.Collections;

public class RopeCheck : MonoBehaviour {

	private ICharacter parent;

	void Start()
	{
		parent = gameObject.GetComponentInParent<ICharacter> ();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Rope") {
			parent.SetCanClimb (true);
		} 
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.tag == "Rope") {
			parent.SetCanClimb (true);
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.tag == "Rope") {
			parent.SetCanClimb (false);
		}
	}
}
