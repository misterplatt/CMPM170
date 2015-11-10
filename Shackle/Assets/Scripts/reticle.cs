using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class reticle : MonoBehaviour {

	Ray ray;
	RaycastHit hit;

	public GameObject invkey;
	public inventory inventoryManager;

	// Use this for initialization
	void Start () {
		invkey = GameObject.Find ("key_icon");
		invkey.SetActive (false);
		inventoryManager = GameObject.Find ("Inventory").GetComponent<inventory> ();
	}
	
	// Update is called once per frame
	void Update () {
		ray = Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth/2, Camera.main.pixelHeight/2, 0));

		if(Physics.Raycast(ray, out hit)){
			Debug.Log(hit.collider.name);

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
					Debug.Log("OPEN SESAME BITCH");
					hit.transform.parent.Rotate(Vector3.up, 90);
				}

				//If click on key, destroy world instance and add to inventory
				if(hit.collider.name == "key"){
					Debug.Log("Got a key!");
					inventoryManager.addItem(hit.collider.name);
					invkey.SetActive (true);
					Destroy(hit.transform.gameObject);
				}
			}
		}
	}
}
