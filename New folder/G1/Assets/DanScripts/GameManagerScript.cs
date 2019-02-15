using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : Photon.MonoBehaviour {

	public GameObject[] redSpawns;
	//public GameObject[] greenSpawns;
	
	public int state = 0;

	void Connect () {
		PhotonNetwork.ConnectToBestCloudServer ("V1.0");
	}

	void OnJoinedLobby() {
		state = 1;
	}

	void OnPhotonRandomJoinFailed() {
		PhotonNetwork.CreateRoom (null);
	}

	void OnJoinedRoom() {
		state = 2;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI () {
		switch (state) 
		{
			case 0:
				//Starting Screen
				if (GUI.Button(new Rect(10, 10, 100, 30), "Connect")){
					Connect ();
				}
				break;
			case 1:
				//Connected to server
				GUI.Label(new Rect(10, 10, 100, 30), "Connected");
				if (GUI.Button(new Rect(10, 10, 100, 30), "Search")){
					PhotonNetwork.JoinRandomRoom();
				}
				break;
			case 2:
				//champion select
				GUI.Label(new Rect(10, 10, 100, 30), "Select your champion");
				if (GUI.Button(new Rect(70, 10, 100, 30), "Tim")){
					Spawn(0, "Tim");
				}
				break;
			case 3:
				//In game
				break;
		}
	}
	void Spawn(int team, string character){
		state = 3;
		Debug.Log("You are on team..." + team + " and are playing as " + character);
	}
}
