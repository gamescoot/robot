using UnityEngine;
using System.Collections;

public class PlayerProjectleShooter : MonoBehaviour {

	

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown ("Fire1")) {
			GameObject proj = (GameObject) Instantiate(Resources.Load("Projectile"));

			proj.transform.position = this.transform.position;
		}

		if (Input.GetButtonDown ("Fire2")) {
			GameObject proj = (GameObject) Instantiate(Resources.Load("Lazer"));
			
			proj.transform.position = this.transform.position;
		}
	}
}
