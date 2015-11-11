using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class Player_Death : NetworkBehaviour {

	private Image reticleImage;
	public static bool dead = false;

	// Use this for initialization
	void Start () {
		reticleImage = GameObject.Find ("reticle").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void enterDeathState(){
		dead = true;
		GetComponent<CharacterController>().enabled = false;
		GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;

	}
}
