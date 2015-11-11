using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class Player_Death : NetworkBehaviour {

	//private Image reticleImage;  (hook = "onDeathStateChange")
	[SyncVar] public bool dead = false;

	public GameObject deathOverlay;
	public reticle reticleScript;
	//private bool aboutToDie =;

	// Use this for initialization
	void Start () {
		deathOverlay = GameObject.Find ("Death Overlay");
		deathOverlay.SetActive(false);

		//reticleImage = GameObject.Find ("reticle").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (transform.name + " " + dead);
		if(Input.GetKey(KeyCode.K) || dead){
			enterDeathState();
		}
	}
	
	void enterDeathState(){
		dead = true;
		deathOverlay.SetActive(true);
		GetComponent<CharacterController>().enabled = false;
		GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
		if(isLocalPlayer) CmdTellServerDeathState (true);
	}

	/*void onDeathStateChange(bool deathState){
		if(deathState == true){
			deathOverlay.SetActive(true);
			GetComponent<CharacterController>().enabled = false;
			GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
		}
	}*/

	//Inform the server of whether players are dead or alive, aka Sync it
	[Command]
	void CmdTellServerDeathState (bool state){
		dead = state;
	}
}
