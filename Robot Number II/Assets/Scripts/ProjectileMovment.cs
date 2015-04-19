using UnityEngine;
using System.Collections;

public class ProjectileMovment : MonoBehaviour {
	public float speed;
	private float time = 0.0f;
	private Vector3 direction;
	private ICharacter character;
	private int characterDirection;
	private int damage = 10;
	private string characterTag;
	// Use this for initialization

	void Start () {





		this.characterDirection = character.GetDirection();
		this.characterTag = this.character.GetTag ();
		if (this.characterDirection > 0.0f) {
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

		if (other.tag.CompareTo("Ground") ==0) {
			Destroy (gameObject);
		}

		//if (other.tag != "Ground") {
		//	Debug.Log("words");
		//}
		//if (other.tag == "Ground") {
		//	other.SendMessage("ApplyDamage", damage);
		//}

		if (other.tag.CompareTo (this.characterTag) != 0 && other.tag.CompareTo ("Ground") != 0 && other.tag.CompareTo ("Projectile") != 0 && other.tag.CompareTo ("Objective") != 0 && other.tag.CompareTo ("DeathBox") != 0) {
			other.SendMessage ("ApplyDamage", damage);
			Destroy (gameObject);
		} 


	}

	public void SetCharacter(ICharacter character){
		this.character = character;
	}

}
