using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GameManager : NetworkBehaviour {
	
	[SyncVar] public bool playersDead = false;
	[SyncVar] public int flashlightCount = 0;

	// Use this for initialization
	void Start () {
		NetworkServer.SpawnObjects();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (flashlightCount);
		gameObject.SetActive(true);
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