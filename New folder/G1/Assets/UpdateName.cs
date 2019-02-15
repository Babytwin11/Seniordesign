using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using PlayFab;
using PlayFab.ClientModels;
namespace S3 { 
public class UpdateName : NetworkBehaviour {
        [SyncVar(hook = "OnStartLocalPlayer")]
        public string username;
        public Text UserName2;
        GameObject yes;
        LoginAuth updatename;
 
        // Use this for initialization
        void Start () {
            OnStartLocalPlayer(username);
		
	}

        // Update is called once per frame
        public  void OnStartLocalPlayer(string User2)
        {
            
            Debug.Log(User2);


            UserName2.text = updatename.ToString();
            Debug.Log(username);
           
          

        }
    }
}