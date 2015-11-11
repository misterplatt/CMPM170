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
		//When the player is alive and right-clicks, toggle the state of the flashlight
		if(Input.GetMouseButtonDown(1)){ // && !gameMgr.dead){
			toggle = !toggle;
			flash_light.SetActive (toggle);
			Debug.Log (flash_light.activeSelf);
		}

		/*Disable flashlight on death
		if(false){
			flash_light.SetActive (false);
		}*/
	}
}
