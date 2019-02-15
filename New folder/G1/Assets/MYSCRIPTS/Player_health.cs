using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;


namespace S3
{
    public class Player_health : NetworkBehaviour
    {
        //cyborg girl
        [SyncVar(hook = "UpdateHealthBar")]
        public int playerHealth = 100;
        public GameObject[] characterModel;
        private float timeToRespawn = 5;
        public GameObject defeatText;
        public GameObject healthBarGo;
        public RectTransform healthBarRect;
        public Text healthText;


        // Use this for initialization
        void Start()
        {
            UpdateHealthBar(playerHealth);
        }

        void UpdateHealthBar(int pHealth)
        {
            healthBarRect.sizeDelta = new Vector2(pHealth * 2, healthBarRect.sizeDelta.y);
            healthText.text = pHealth.ToString();
        }


        public void DeductHealth(int damage)
        {
            Debug.Log("cdsdsd");
            if (!isServer)
            {
                return;
            }

            playerHealth -= damage;

            if (playerHealth <= 0)
            {
                playerHealth = 0;
                RpcDeactivatePlayer();
            }
        }


        [ClientRpc]
        void RpcDeactivatePlayer()
        {
            GetComponent<FirstPersonController>().enabled = false;
            GetComponent<Player_shoot>().enabled = false;
            GetComponent<NetworkTransform>().enabled = false;
            GetComponent<CharacterController>().enabled = false;
            healthBarGo.SetActive(false);

            foreach (GameObject go in characterModel)
            {
                go.SetActive(false);
            }

            if (isLocalPlayer)
            {
                defeatText.SetActive(true);
            }

            StartCoroutine(Respawn());

        }

        IEnumerator Respawn()
        {
            yield return new WaitForSeconds(timeToRespawn);
            ReactivatePlayer();
        }

        void ReactivatePlayer()
        {
            playerHealth = 100;
            GetComponent<NetworkTransform>().enabled = true;
            GetComponent<Player_shoot>().enabled = true;
            GetComponent<CharacterController>().enabled = true;

            if (isLocalPlayer)
            {
                GetComponent<FirstPersonController>().enabled = true;
                defeatText.SetActive(false);
                SelectSpawnPoint();
            }

            else
            {
                StartCoroutine(MakePlayerModelVisible());
            }
        }

        void SelectSpawnPoint()
        {
            Transform chosenSpawnPoint = NetworkManager.singleton.startPositions[Random.Range(0, NetworkManager.singleton.startPositions.Count)];
            transform.position = chosenSpawnPoint.position;
            transform.rotation = chosenSpawnPoint.rotation;
        }

        IEnumerator MakePlayerModelVisible()
        {
            yield return new WaitForSeconds(1.5f);

            foreach (GameObject go in characterModel)
            {
                go.SetActive(true);
            }

           healthBarGo.SetActive(true);
        }


    }
}


