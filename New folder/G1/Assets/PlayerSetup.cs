using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;



namespace S3
{
    public class PlayerSetup : NetworkBehaviour
    {

        public GameObject Camera;
        public GameObject[] characterModel;
        public GameObject canvasWorld;

        public override void OnStartLocalPlayer()
        {
            if (isLocalPlayer)
            {
                Debug.Log("as");
                Camera.SetActive(true);
                GetComponent<player_controler>().enabled = true;
                Team color = GetComponent<Team>();
            
                //GetComponent<Camera>().enabled = true;
            }



            foreach (GameObject go in characterModel)
            {
                go.SetActive(true);
            }

           // canvasWorld.SetActive(false);
        }
    }
}