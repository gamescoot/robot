using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestMenu : MonoBehaviour {

	public string testMessage;
	public string[] availableWeapons;
	public string[] weaponData;
	private int[] buttonTracker;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(transform.gameObject);
		testMessage = "Try and find me";
		availableWeapons = new string[] {"Projectile", "Lazer"};
		weaponData = new string[] {availableWeapons [0], availableWeapons [0], availableWeapons [0], availableWeapons [0]};
		buttonTracker = new int[] {0, 0, 0, 0};
	}
	
	public void startGame() {
		Application.LoadLevel ("MainTesting Ground");
	}

	public void pressedWeaponButton(Button button) {
		int buttonID = int.Parse (button.name);
		buttonTracker [int.Parse (button.name)]++;
		string newText = availableWeapons [buttonTracker [buttonID] % availableWeapons.Length];
		weaponData [buttonID] = newText;
		button.GetComponentInChildren<Text> ().text = newText;
	}
}