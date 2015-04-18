using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour, ICharacter {
	
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
	private float time = 0.0f;
	private float attackTimer = 0.0f;
	private float visionDistance = 3;

	private ICharacter player;
	private float distanceToPlayer;
	private float directionToPlayer;
	private ProjSpawner projspawner;
	
	// Use this for initialization
	void Start () {
		//player =(ICharacter) GameObject.FindGameObjectWithTag("Player");
		player =(ICharacter) FindObjectOfType (typeof(Player));
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		anim = gameObject.GetComponent<Animator> ();
		this.projspawner = gameObject.GetComponentInChildren<ProjSpawner> ();
	}
	

	void Update () {


		if (this.transform.position.x < this.player.GetPosition ().x) {
			this.directionToPlayer = -1;
		} else {
			this.directionToPlayer = 1;
		}

		this.distanceToPlayer = Mathf.Abs( this.transform.position.x - this.player.GetPosition ().x);



		anim.SetBool("Grounded", grounded);
		anim.SetFloat ("Speed", Mathf.Abs(rb2d.velocity.x));

		time += Time.deltaTime;
		attackTimer += Time.deltaTime;

		if (health <= 0.0) {
			Respawn();
		}

		AI ();
		UpdateSprite ();


		
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
		float h = direction;
		rb2d.AddForce ((Vector2.right * speed) * h);
		
		
		//limits the speed
		if (rb2d.velocity.x > maxSpeed) {
			rb2d.velocity = new Vector2 (maxSpeed, rb2d.velocity.y);
		}
		
		if (rb2d.velocity.x < -maxSpeed) {
			rb2d.velocity = new Vector2 (-maxSpeed, rb2d.velocity.y);
		}
	}


	void Respawn(){
		GameObject newEnemy = (GameObject) Instantiate(Resources.Load("Enemy"));
		newEnemy.transform.position = new Vector3(2.4f,1.4f,0.0f);
		Destroy (gameObject);
		
	}

	void UpdateSprite(){
		if (direction < -0.1f) {
			direction = -1;
			transform.localScale = new Vector3(-1,1,1);
		}
		if (direction > 0.1f) {
			transform.localScale = new Vector3(1,1,1);
			direction = 1;
		}
	}

	void AI(){

		if (this.distanceToPlayer > this.visionDistance) {
			if (time > 2) {
				time = 0;
				this.direction = this.direction * -1;
			}
		} else {
			if (this.directionToPlayer == -1.0f) {
				this.direction = 1;
			} else {
				this.direction = -1;
			}
		}
		if (this.GetComponent<Rigidbody2D> ().velocity.x == 0) {
			this.Jump ();
		}

		if (this.distanceToPlayer < this.visionDistance){
			if (attackTimer > .3) {
				attackTimer = 0;
				this.projspawner.ShootProjTwo ();
			}
	}
	}

	public void ApplyDamage(float damage){
		this.health = this.health - damage;
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

	void Jump(){
		if (grounded){
			rb2d.AddForce(Vector2.up * jumpPower);	
			canDoubleJump = true;
		}else{
			
			if (canDoubleJump){
				canDoubleJump = false;
				rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
				rb2d.AddForce(Vector2.up * jumpPower);
			}			
		}
	}
	
	
}