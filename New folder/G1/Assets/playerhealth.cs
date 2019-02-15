using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace S3
{
    public class playerhealth : NetworkBehaviour
    {
        //dimples
        public const int maxHealth = 100;
        [SyncVar(hook = "OnChangeHealth")]
        public int currentHealth = maxHealth;
        // public  int currentHealth = 100;
        public RectTransform healthbar;
        public bool destroyOnDeath;
        private NetworkStartPosition[] spawnPoints;
        public GameObject[] Model;

        void Start()
        {
            if (isLocalPlayer)
            {
                spawnPoints = FindObjectsOfType<NetworkStartPosition>();
            }

            OnChangeHealth(currentHealth);
        }

        public void TakeDamage(int amount)
        {
            Debug.Log("cdsd");
            if (!isServer)
            {
                Debug.Log("takeda");
                return;
            }
            Debug.Log("yeyey");

            currentHealth -= amount;
            if (currentHealth <= 0)
            {
                if (destroyOnDeath)
                {
                    currentHealth = maxHealth;
                    Debug.Log("dead");
                    //Destroy(gameObject);

                    RpcDeactivatePlayer();
                }

                else
                {
                    currentHealth = maxHealth;
                    Respawnrt();
                }
            }
        }
        IEnumerator Respawn()
        {

            yield return new WaitForSeconds(5.0f);
            Respawnrt();

        }
        void OnChangeHealth(int health)
        {

            healthbar.sizeDelta = new Vector2(health * 2, healthbar.sizeDelta.y);
        }

        // [ClientRpc]
        void Respawnrt()
        {





            GetComponent<NetworkTransform>().enabled = true;
            GetComponent<NetworkTransformChild>().enabled = true;
            //GetComponent<Deal_Damage>().enabled = true;
            if (isLocalPlayer)
            {
                currentHealth = 100;
                Debug.Log("michelle");
                GetComponent<player_controler>().enabled = true;
                StartCoroutine(MakePlayerModelVisible());
                SelectSpawnPoint();

            }
            else
            {
                Debug.Log("leighane");
                StartCoroutine(MakePlayerModelVisible());




            }




        }
        [ClientRpc]
        void RpcDeactivatePlayer()
        {





            GetComponent<player_controler>().enabled = false;
            GetComponent<NetworkTransform>().enabled = false;
            GetComponent<NetworkTransformChild>().enabled = false;
            // GetComponent<Deal_Damage>().enabled = false;



            foreach (GameObject go in Model)
            {
                go.SetActive(false);
            }



            StartCoroutine(Respawn());

        }
        public override void OnStartClient()
        {
            base.OnStartClient();

        }

        IEnumerator MakePlayerModelVisible()
        {
            Debug.Log("hoes");
            yield return new WaitForSeconds(1.5f);

            foreach (GameObject go in Model)
            {
                go.SetActive(true);
            }


        }
        public void SelectSpawnPoint()
        {
            Team TeamId = GetComponent<Team>();
            NetworkManager_custom spawn = GetComponent<NetworkManager_custom>();
            Transform chosenSpawnPoint = NetworkManager.singleton.startPositions[Random.Range(0, NetworkManager.singleton.startPositions.Count)];
            Debug.Log("destory4");
            if (TeamId.TeamId == 1)
            {
                Debug.Log("destory2");
                transform.position = spawn.redspawnpoints[1].position;
                transform.rotation = spawn.redspawnpoints[1].rotation;
            }
            else
            {
                Debug.Log("destory3");
                transform.position = spawn.greenspawnpoints[0].position;
                transform.rotation = spawn.greenspawnpoints[0].rotation;
            }
        }

    }

}