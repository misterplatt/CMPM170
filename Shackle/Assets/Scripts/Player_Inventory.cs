﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class Player_Inventory : NetworkBehaviour {

	[SyncVar] public bool holdingRemote = true;
	[SyncVar] public bool holdingKey = true;
	[SyncVar] public bool holdingScrewdriver = true;

	[SyncVar] public bool cablePlugged = false;

	public Sprite currentReticle;

	public Sprite plusSprite;
	public Sprite remoteSprite;
	public Sprite keySprite;
	public Sprite screwdriverSprite;

	public GameManager gameMgr;
	public GameManager_References game_Refs;

	// Use this for initialization
	void Start () {
		gameMgr = GameObject.Find ("GameManager").GetComponent<GameManager>();
		game_Refs = GameObject.Find("GameManager").GetComponent<GameManager_References>();
		currentReticle = GameObject.Find("active").GetComponent<Image>().sprite;
	}
	
	// Update is called once per frame
	void Update () {
		//ALL JANK

		//If reticle has picked up an object, update sync var
		if(reticle.remoteHeld){
			holdingRemote = true;
			if(isLocalPlayer) CmdTellServerItems("remote");
		} 
		if(reticle.keyHeld){
			holdingKey = true;
			if(isLocalPlayer) CmdTellServerItems("key");
		} 
		if(reticle.screwdriverHeld){
			holdingScrewdriver = true;
			if(isLocalPlayer) CmdTellServerItems("screwdriver");
		}

		//Tell server plug has been placed
		if(reticle.pluggedIn){
			cablePlugged = true;
			if(isLocalPlayer) CmdTellServerPlugged();
		}
		//Update other player
		if(cablePlugged){
			reticle.pluggedIn = true;
		}


		//If SyncVar is true, display inventory item for both players
		if(holdingRemote) game_Refs.invRemote.SetActive (true);
		if(holdingKey) game_Refs.invKey.SetActive (true);
		if(holdingScrewdriver) game_Refs.invScrewdriver.SetActive (true);

		//RETICLE CYCLING
		if(Input.GetKeyDown (KeyCode.D)){
			//FROM PLUS
			if(currentReticle == plusSprite){
				if(holdingRemote) currentReticle = remoteSprite;
				else if (holdingKey) currentReticle = keySprite;
				else if (holdingScrewdriver) currentReticle = screwdriverSprite;
			}

			//FROM REMOTE
			else if(currentReticle == remoteSprite){
				if(holdingKey) currentReticle = keySprite;
				else if (holdingScrewdriver) currentReticle = screwdriverSprite;
				else currentReticle = plusSprite;
			}

			//FROM KEY
			else if(currentReticle == keySprite){
				if(holdingScrewdriver) currentReticle = screwdriverSprite;
				else currentReticle = plusSprite;
			}

			//FROM SCREWDRIVER
			 else if(currentReticle == screwdriverSprite){
				currentReticle = plusSprite;
			}
		}

		if(Input.GetKeyDown (KeyCode.A)){
			Debug.Log ("SWAP IT");
			//FROM PLUS
			if(currentReticle == plusSprite){
				if(holdingScrewdriver) currentReticle = screwdriverSprite;
				else if (holdingKey) currentReticle = keySprite;
				else if (holdingRemote) currentReticle = remoteSprite;
			}
			
			//FROM REMOTE
			else if(currentReticle == remoteSprite){
				currentReticle = plusSprite;
			}
			
			//FROM KEY
			else if(currentReticle == keySprite){
				if(holdingRemote) currentReticle = remoteSprite;
				else currentReticle = plusSprite;
			}
			
			//FROM SCREWDRIVER
			else if(currentReticle == screwdriverSprite){
				if(holdingKey) currentReticle = keySprite;
				else if (holdingRemote) currentReticle = remoteSprite;
				else currentReticle = plusSprite;
			}
		}
		GameObject.Find("active").GetComponent<Image>().sprite = currentReticle;
	}
	
	//Inform the server of whether players are dead or alive, aka Sync it
	[Command]
	void CmdTellServerItems (string name){
		if(name == "remote"){
			holdingRemote = true;
		} 
		if(name == "key"){
			holdingKey = true;
		} 
		if(name == "screwdriver"){
			holdingScrewdriver = true;
		}
	}

	//Inform the server of whether players are dead or alive, aka Sync it
	[Command]
	void CmdRemoveItems (){
		reticle.remoteHeld = false;
		reticle.keyHeld = false;
		reticle.screwdriverHeld = false;
		holdingRemote = false;
		holdingKey = false;
		holdingScrewdriver = false;
	}

	//Inform the server that cable was plugged in
	[Command]
	void CmdTellServerPlugged (){
		cablePlugged = true;
	}
}
