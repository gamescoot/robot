using UnityEngine;
using System.Collections;

public class EnemyGroundCheck : MonoBehaviour {

	private EnemyController parent;
	
	void Start()
	{
		parent = gameObject.GetComponentInParent<EnemyController> ();
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
