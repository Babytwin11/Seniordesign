using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace S3
{
    public  class LoginAuth : MonoBehaviour
    {

        public InputField user;
        public InputField password;
        public InputField email;
        public Text message;
        public Text UserName;
        public string lobbyScene = "Menu";
        LoginWithPlayFabRequest loginRequest;
        public bool IsAuthenticated = false;
        public Button login;
     
        // Use this for initialization

        void Start()
        {
            email.gameObject.SetActive(false);
        }
    
        // Update is called once per frame
        void Update()
        {

        }
        public void Login()
        {
            loginRequest = new LoginWithPlayFabRequest();
            loginRequest.Username = user.text;
            loginRequest.Password = password.text;
            DontDestroyOnLoad(this);
     
            PlayFabClientAPI.LoginWithPlayFab(loginRequest, result =>
            {

                Debug.Log(user.text);
                //if the account is found
                IsAuthenticated = true;
                
                if (IsAuthenticated == true)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene( "MainMenu");

                    Appdata.Playername = user.text;
                    
                 
                    NetworkManager_custom ha = GetComponent<NetworkManager_custom>();
                    ha.ActivatePanel("PanelMainMenu");
     


                }

                message.text = " Welcome " + user.text + ", Connecting...";
            }, error =>
            {
                // if the account is not found
                IsAuthenticated = false;
                email.gameObject.SetActive(true);
                message.text = " User Not Found/ Regester if you are New!";
                email.gameObject.SetActive(true);
                login.gameObject.SetActive(false);

                Debug.Log(error.ErrorMessage);
                Debug.Log("no");
            }, null);

        }
        public void login2(string name)
        {
            UserName.text = user.text;
            name = user.text;
            Debug.Log(name);
        }
        public void Register()
        {

            RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest();
            request.Email = email.text;
            request.Username = user.text;
            request.Password = password.text;

            PlayFabClientAPI.RegisterPlayFabUser(request, result =>
            {
                message.text = user.text + " Your Accout has been created";
                email.gameObject.SetActive(false);
            }, error =>
            {
                email.gameObject.SetActive(true);
                message.text = " Failed To create account" + error.ErrorMessage + "   ";

            });


        } public static class Appdata
    {
            public static void ha()
            {

            }
            public static string Playername;
    }
    }
   
}