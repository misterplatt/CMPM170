using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class NetworkManager_UI : NetworkManager {

	//Function for hosting a lobby
	public void StartupHost () {
		SetPort();
		NetworkManager.singleton.StartHost ();
	}

	//Function for joining a lobby
	public void JoinGame(){
		SetIPAddress();
		SetPort ();
		NetworkManager.singleton.StartClient();
	}

	//Set IP address based on what user entered in the text field
	void SetIPAddress(){
		string ip = GameObject.Find ("IP_InputField").transform.FindChild("Text").GetComponent<Text>().text;
		NetworkManager.singleton.networkAddress = ip;
	}

	//Set the port to 7777 by default
	void SetPort(){
		NetworkManager.singleton.networkPort = 7777;
	}

	void OnLevelWasLoaded(int level){
		if(level == 0) Invoke("SetupOfflineButtons", 0.3f);
		else Invoke("SetupOnlineButtons", 0.3f);
	}

	//Reactivates the onClick listeners when returning to menu
	void SetupOfflineButtons(){
		GameObject.Find ("HostButton").GetComponent<Button>().onClick.RemoveAllListeners();
		GameObject.Find ("HostButton").GetComponent<Button>().onClick.AddListener(StartupHost);

		GameObject.Find ("JoinButton").GetComponent<Button>().onClick.RemoveAllListeners();
		GameObject.Find ("JoinButton").GetComponent<Button>().onClick.AddListener(JoinGame);
	}

	//Reactivates the onClick listeners when returning to menu
	void SetupOnlineButtons(){
		GameObject.Find ("HostButton").GetComponent<Button>().onClick.RemoveAllListeners();
		GameObject.Find ("HostButton").GetComponent<Button>().onClick.AddListener(StartupHost);
	}

}
