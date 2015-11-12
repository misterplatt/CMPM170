using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class reticle : NetworkBehaviour {

	Ray ray;
	RaycastHit hit;
	
	public GameObject invRemote;
	public GameObject invKey;
	public GameObject invScrewdriver;
	public GameObject player;
	public inventory inventoryManager;

	void Awake () {
		invRemote = GameObject.Find ("remote_icon");
		invRemote.SetActive (false);
		invKey = GameObject.Find ("key_icon");
		invKey.SetActive (false);
		invScrewdriver = GameObject.Find ("screwdriver_icon");
		invScrewdriver.SetActive (false);
		inventoryManager = GameObject.Find ("Inventory").GetComponent<inventory> ();
	}
	
	// Update is called once per frame
	void Update () {
		//If players are not dead, let reticle cast for interaction
		if(NetworkServer.active) ray = Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth/2, Camera.main.pixelHeight/2, 0));

		//Reticle interaction options
		if(Physics.Raycast(ray, out hit)){
			//Debug.Log(hit.collider.name);
			//Change reticle color when object is interactable
			if(hit.collider.tag == "Interactable"){
				this.GetComponent<Image>().color = Color.green;
			} else {
				this.GetComponent<Image>().color = Color.white;
			}

			//Specific object actions
			if(Input.GetMouseButtonDown(0)){
				//If the player click's on the door, open around parent pivot
				if(hit.collider.name == "door"){
					hit.transform.parent.Rotate(Vector3.up, 90);
				}

				//If click on key, destroy world instance and add to inventory
				if(hit.collider.name == "remote"){
					Debug.Log("Got a remote!");
					inventoryManager.addItem(hit.collider.name);
					invRemote.SetActive (true);
					Destroy(hit.transform.gameObject);
				}
				if(hit.collider.name == "screwdriver"){
					Debug.Log("Got a screwdriver!");
					inventoryManager.addItem(hit.collider.name);
					invScrewdriver.SetActive (true);
					Destroy(hit.transform.gameObject);
				}
				if(hit.collider.name == "key"){
					Debug.Log("Got a key!");
					inventoryManager.addItem(hit.collider.name);
					invKey.SetActive (true);
					Destroy(hit.transform.gameObject);
				}
				
			}
		}
	}
}
