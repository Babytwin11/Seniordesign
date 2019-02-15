using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.NetworkSystem;
using PlayFab;
using PlayFab.ClientModels;
using System;
namespace S3
{
    public class NetworkManager_custom : NetworkManager
    {
        public Texture2D dimplestexturegreen;
        public Texture2D cynditexturegreen;
        public Texture2D dimplestexturered;
        public Texture2D cynditexturered;
        private string ipAddress;
        private int port = 7777;
        public Text textConnectionInfo;
        public Text ipAddressTextField;
        private Scene currentScene;
        public GameObject[] panelsForUI;
        private MatchInfo hostinfo;
        public Text matchRoomNameText;
        public Transform contentRoomList;
        public GameObject roomButtonPrefab;
        public Text playerNameText;
        private string playerName;
        public Text mapSelectedText;
        private string mapSelected = "Map 1";
        private int characterSelected = 0;
        public Text characterSelectedText;
        public GameObject[] characterPrefabs;
        private short playerControllerID = 0;
        public Text haha;
        private GameObject goo;
        public Texture cynditext;
        public Transform[] redspawnpoints;
        public Transform[] greenspawnpoints;

        public int nextPlayersTeam;
        public Button RedTeam;
        public Button GreenTeam;
        int teamID = 0;
        // SpawnSpot[] spawnSpots;


        // public string TeamId = "red";
        public Text TeamSelectedText;
        bool hasPickedTeam = false;
        //GameObject player = Instantiate(characterPrefabs[id], chosenSpawnPoint.position, chosenSpawnPoint.rotation) as GameObject;

        #region Unity Methods
        void Start()
        {
            // spawnSpots = GameObject.FindObjectsOfType<SpawnSpots>();


        }


        //  public void Update ()
        //{
        //      string yo=  LoginAuth.Appdata.Playername.ToString();
        //    
        //    haha.text = yo.ToString();
        // }

        private void OnEnable()
        {
            RegisterCharacterPrefabs();
            SceneManager.sceneLoaded += OnMySceneLoaded;


        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnMySceneLoaded;
        }

        public override void OnClientDisconnect(NetworkConnection conn)
        {
            Debug.Log("didsdscks");
            base.OnClientDisconnect(conn);
            if (textConnectionInfo.text != null)
            {
                textConnectionInfo.text = " or timed out.";
                ActivatePanel("PanelMainMenu");
            }
        }

        public override void OnClientConnect(NetworkConnection conn)
        {
            Debug.Log("sdsd");
        }
        public override void OnClientSceneChanged(NetworkConnection conn)
        {
            Debug.Log("didsdsdsdscks");
            IntegerMessage msg = new IntegerMessage(characterSelected);
            ClientScene.AddPlayer(conn, playerControllerID, msg);
        }
        public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
        {
            int id = 0;
            if (extraMessageReader != null)
            {
                Debug.Log("we");
                var i = extraMessageReader.ReadMessage<IntegerMessage>();
                id = i.value;
            }
            Debug.Log("disacks");

            // Transform chosenSpawnPoint = NetworkManager.singleton.startPositions[Random.Range(0, NetworkManager.singleton.startPositions.Count)];
            TeamRed spawn = GetComponent<TeamRed>();
            playerhealth spawns = GetComponent<playerhealth>();
            Team tele = GetComponent<Team>();
            if (characterSelected == 0)
            {


                GameObject player = Instantiate(characterPrefabs[id], redspawnpoints[1].position, redspawnpoints[1].rotation) as GameObject;

                NetworkServer.AddPlayerForConnection(conn, player, playerControllerID);

                Debug.Log("t44444try");

                player.GetComponent<Team>().CmdTele(player);





            }
            else
            {
                GameObject player = Instantiate(characterPrefabs[id], greenspawnpoints[0].position, greenspawnpoints[0].rotation) as GameObject;
                NetworkServer.AddPlayerForConnection(conn, player, playerControllerID);

                Debug.Log("destory53");

                player.GetComponent<Team>().CmdTele(player);

                Debug.Log("destory53");
                //Object.Destroy(player);
                spawns.SelectSpawnPoint();

            }


        }
        #endregion

        #region My Methods
        void RegisterCharacterPrefabs()
        {
            foreach (GameObject character in characterPrefabs)
            {
                Debug.Log("e");


                ClientScene.RegisterPrefab(character);
            }
        }

        public void OnClickSelectCharacter(int charNum)
        {
            Debug.Log("charselect");
            characterSelected = charNum;
            characterSelectedText.text = "Character" + charNum + "selected";

            LoginAuth name = GetComponent<LoginAuth>();



        }

        public void OnClickTeamSelect(int teamID)
        {

            Debug.Log("teamseleddct");

            //changecolors(teamString);
            Debug.Log("teamselect");

            foreach (GameObject go in characterPrefabs)
            {
                if (go.name == "Player 1")
                {
                    Debug.Log("haha");


                    //  go.GetComponent<Team>().TeamId = teamID;

                    Debug.Log("hehe");
                    Debug.Log("hehe");
                }
                else if (go.name == "Player 2")
                {
                    //      go.GetComponent<Team>().TeamId = teamID;
                }
                else
                {
                    go.GetComponent<Team>().TeamId = teamID;
                }
            }





            //teamString = GetComponent<Team>().TeamId.ToString();
            // string TeamId = teamString;
            Debug.Log("teamselect2");
            //Acceptcolor(acolor);
            Debug.Log("teamselect4");
        }

        public void changecolors(string color)
        {
            Debug.Log(color);
            Team ha = GetComponent<Team>();
            // ha.CmdSetTeamId(color);
            Debug.Log(color);
            Debug.Log("teamselect3");

            TeamSelectedText.text = "You Selected " + color + " Team!";
            foreach (GameObject go in characterPrefabs)
            {
                if (go.name == "Dimples (3)")
                {
                    Debug.Log("haha");
                    // go.GetComponent<Team>().TeamId = color;
                    Debug.Log("hehe");
                    Debug.Log("hehe");
                }
            }


            if (color == "red" && GameObject.FindWithTag("Player"))
            {
                Debug.Log("hellok");
                Texture texg = (Texture)Resources.Load("Cyndi Green");
            }

            else if (color == "red")
            {
                Debug.Log("reddddd");
                foreach (GameObject go in characterPrefabs)
                {
                    if (go.name == "Dimples (3)")
                    {



                        // goo = GameObject.Find("Assets / myprefabs / Dimples(3).prefab");
                        Debug.Log(go + "this is go");
                        Texture texg = dimplestexturered;
                        Debug.Log(texg + "this is texg");
                        go.GetComponentInChildren<Renderer>().sharedMaterial.mainTexture = texg;
                        Debug.Log(texg + "this is the lAR");

                    }
                    else
                    {

                        Debug.Log(go + "this is go");
                        Texture texg = cynditexturegreen;
                        Debug.Log(texg + "this is texg");
                        go.GetComponentInChildren<Renderer>().sharedMaterial.mainTexture = texg;
                        Debug.Log(texg + "this is the lAR");

                    }

                }


                //gotex = GameObject[0].characterPrefabs;


                Transform playerd = transform.Find("Dimples (3)");



                Debug.Log("uj");

                //dimplesSkin.material.color = Color.red;
            }
            else if (color == "green" && GameObject.FindWithTag("Player"))
            {
                // cyndiSkin.material.color = Color.green;
            }
            else
            {// dimplesSkin.material.color = Color.green;
            }
        }

        void OnMySceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log("teamsesdsdlect");
            SetInitialReferences();
        }

        void SetInitialReferences()
        {
            currentScene = SceneManager.GetActiveScene();

            Debug.Log(currentScene);
            if (currentScene.name == "MainMenu")
            {
                ActivatePanel("PanelMainMenu");


            }
            else
            {
                ActivatePanel("PanelInGame");
                OnClickClearConnectionTextInfo();

            }
        }


        public void ActivatePanel(string panelName)
        {
            foreach (GameObject panelGO in panelsForUI)
            {
                if (panelGO.name.Equals(panelName))
                {
                    panelGO.SetActive(true);
                }
                else
                {
                    panelGO.SetActive(false);
                }
            }
        }

        void GetIPAddress()
        {
            ipAddress = ipAddressTextField.text;
        }

        void SetPort()
        {
            NetworkManager.singleton.networkPort = port;
        }

        void SetIPAddress()
        {
            NetworkManager.singleton.networkAddress = ipAddress;
        }

        public void OnClickClearConnectionTextInfo()
        {
            textConnectionInfo.text = string.Empty;
        }

        public void OnClickStartLANHost()
        {
            SetPort();
            NetworkManager.singleton.StartHost();
        }

        public void OnClickStartServerOnly()
        {
            Debug.Log("serer");
            SetPort();
            NetworkManager.singleton.StartServer();
        }

        public void OnClickJoinLANGame()
        {
            SetPort();
            GetIPAddress();
            SetIPAddress();
            NetworkManager.singleton.StartClient();
        }

        public void OnClickDisconnectFromNetwork()
        {
            NetworkManager.singleton.StopHost();
            NetworkManager.singleton.StopServer();
            NetworkManager.singleton.StopClient();
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");

        }

        public void OnClickExitGame()
        {
            Application.Quit();
        }

        public void OnClickDisableMatchMaker()
        {
            NetworkManager.singleton.StopMatchMaker();
        }

        public void OnClickEnableMatchMaker()
        {
            OnClickDisableMatchMaker();
            SetPort();
            NetworkManager.singleton.StartMatchMaker();

        }
        public void OnClickCreateMatch()
        {
            NetworkManager.singleton.matchMaker.CreateMatch(matchRoomNameText.text, 4, true, "", "", "", 0, 0, OnInternetCreateMatch);

        }
        void OnInternetCreateMatch(bool success, string extendInfo, MatchInfo matchInfo)
        {
            if (success)
            {
                textConnectionInfo.text = "Create Match Succeded";
                hostinfo = matchInfo;
                NetworkServer.Listen(hostinfo, NetworkManager.singleton.matchPort);
                NetworkManager.singleton.StartHost(hostinfo);
            }
            else
            {
                textConnectionInfo.text = "Create Match Failed";
            }
        }
        void ClearContentRoomList()
        {
            foreach (Transform child in contentRoomList)
            {
                Destroy(child.gameObject);
            }
        }
        public void OnClickFindInternetMatch()
        {
            ClearContentRoomList();
            NetworkManager.singleton.matchMaker.ListMatches(0, 10, "", true, 0, 0, OnIntenetMatchList);
        }
        void OnIntenetMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
        {
            if (success)
            {
                if (matches.Count != 0)
                {
                    foreach (MatchInfoSnapshot matchesAvailable in matches)
                    {
                        GameObject rButton = Instantiate(roomButtonPrefab) as GameObject;
                        rButton.GetComponentInChildren<Text>().text = matchesAvailable.name;
                        rButton.GetComponent<Button>().onClick.AddListener(delegate { JoinInternetMatch(matchesAvailable.networkId, "", "", "", 0, 0, OnJoinInternetMatch); });
                        rButton.GetComponent<Button>().onClick.AddListener(delegate { ActivatePanel("PanelAttemptingTooConect"); });
                        rButton.transform.SetParent(contentRoomList, false);
                    }

                }
            }
            else
            {
                textConnectionInfo.text = " Couldn't Connect to Match Maker";
            }
        }

        public void JoinInternetMatch(NetworkID netID, string password, string PubClientAddress, string privClientAddress, int eloScore, int reqDomain, NetworkMatch.DataResponseDelegate<MatchInfo> callback)
        {
            Debug.Log("dicks");
            NetworkManager.singleton.matchMaker.JoinMatch(netID, password, PubClientAddress, privClientAddress, eloScore, reqDomain, OnJoinInternetMatch);

        }

        void OnJoinInternetMatch(bool success, string extendedInfo, MatchInfo matchInfo)
        {
            if (success)
            {
                hostinfo = matchInfo;
                NetworkManager.singleton.StartClient(hostinfo);

            }
            else
            {
                textConnectionInfo.text = "Join Match Failed";
            }





        }

        #endregion
    }
}
