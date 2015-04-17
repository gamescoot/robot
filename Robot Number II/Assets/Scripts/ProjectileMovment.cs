using UnityEngine;
using System.Collections;

public class ProjectileMovment : MonoBehaviour {
	public float speed;
	private float time = 0.0f;
	private Vector3 direction;
	private Player player;
	private int damage = 10;
	private string shooterTag;
	// Use this for initialization

	void Start () {




		player = (Player)FindObjectOfType (typeof(Player));


		if (player.direction > 0) {
			direction = new Vector3 (1, 0, 0);
		} else {
			direction = new Vector3(-1, 0, 0);
		}

	}
	
	// Update is called once per frame
	void Update () {

		time += Time.deltaTime;


		transform.Translate (direction * speed * Time.deltaTime);//,Space.Self);

		if(time > 2)
		{
			Destroy(gameObject);
		}
	}
	
	void OnTriggerEnter2D (Collider2D other){

		if (other.tag == "Enemy") {
			other.SendMessage("ApplyDamage", damage);
			Destroy (gameObject);
		}

		if(other.tag != "Player"){

			Destroy (gameObject);
		}


	}

}
