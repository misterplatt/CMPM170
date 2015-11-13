using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class Player_Death : NetworkBehaviour {

	private Image reticleImage;
	[SyncVar] public bool dead = false;
	private bool once = false;
	private bool monsterHit = false;

	public GameObject deathOverlay;
	public GameManager gameMgr;
	public reticle reticleScript;

	// Use this for initialization
	void Start () {
		deathOverlay = GameObject.Find ("Death Overlay");
		reticleImage = GameObject.Find ("reticle").GetComponent<Image>();
		gameMgr = GameObject.Find ("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if((Input.GetKey(KeyCode.K) || monsterHit || dead) && !once){
			enterDeathState();
			once = true;
		} else if (once && Input.anyKeyDown){
			leaveDeathState();
			once = false;
			monsterHit = false;
		}
	}

	void OnTriggerEnter(Collider col){
		Debug.Log (col.gameObject.name);
		if(col.gameObject.name == "P1 Monster")monsterHit = true;
	}

	//Removes reticle, brings in death overlay, and informs server/game manager of death
	void enterDeathState(){
		dead = true;
		reticleImage.enabled = false;
		deathOverlay.transform.localPosition = new Vector3(0, 0, 0);
		gameMgr.setMgrDeath(true);
		if(isLocalPlayer) CmdTellServerDeathState (true);
	}

	//Opposite of enter death state
	void leaveDeathState(){
		dead = false;
		reticleImage.enabled = true;
		deathOverlay.transform.localPosition = new Vector3(0, 300, 0);
		gameMgr.setMgrDeath(false);
		if(isLocalPlayer) CmdTellServerDeathState (false);
	}

	//Inform the server of whether players are dead or alive, aka Sync it
	[Command]
	void CmdTellServerDeathState (bool state){
		dead = state;
	}
}
