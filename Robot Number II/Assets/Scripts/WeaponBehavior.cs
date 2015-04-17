using UnityEngine;
using System.Collections;

public class WeaponBehavior : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector2 playerpos = GameObject.FindGameObjectWithTag("Player").transform.position;
		playerpos.x = playerpos.x + 0.2f;
		//playerpos.y = playerpos.y;



		Vector2 mouse = Camera.main.ScreenToViewportPoint(Input.mousePosition);        //Mouse position
		Vector3 objpos = Camera.main.WorldToViewportPoint (transform.position);        //Object position on screen
		Vector2 relobjpos = new Vector2(objpos.x - 0.5f,objpos.y - 0.5f);            //Set coordinates relative to object
		Vector2 relmousepos = new Vector2 (mouse.x - 0.5f,mouse.y - 0.5f) - relobjpos;
		float angle = Vector2.Angle (Vector2.up, relmousepos);    //Angle calculation
		if (relmousepos.x > 0)
			angle = 360-angle;
		Quaternion quat = Quaternion.identity;
		quat.eulerAngles = new Vector3(0,0,angle + 90); //Changing angle
		transform.rotation = quat;
		transform.position = playerpos;
	
	}
}
