using UnityEngine;
using System.Collections;

public class flashlight : MonoBehaviour {

	public GameObject flash_light;
	bool toggle = false;

	// Use this for initialization
	void Start () {
		flash_light = GameObject.Find ("flashlight");
		flash_light.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		//When the player right-clicks, toggle the state of the flashlight
		if(Input.GetMouseButtonDown(1)){
			toggle = !toggle;
			flash_light.SetActive (toggle);
			Debug.Log (flash_light.activeSelf);
		}
	}
}
