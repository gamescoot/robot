using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	//floats
	public float maxSpeed = 3;
	public float speed = 50f;
	public float jumpPower = 200f;

	//booleans
	public bool grounded;
	public bool canDoubleJump;


	//refrences
	private Rigidbody2D rb2d;
	private Animator anim;
	

	// Use this for initialization
	void Start () {

		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		anim = gameObject.GetComponent<Animator> ();
	
	}
	
	// Update is called once per frame
	//public Transform testProj;
	void Update () {
	

		anim.SetBool("Grounded", grounded);
		anim.SetFloat ("Speed", Mathf.Abs(rb2d.velocity.x));

		if (Input.GetAxis ("Horizontal") < -0.1f) {
			transform.localScale = new Vector3(-1,1,1);
		}
		if (Input.GetAxis ("Horizontal") > 0.1f) {
			transform.localScale = new Vector3(1,1,1);
		}




		if (Input.GetButtonDown ("Jump"))
		{
			if (grounded){
				rb2d.AddForce(Vector2.up * jumpPower);	
				canDoubleJump = true;
			}else
			{

				if (canDoubleJump)
				{
					canDoubleJump = false;
					rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
					rb2d.AddForce(Vector2.up * jumpPower);
				}
			
			}
				

		}
	}

	// do all physics in here
	void FixedUpdate()
	{
		Vector3 easeVelocity = rb2d.velocity;
		easeVelocity.y = rb2d.velocity.y;
		easeVelocity.z = 0.0f;
		easeVelocity.x *= 0.80f;

		//fake friction
		if (grounded) {
			rb2d.velocity = easeVelocity;
		}


		//moves player
		float h = Input.GetAxis ("Horizontal");
		rb2d.AddForce ((Vector2.right * speed) * h);


		//limits the speed
		if (rb2d.velocity.x > maxSpeed) {
			rb2d.velocity = new Vector2 (maxSpeed, rb2d.velocity.y);
		}

		if (rb2d.velocity.x < -maxSpeed) {
			rb2d.velocity = new Vector2 (-maxSpeed, rb2d.velocity.y);
		}
	}
}

























