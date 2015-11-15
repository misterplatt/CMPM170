using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class flashlight : NetworkBehaviour {

	[SyncVar] public int flashlightCount = 0;

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
		//Debug.Log (flashlightCount);
		Debug.Log (gameMgr.flashlightCount + " flashlights");
		//When the player is alive and right-clicks, toggle the state of the flashlight
		if(Input.GetMouseButtonDown(1) && !gameMgr.playersDead){
			flash_light.SetActive(!flash_light.activeSelf);
			if(flash_light.activeSelf == true && isLocalPlayer) {
				gameMgr.IncrementFlashlights();
			} else if(isLocalPlayer) {
				gameMgr.DecrementFlashlights();
			}
		}

		//Disable flashlight on death
		if(gameMgr.playersDead){
			flash_light.SetActive (false);
			if(isLocalPlayer) CmdResetFlashlightCount ();
		}
	}

	[Command]
	void CmdUpdateFlashlightCount(int tick){
		flashlightCount = flashlightCount + tick;
	}
	[Command]
	void CmdResetFlashlightCount(){
		flashlightCount = 0;
	}

}
