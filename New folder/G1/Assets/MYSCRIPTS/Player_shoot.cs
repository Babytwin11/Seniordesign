using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace S3
{
    public class Player_shoot : NetworkBehaviour
    {

        public GameObject hitEffect;
        public Transform dimples;
        public Collider dimples2;
        public Transform firstPersonCharacter;
        private RaycastHit hit;
        private int damage = 20;
        private int amount = 20;


        // Update is called once per frame
        void Update()
        {
            if (isLocalPlayer && Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }

        void Shoot()
        {
            if (Physics.Raycast(firstPersonCharacter.transform.position, firstPersonCharacter.transform.forward, out hit))
            {
                Quaternion hitAngle = Quaternion.LookRotation(hit.normal);
                CmdSpawnHitPrefab(hit.point, hitAngle);
                Debug.Log("haah");

                if (hit.transform.CompareTag("Player") || hit.collider.CompareTag("Player"))
                {
                    Team mytm = this.GetComponent<Team>();
                    Team tm = hit.transform.GetComponent<Team>();

                    Team dtm = hit.collider.GetComponent<Team>();
                    //|| tm.TeamId != mytm.TeamId
                    if (tm == null || tm.TeamId != mytm.TeamId)
                    {

                        Debug.Log("hahahahah2");
                        CmdApplyDamageOnServer(hit.transform.GetComponent<NetworkIdentity>().netId);
                        CmdApplyDamageOnServer(hit.collider.GetComponent<NetworkIdentity>().netId);
                        Debug.Log("he are here");
                    }


                }
                else if (hit.transform.CompareTag("Dimples"))
                {
                    Team mytm = this.GetComponent<Team>();
                    Team tm = hit.transform.GetComponent<Team>();

                    Team dtm = hit.collider.GetComponent<Team>();
                    //|| tm.TeamId != mytm.TeamId
                    if (tm == null || tm.TeamId != mytm.TeamId)
                    {
                        Debug.Log("hahahahah3");
                        CmdApplyDamage(hit.transform.GetComponent<NetworkIdentity>().netId);
                        CmdApplyDamage(hit.transform.GetComponent<NetworkIdentity>().netId);
                        Debug.Log("we are here");
                    }

                }
            }
        }

        [Command]
        void CmdSpawnHitPrefab(Vector3 pos, Quaternion rot)
        {
            GameObject hitEffectGo = (GameObject)Instantiate(hitEffect, pos, rot);
            NetworkServer.Spawn(hitEffectGo);
        }

        [Command]
        public void CmdApplyDamageOnServer(NetworkInstanceId networkID)
        {//cyborg

            GameObject hitPlayerGo = NetworkServer.FindLocalObject(networkID);
            hitPlayerGo.GetComponent<Player_health>().DeductHealth(damage);

            //dimples
            GameObject hitPlayerGo2 = NetworkServer.FindLocalObject(networkID);
            hitPlayerGo.GetComponent<playerhealth>().TakeDamage(amount);
            Debug.Log("ayyayay");

            Debug.Log(hitPlayerGo2);

        }
        [Command]
        public void CmdApplyDamage(NetworkInstanceId networkID)
        {

            //dimples
            amount = 5;
            GameObject hitPlayerGo2 = NetworkServer.FindLocalObject(networkID);
            hitPlayerGo2.GetComponent<playerhealth>().TakeDamage(amount);
            Debug.Log("ayyayay");

            Debug.Log(hitPlayerGo2);

        }

    }
}