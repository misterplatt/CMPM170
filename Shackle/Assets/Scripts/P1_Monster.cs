using UnityEngine;
using System.Collections;

public class P1_Monster : MonoBehaviour {

	private bool attacking = false;
	private Vector3 initialPosition;
	public float yDistance = 1.2f;
	public float zDistance = 4f;

	// Use this for initialization
	void Start () {
		initialPosition = transform.position;
		Invoke ("Attack", 2f);
	}
	
	// Update is called once per frame
	void Update () {
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
