using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	//floats
	public float maxSpeed = 3;
	public float speed = 50f;
	public float jumpPower = 200f;
	
	//booleans
	public bool grounded;
	public bool canDoubleJump;
	
	public int direction = 1;

	//refrences
	private Rigidbody2D rb2d;
	private Animator anim;
	
	//Game values
	private float health =100;
	
	
	// Use this for initialization
	void Start () {
		
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		//anim = gameObject.GetComponent<Animator> ();

		//Debug.Log ("hello world");
	}
	
	// Update is called once per frame
	//public Transform testProj;
	void Update () {

		//Debug.Log(health);

		if (health <= 0.0) {
			Destroy (gameObject);
		}
		
		//anim.SetBool("Grounded", grounded);
		//anim.SetFloat ("Speed", Mathf.Abs(rb2d.velocity.x));
		

	}
	
	// do all physics in here
	void FixedUpdate()
	{
	
	}

	void ApplyDamage(float damage){
		this.health = this.health - damage;
	}
	
	
}


