using UnityEngine;
using System.Collections;

public class P1_Monster : MonoBehaviour {

	public GameManager gameMgr;

	private bool attacking = false;
	private Vector3 initialPosition;
	public float yDistance = 1.2f;
	public float zDistance = 4f;

	private float timer = 0;

	// Use this for initialization
	void Awake () {
		initialPosition = transform.position;
		gameMgr = GameObject.Find ("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		//Flashlight counter
		if(gameMgr.flashlightCount > 0){
			timer += Time.deltaTime;
			if(timer > 5) Attack ();
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
