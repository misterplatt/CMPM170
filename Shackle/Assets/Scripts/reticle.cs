﻿using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class reticle : NetworkBehaviour {

	Ray ray;
	RaycastHit hit;

	public GameObject winOverlay;

	public GameObject plugged;
	public Player_Inventory inventory;

	public Sprite plusSprite;
	public Sprite remoteSprite;
	public Sprite keySprite;
	public Sprite screwdriverSprite;

	public static bool remoteHeld = false;
	public static bool keyHeld = false;
	public static bool screwdriverHeld = false;

	public static bool pluggedIn = false;

	void Start () {
		winOverlay = GameObject.Find ("Win Overlay");
		plugged = GameObject.Find ("outlet_plugged");
		plugged.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		//If players are not dead, let reticle cast for interaction
		if(Camera.main != null){
			ray = Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth/2, Camera.main.pixelHeight/2, 0));
		}

		//Reticle interaction options
		if(Physics.Raycast(ray, out hit)){
			//Debug.Log(hit.collider.name);
			//Change reticle color when object is interactable
			if(hit.collider.tag == "Interactable" && transform.GetChild(0).GetComponent<Image>().sprite == plusSprite){
				GetComponent<Image>().color = Color.green;
				transform.GetChild (0).GetComponent<Image>().color = Color.green;
			} else if (hit.collider.name == "TV" && transform.GetChild(0).GetComponent<Image>().sprite == remoteSprite){
				GetComponent<Image>().color = Color.green;
				transform.GetChild (0).GetComponent<Image>().color = Color.green;
			} else if (hit.collider.name == "airconditioner" && transform.GetChild(0).GetComponent<Image>().sprite == screwdriverSprite){
				GetComponent<Image>().color = Color.green;
				transform.GetChild (0).GetComponent<Image>().color = Color.green;
			} else if (hit.collider.name == "door" && transform.GetChild(0).GetComponent<Image>().sprite == keySprite){
				GetComponent<Image>().color = Color.green;
				transform.GetChild (0).GetComponent<Image>().color = Color.green;
			} else {
				GetComponent<Image>().color = Color.white;
				transform.GetChild (0).GetComponent<Image>().color = Color.white;
			}


			//Specific object on-click actions
			if(Input.GetMouseButtonDown(0)){
				//If the player clicks on the door, open around parent pivot
				if(hit.collider.name == "door" && transform.GetChild(0).GetComponent<Image>().sprite == keySprite){
					hit.transform.GetChild(2).localRotation = Quaternion.AngleAxis (30, Vector3.forward);
					Behaviour h = (Behaviour)hit.transform.GetChild (3).GetComponent("Halo");
					h.enabled = !h.enabled;
					//WIN STATE
					winOverlay.transform.localPosition = new Vector3(0, 0, 0);
				}
				//If player clicks on the light, toggle light and halo components
				if(hit.collider.name == "wall-light"){
					hit.transform.gameObject.GetComponentInChildren<Light>().enabled = !hit.transform.gameObject.GetComponentInChildren<Light>().enabled;
					Behaviour h = (Behaviour)hit.transform.GetChild (0).GetComponent("Halo");
					h.enabled = !h.enabled;
				}
				//If player clicks on TV with remote selected, turn it on
				if(hit.collider.name == "TV" && transform.GetChild(0).GetComponent<Image>().sprite == remoteSprite && pluggedIn){
					hit.transform.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = !hit.transform.gameObject.GetComponentInChildren<SpriteRenderer>().enabled;
				}
				//If player clicks on AC with key selected, open it and disable collider
				if(hit.collider.name == "airconditioner" && transform.GetChild(0).GetComponent<Image>().sprite == screwdriverSprite){
					//Debug.Log(hit.transform.GetChild(0).name);
					hit.transform.gameObject.GetComponent<BoxCollider>().enabled = false;
					hit.transform.GetChild(0).localRotation = Quaternion.AngleAxis (36, Vector3.up);
				}
				//If player clicks on plug, plug it into the wall
				if(hit.collider.name == "outlet_unplugged"){
					hit.transform.gameObject.SetActive(false);
					plugged.SetActive (true);
					pluggedIn = true;
				}
				//If click on key, destroy world instance and add to inventory
				if(hit.collider.name == "remote"){
					Debug.Log("Got a remote!");
					remoteHeld = true;
					Destroy(hit.transform.gameObject);
				}
				if(hit.collider.name == "key"){
					Debug.Log("Got a key!");
					keyHeld = true;
					Destroy(hit.transform.gameObject);
				}
				if(hit.collider.name == "screwdriver"){
					Debug.Log("Got a screwdriver!");
					screwdriverHeld = true;
					Destroy(hit.transform.gameObject);
				}
			}
		}
	}
}
