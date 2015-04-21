using UnityEngine;
using System.Collections;

public class ObjectiveHandler : MonoBehaviour {

	public string nextLevel;
	public float rotationSpeed = 1.0f;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		transform.Rotate(0,0,this.rotationSpeed);


	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) {
			int i = Application.loadedLevel;

			if(Application.loadedLevelName == "SecondaryTesting Ground"){
				Application.LoadLevel(i-1);
			}else{
			Application.LoadLevel(i+1);
			}
		}
		

	}
}
