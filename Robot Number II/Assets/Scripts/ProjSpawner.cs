using UnityEngine;
using System.Collections;

public class ProjSpawner : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	
	public void ShootProjOne(){
		GameObject proj = (GameObject) Instantiate(Resources.Load("Projectile"));
		
		proj.transform.position = this.transform.position;
		proj.SendMessage("SetCharacter", gameObject.GetComponentInParent<ICharacter> ());
		
	}
	
	public void ShootProjTwo(){
		GameObject proj = (GameObject) Instantiate(Resources.Load("Lazer"));
		
		proj.transform.position = this.transform.position;
		proj.SendMessage("SetCharacter", gameObject.GetComponentInParent<ICharacter> ());
	}
}
