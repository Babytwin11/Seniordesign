using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
namespace S3
{
    public class Team : NetworkBehaviour
    {

        public bool haspicked = false;
        [SyncVar]
        public int TeamId;
        public Transform[] redspawnpoints;
        public Transform[] greenspawnpoints;
        void spawn()
        {



        }
        void Start()
        {
            TeamRed red = GetComponent<TeamRed>();
            TeamGreen green = GetComponent<TeamGreen>();
            try
            {
                if (TeamId == 0 && red.red == true || green.green == false)
                {
                    Debug.Log("44sS");

                    CmdredUpdateTeam(TeamId);
                    Debug.Log("44");

                }
                else if (TeamId == 1 && red.red == true)
                {
                    Debug.Log("44");

                    return;
                }
                else
                {
                    Debug.Log("4");
                    CmdGreenUpdateTeam(TeamId);
                    Debug.Log("3");
                }
            }
            catch (NullReferenceException)
            {
                Debug.Log("1");
                CmdGreenUpdateTeam(TeamId);
                Debug.Log("2");
            }




        }

        [Command]
        public void CmdTele(GameObject play)
        {

            RpcTeleport(play);
        }
        [ClientRpc]
        public void RpcTeleport(GameObject player)
        {

            Debug.Log("t44try");
            new WaitForSeconds(5f);
            Debug.Log("ttry");

            player.transform.position = new Vector3(1, 1, 1);

            try
            {
                if (player.GetComponent<TeamRed>().red == true)
                {
                    //Team RedTele = GetComponent<Team>();
                    //  Debug.Log(RedTele.TeamId);
                    Debug.Log("if");
                    if (isLocalPlayer)
                    {

                        Debug.Log("e22lse");
                        player.transform.position = new Vector3(0, 0, 0);
                        Debug.Log(player.GetComponent<TeamRed>().red);

                    }

                }
                else
                {
                    Debug.Log("else");
                    if (isLocalPlayer)
                    {
                        Debug.Log("el22se");
                        player.transform.position = new Vector3(1, 1, 1);
                    }

                }
            }
            catch (NullReferenceException)
            {
                if (isLocalPlayer)
                {
                    Debug.Log("else22");
                    player.transform.position = new Vector3(3, 3, 23);
                }
            }


        }







        [ClientRpc]
        void RpcGetTeam()
        {

        }
        [ClientRpc]
        public void RpcSentTeam(int _teamID)
        {
            Debug.Log("hi");

            TeamId += _teamID;

            haspicked = true;
            CmdredUpdateTeam(TeamId);
            CmdGreenUpdateTeam(TeamId);
        }
        [Command]
        public void CmdredUpdateTeam(int __teamId)
        {
            if (__teamId == 2)
            {
                return;
            }
            else if (__teamId == 1)
            {
                Debug.Log("/i");

                return;
            }
            else if (__teamId != 0)
            {
                Debug.Log("5");

                TeamId = 0;
                Debug.Log("6i");

            }
            if (__teamId == 0)
            {
                Debug.Log("8");

                TeamId = __teamId + 1;

            }

        }
        [Command]
        public void CmdGreenUpdateTeam(int __teamId)
        {

            if (__teamId == 1)
            {
                Debug.Log("10");

                return;
            }
            else if (__teamId == 2)
            {
                Debug.Log("10");

                return;
            }
            else
            {
                Debug.Log("9");
                TeamId = __teamId + 2;
            }


        }
    }


}

