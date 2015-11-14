using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GameManager : NetworkBehaviour {
	
	[SyncVar] public bool playersDead = false;
	[SyncVar] public int flashlightCount = 0;

	[SyncVar] public bool holdingRemote = false;
	[SyncVar] public bool holdingKey = false;
	[SyncVar] public bool holdingScrewdriver= false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (flashlightCount);
	}

	//INVENTORY SYNC
	public void setMgrPickup(string name, bool held){
		if(name == "remote"){
			holdingRemote = true;
		} else if (name == "key"){
			holdingKey = true;
		} else if (name == "screwdriver"){
			holdingScrewdriver= true;
		}
		CmdUpdateItems(name, held);
	}
	
	[Command]
	void CmdUpdateItems(string name, bool held){
		if(name == "remote"){
			holdingRemote = true;
		} else if (name == "key"){
			holdingKey = true;
		} else if (name == "screwdriver"){
			holdingScrewdriver= true;
		}
	}

	//FLASHLIGHT SYNC
	public void IncrementFlashlights(){
		flashlightCount = flashlightCount + 1;
		CmdUpdateFlashlights(flashlightCount);
	}
	public void DecrementFlashlights(){
		flashlightCount = flashlightCount - 1;
		CmdUpdateFlashlights(flashlightCount);
	}

	[Command]
	void CmdUpdateFlashlights(int count){
		flashlightCount = count;
	}

	//DEATH SYNC
	public void setMgrDeath(bool state){
		playersDead = state;
		CmdUpdateDeathState(state);
	}

	[Command]
	void CmdUpdateDeathState(bool state){
		playersDead = state;
	}
	
}