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
	//public GameObject player;
	public GameObject plugged;
	public GameManager gameMgr;
		
	public Sprite remoteSprite;
	public Sprite keySprite;
	public Sprite screwdriverSprite;

	void Awake () {
		invRemote = GameObject.Find ("remote_icon");
		invRemote.SetActive (false);
		invKey = GameObject.Find ("key_icon");
		invKey.SetActive (false);
		invScrewdriver = GameObject.Find ("screwdriver_icon");
		invScrewdriver.SetActive (false);

		plugged = GameObject.Find ("outlet_plugged");
		plugged.SetActive (false);
		gameMgr = GameObject.Find ("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		//If players are not dead, let reticle cast for interaction
		//if(NetworkServer.active) 
		ray = Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth/2, Camera.main.pixelHeight/2, 0));

		if(Input.GetKeyDown(KeyCode.D)){
			//transform.GetChild (0).GetComponent<Image>().sprite = screwdriverSprite;
		}

		//Reticle interaction options
		if(Physics.Raycast(ray, out hit)){
			Debug.Log(hit.collider.name);
			//Change reticle color when object is interactable
			if(hit.collider.tag == "Interactable"){
				GetComponent<Image>().color = Color.green;
				transform.GetChild (0).GetComponent<Image>().color = Color.green;
			} else {
				GetComponent<Image>().color = Color.white;
				transform.GetChild (0).GetComponent<Image>().color = Color.white;
			}

			//Specific object actions
			if(Input.GetMouseButtonDown(0)){
				//If the player click's on the door, open around parent pivot
				if(hit.collider.name == "door"){
					hit.transform.parent.Rotate(Vector3.up, 90);
				}
				if(hit.collider.name == "wall-light"){
					hit.transform.gameObject.GetComponentInChildren<Light>().enabled = !hit.transform.gameObject.GetComponentInChildren<Light>().enabled;
					//Behaviour h = (Behaviour)hit.transform.GetChild (0).GetComponent("Halo");
					//h.enabled = !h.enabled;
					//hit.transform.GetChild(0).GetComponent(Halo).enabled = !hit.transform.gameObject.GetComponentInChildren<Halo>().enabled;
				}
				if(hit.collider.name == "TV"){
					//if(GameObject
					hit.transform.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = !hit.transform.gameObject.GetComponentInChildren<SpriteRenderer>().enabled;
				}
				if(hit.collider.name == "outlet_unplugged"){
					hit.transform.gameObject.SetActive(false);
					plugged.SetActive (true);
				}

				//If click on key, destroy world instance and add to inventory
				if(hit.collider.name == "remote" || gameMgr.holdingRemote){
					Debug.Log("Got a remote!");
					gameMgr.setMgrPickup(hit.collider.name, true);
					invRemote.SetActive (true);
					Destroy(hit.transform.gameObject);
				}
				if(hit.collider.name == "key" || gameMgr.holdingKey){
					Debug.Log("Got a key!");
					gameMgr.setMgrPickup(hit.collider.name, true);
					invKey.SetActive (true);
					Destroy(hit.transform.gameObject);
				}
				if(hit.collider.name == "screwdriver" || gameMgr.holdingScrewdriver){
					Debug.Log("Got a screwdriver!");
					gameMgr.setMgrPickup(hit.collider.name, true);
					invScrewdriver.SetActive (true);
					Destroy(hit.transform.gameObject);
				}
			}
		}
	}
}
