using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player_NetworkID : NetworkBehaviour {

	[SyncVar]
	public string uniquePlayerIdentity;
	private NetworkInstanceId networkPlayerID;

	public override void OnStartLocalPlayer (){
		getNetIdentity ();
		setIdentity ();
	}

	// Update is called once per frame
	void Update (){
		//Rename the player from it's instantiated name ("" check makes sure Identity has been synced)
		if( transform.name == "Player(Clone)" || transform.name == ""){
			setIdentity();
		}
	}

	//retrieve network ID from server, then tell it the new unique one created
	[Client]
	void getNetIdentity (){
		networkPlayerID = GetComponent<NetworkIdentity> ().netId;
		CmdTellServerMyIdentity (createUniqueIdentity ());
	}

	//Set's non-local player's name to the identity synced on the server, sets the local's using the create function
	void setIdentity (){
		if (!isLocalPlayer) {
			transform.name = uniquePlayerIdentity;
		} else {
			transform.name = createUniqueIdentity ();
		}
	}

	//Concatenates the the string "Player " with the networkID to tell the server
	string createUniqueIdentity (){
		string newID = "Player " + networkPlayerID.ToString ();
		return newID;
	}

	//Inform the server of the new identity, aka Sync it
	[Command]
	void CmdTellServerMyIdentity (string ID){
		uniquePlayerIdentity = ID;
	}

}
