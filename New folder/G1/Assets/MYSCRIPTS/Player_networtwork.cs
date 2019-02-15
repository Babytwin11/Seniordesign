using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;


namespace S3
{
    public class Player_networtwork : NetworkBehaviour
    {

        public GameObject firstPersonCharacter;
        public GameObject[] characterModel;
        public GameObject canvasWorld;

        public override void OnStartLocalPlayer()
        {
            if (isLocalPlayer)
            {

            GetComponent<FirstPersonController>().enabled = true;
                firstPersonCharacter.SetActive(true);

            }
          
           

            foreach (GameObject go in characterModel)
            {
                go.SetActive(false);
            }

            canvasWorld.SetActive(false);
        }
    }
}

