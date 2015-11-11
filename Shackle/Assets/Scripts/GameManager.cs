using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GameManager : NetworkBehaviour {
	
	[SyncVar] public bool dead = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.K)){
			dead = true;
			//CmdTellServerDeathState (true);
		}
	}
	
}