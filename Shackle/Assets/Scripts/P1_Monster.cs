using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class P1_Monster : NetworkBehaviour {

	private bool attacking = false;
	private Vector3 initialPosition;
	public float yDistance = 1.2f;
	public float zDistance = 4f;

	private float timer = 0;

	// Use this for initialization
	void Start () {
		initialPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//Flashlight counter
		if(NetworkServer.active && GameObject.Find ("flashlight").activeSelf == true){
			timer += Time.deltaTime;
			if(timer > 7) Attack ();
		} else {
			timer = 0;
		}

		//Attack sequence
		if(attacking){
			//Enter room
			if(initialPosition.y - transform.position.y < yDistance){
				transform.Translate(Vector3.up * Time.deltaTime);
			} 
			//Approach player
			else if(initialPosition.z - transform.position.z < zDistance){
				transform.Translate(Vector3.forward * Time.deltaTime);
			}
		}
	}

	void Attack(){
		attacking = true;
	}

}
