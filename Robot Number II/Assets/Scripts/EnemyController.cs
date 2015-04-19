using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour, ICharacter {
	
	//floats
	public float maxSpeed = 3;
	public float speed = 50f;
	public float jumpPower = 200f;
	public int numOfJumps = 2;
	
	//booleans
	public bool grounded;
	public bool canDoubleJump;
	public bool groundOnLeft;
	public bool groundOnRight;
	public bool noGroundLeft;
	public bool noGroundRight;
	public bool standingOnPlayer;
	
	public int direction = 1;
	
	//refrences
	private Rigidbody2D rb2d;
	private Animator anim;
	
	//Game values
	private float health =100;
	private float time = 0.0f;
	private float attackTimer = 0.0f;
	public float attackSpeed;
	public float jumpTime;

	public float visionDistance;

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
		anim.SetBool("Grounded", grounded);
		anim.SetFloat ("Speed", Mathf.Abs(rb2d.velocity.x));

		GroundOnLeft ();
		GroundOnRight ();
		NoGroundLeft ();
		NoGroundRight ();
		StandinOnPlayer ();

		time += Time.deltaTime;
		attackTimer += Time.deltaTime;



		if (this.transform.position.x < this.player.GetPosition ().x) {
			this.directionToPlayer = -1;
		} else {
			this.directionToPlayer = 1;
		}

		this.distanceToPlayer = Mathf.Abs( this.transform.position.x - this.player.GetPosition ().x);


		if (health <= 0.0) {
			Die();
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

	void AI(){
		
		if (this.distanceToPlayer > this.visionDistance) {
			if (time > 2) {
				time = 0;
				this.direction = this.direction * -1;
			}
		} else {
			if(!this.standingOnPlayer){
				if (this.directionToPlayer == -1.0f) {
					this.direction = 1;
				} else {
					this.direction = -1;
				}
			}else{
				this.direction = 1;
			}

		}

		if (this.direction < 0 && this.groundOnLeft || this.direction<0 && this.noGroundLeft ) {
			if(this.grounded){
				this.numOfJumps = 1;
			}
			this.Jump ();
		}
		if (this.direction > 0 && this.groundOnRight || this.direction>0 && this.noGroundRight){
			if(this.grounded){
				this.numOfJumps = 1;
			}
			this.Jump ();
		}
		
		if (this.distanceToPlayer < this.visionDistance){
			if (attackTimer > this.attackSpeed) {
				attackTimer = 0;
				this.projspawner.ShootProjTwo ();
			}
		}
	}


	void Respawn(){
		GameObject newEnemy = (GameObject) Instantiate(Resources.Load("Enemy"));
		newEnemy.transform.position = new Vector3(2.4f,1.4f,0.0f);
		Destroy (gameObject);
		
	}

	public void Die(){
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
		if (this.numOfJumps>0 && this.jumpTime < this.time-.3){
			rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
			rb2d.AddForce(Vector2.up * jumpPower);	
			this.numOfJumps= this.numOfJumps-1;
			this.jumpTime= this.time;

			}			
	}



	void GroundOnLeft(){
		Vector3 currentPos = this.transform.position;
		Vector3 endPos = new Vector3 (currentPos.x - 1, currentPos.y-.3f, currentPos.z);
		Debug.DrawLine(currentPos, endPos,Color.green);
		this.groundOnLeft = Physics2D.Linecast (currentPos, endPos, 1 << LayerMask.NameToLayer ("Ground"));
	}

	void GroundOnRight(){
		Vector3 currentPos = this.transform.position;
		Vector3 endPos = new Vector3 (currentPos.x + 1, currentPos.y-.3f, currentPos.z);
		Debug.DrawLine(currentPos, endPos,Color.green);
		this.groundOnRight = Physics2D.Linecast (currentPos, endPos, 1 << LayerMask.NameToLayer ("Ground"));
	}


	void NoGroundLeft(){
		Vector3 currentPos = new Vector3(this.transform.position.x-.5f,this.transform.position.y, this.transform.position.z);
		Vector3 endPos = new Vector3 (currentPos.x, currentPos.y-3f, currentPos.z);
		Debug.DrawLine(currentPos, endPos,Color.green);
		this.noGroundLeft = !Physics2D.Linecast (currentPos, endPos, 1 << LayerMask.NameToLayer ("Ground"));
	}

	void NoGroundRight(){
		Vector3 currentPos = new Vector3(this.transform.position.x+.5f,this.transform.position.y, this.transform.position.z);
		Vector3 endPos = new Vector3 (currentPos.x, currentPos.y-3f, currentPos.z);
		Debug.DrawLine(currentPos, endPos,Color.green);
		this.noGroundRight = !Physics2D.Linecast (currentPos, endPos, 1 << LayerMask.NameToLayer ("Ground"));
	}

	void StandinOnPlayer(){
		Vector3 currentPos = this.transform.position;
		Vector3 endPos = new Vector3 (currentPos.x, currentPos.y-1, currentPos.z);
		Debug.DrawLine(currentPos, endPos,Color.green);
		this.standingOnPlayer = Physics2D.Linecast (currentPos, endPos, 1 << LayerMask.NameToLayer ("Player"));
	}



















	
	
}