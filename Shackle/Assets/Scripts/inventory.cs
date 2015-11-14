using UnityEngine;
using System.Collections;

public class inventory : MonoBehaviour {

	public string[] items = new string[3];


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void addItem (string itemName){
		for(int i = 0; i < 3; i++) {
			Debug.Log ("Check: " + items[i]);
			if(items[i] == ""){
				items[i] = itemName;
				Debug.Log ("Added " + itemName);
				break;
			}
		}
	}
}
