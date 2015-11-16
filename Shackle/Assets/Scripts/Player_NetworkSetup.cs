using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player_NetworkSetup : NetworkBehaviour {

	public Camera CharacterCam;
	public AudioListener audioListener;

	//Enable certain elements on only the local player
	public override void OnStartLocalPlayer(){
		GameObject.Find("Scene Camera").SetActive(false);
		GameObject.Find ("Title").SetActive(false);
		GetComponent<CharacterController>().enabled = true;
		GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
		CharacterCam.enabled = true;
		audioListener.enabled = true;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	void Update(){
		//Disconnect and show cursor
		if(Input.GetKeyDown(KeyCode.Escape)){
			NetworkManager.singleton.StopHost();
			Cursor.visible = true;
		} 
		//Toggle cursor lock
		if(Input.GetKeyDown(KeyCode.L)){
			if(Cursor.lockState == CursorLockMode.Locked) {
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
			else if(Cursor.lockState == CursorLockMode.None){
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			} 
		}
		Debug.Log (NetworkManager.singleton.matches);
	}

	void OnDisconnectedFromServer(NetworkDisconnection info){
		Cursor.visible = true;
	}

}
