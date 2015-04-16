using UnityEngine;
using System.Collections;

public class ProjectileMovment : MonoBehaviour {
	public float speed;
	public float time = 0.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		time += Time.deltaTime;
		Vector3 direction;

		if (Input.GetAxis ("Horizontal") < -0.1f) {
			direction = new Vector3(-1, 0, 0);
		}
		if (Input.GetAxis ("Horizontal") > 0.1f) {
			direction = new Vector3(1, 0, 0);
		}



		direction = new Vector3(1, 0, 0);
		transform.Translate (direction*speed*Time.deltaTime,Space.Self);

		if(time > 2)
		{
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter (Collision other){
		Destroy (gameObject);
	}
}
