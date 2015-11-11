using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GameManager : NetworkBehaviour {
	
	[SyncVar] public bool playersDead = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void setMgrDeath(bool state){
		playersDead = state;
		CmdUpdateMgr(state);
	}

	[Command]
	void CmdUpdateMgr(bool state){
		playersDead = state;
	}
	
}