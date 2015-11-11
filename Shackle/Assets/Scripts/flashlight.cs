using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class flashlight : NetworkBehaviour {
	
	public GameObject flash_light;
	public GameManager gameMgr;
	bool toggle = false;

	void Awake (){
		flash_light = GameObject.Find ("flashlight");
		flash_light.SetActive (false);
		gameMgr = GameObject.Find ("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		//When the player is alive and right-clicks, toggle the state of the flashlight
		if(Input.GetMouseButtonDown(1) && !gameMgr.playersDead){
			toggle = !toggle;
			flash_light.SetActive (toggle);
			Debug.Log (flash_light.activeSelf);
		}

		//Disable flashlight on death
		if(gameMgr .playersDead){
			flash_light.SetActive (false);
		}
	}
}
