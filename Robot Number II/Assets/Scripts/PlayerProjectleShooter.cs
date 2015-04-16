using UnityEngine;
using System.Collections;

public class PlayerProjectleShooter : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = player.transform.position;

		if (Input.GetButtonDown ("Fire1")) {
			GameObject proj = (GameObject) Instantiate(Resources.Load("Projectile"));
			proj.transform.position = this.transform.position;
		}
	}
}
