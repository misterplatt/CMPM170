using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class flashlight : NetworkBehaviour {
	
	public GameObject flash_light;
	public GameManager gameMgr;

	void Awake (){
		flash_light = GameObject.Find ("flashlight");
		flash_light.SetActive (false);
		GameObject.Find ("GameManager").SetActive(true);
		gameMgr = GameObject.Find ("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		//When the player is alive and right-clicks, toggle the state of the flashlight
		if(Input.GetMouseButtonDown(1) && !gameMgr.playersDead){
			flash_light.SetActive(!flash_light.activeSelf);
			if(flash_light.activeSelf == true)gameMgr.IncrementFlashlights();
			else if (flash_light.activeSelf == false)gameMgr.DecrementFlashlights();
			//if(isLocalPlayer) CmdTellServerDeathState (true);
		}

		//Disable flashlight on death
		if(gameMgr.playersDead){
			flash_light.SetActive (false);
		}
	}
}
