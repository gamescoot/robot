using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, ICharacter {

	//floats
	public float maxSpeed = 3;
	public float speed = 50f;
	public float jumpPower = 200f;
	public float climbPower = 1;

	//booleans
	public bool grounded;
	public bool canDoubleJump;
	public bool canClimb;

	public int direction = 1;

	//refrences
	private Rigidbody2D rb2d;
	private Animator anim;
	private ProjSpawner projspawner;

	//Game values
	private float health =100.0f;
	

	// Use this for initialization
	void Start () {

		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		anim = gameObject.GetComponent<Animator> ();
		this.projspawner = gameObject.GetComponentInChildren<ProjSpawner> ();
	}
	
	// Update is called once per frame
	//public Transform testProj;
	void Update () {
	
		anim.SetBool ("Grounded", grounded);
		anim.SetFloat ("Speed", Mathf.Abs (rb2d.velocity.x));

		if (health <= 0.0) {
			Die ();
		}

		UpdateSprite ();
		if (Input.GetButtonDown ("Jump")){
			Jump();		
		}
		if (Input.GetButtonDown ("Fire1")) {
			this.projspawner.ShootProjOne();
		}
		if (Input.GetButtonDown ("Fire2")) {
			this.projspawner.ShootProjTwo();
		}
	}

	// do all physics in here
	void FixedUpdate()
	{
		Vector3 easeVelocity = rb2d.velocity;
		easeVelocity.y = rb2d.velocity.y;
		easeVelocity.z = 0.0f;
		easeVelocity.x *= 0.80f;
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Ground"), canClimb && ((Input.GetAxis("Vertical") > 0.1f) || (Input.GetAxis("Vertical") < -0.1f)) );
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

		float v = Input.GetAxis ("Vertical");
		Climb ();

		if (!canClimb) {
				rb2d.gravityScale = 1;
		}
	}


	void Jump(){
		if (grounded){
			rb2d.AddForce(Vector2.up * jumpPower);	
			canDoubleJump = true;
		}else{
			
			if (canDoubleJump){
				canDoubleJump = false;
				rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
				rb2d.AddForce(Vector2.up * jumpPower * Input.GetAxis("Horizontal"));
			}			
		}
	}

	void Climb(){
		if (canClimb) {
			rb2d.gravityScale = 0;
			rb2d.velocity = new Vector2 (0, 5 * Input.GetAxis ("Vertical"));
		}
	}

	void UpdateSprite(){
		if (Input.GetAxis ("Horizontal") < -0.1f) {
			direction = -1;
			transform.localScale = new Vector3(-1,1,1);
		}
		if (Input.GetAxis ("Horizontal") > 0.1f) {
			transform.localScale = new Vector3(1,1,1);
			direction = 1;
		}
	}
	void Respawn(){
		Application.LoadLevel(Application.loadedLevel);
	}

	public void Die(){
		Application.LoadLevel(Application.loadedLevel);
	}

	public void ApplyDamage(float damage){
		this.health = this.health - damage;
	}

	public void SetCanClimb(bool canClmb){
		this.canClimb = canClmb;
	}

	public void SetGrounded(bool grd){
		this.grounded = grd;

	}

	public int GetDirection(){
		return this.direction;
	}

	public string GetTag(){
		return this.tag.ToString();
	}

	public Vector3 GetPosition(){
		return this.transform.position;
	}

	public bool GetCanClimb(){
		return this.canClimb;
	}

}

























