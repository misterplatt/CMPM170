using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player_NetworkSetup : NetworkBehaviour {

	public Camera CharacterCam;
	public AudioListener audioListener;

	// Use this for initialization
	void Start () {
		if (isLocalPlayer) {
			//Enable certain elements on only the local player
			GameObject.Find("Scene Camera").SetActive(false);
			GameObject.Find ("Title").SetActive(false);
			GetComponent<CharacterController>().enabled = true;
			GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
			CharacterCam.enabled = true;
			audioListener.enabled = true;
		}
	}

}
