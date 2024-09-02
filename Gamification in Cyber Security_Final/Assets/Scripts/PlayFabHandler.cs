using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class PlayFabHandler: MonoBehaviour
{
    public static PlayFabHandler Instance;
    public GameData gameData;

    public bool isLoggedIn;
    public bool EmailLoggedIn;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        isLoggedIn = false;  
    }

    private void Start()
    {
       

        if(PlayerPrefs.HasKey("email") &&PlayerPrefs.HasKey("password"))
        {
            SignInWithEmail(PlayerPrefs.GetString("email"),PlayerPrefs.GetString("password"));
        }
        else
        {
          //  LoginAsGuest();
        }
       
    }
    #region Guest Login
    public void LoginAsGuest()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Logged in successfully as guest!");
        gameData.PlayerName = "GuestUser_" + UnityEngine.Random.Range(1000,100000).ToString();
        
        GetUserData();
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogError("Error logging in: " + error.GenerateErrorReport());
    }
    #endregion
    #region Sign In
    public void SignInWithEmail(string email, string password)
    {
        var loginRequest = new LoginWithEmailAddressRequest
        {
            Email = email,
            Password = password
        };

        PlayFabClientAPI.LoginWithEmailAddress(loginRequest, OnEmailLoginSuccess, error => OnEmailLoginFailure(error, email, password));
    }

    private void OnEmailLoginSuccess(LoginResult result)
    {
        EmailLoggedIn = true;
        if (SceneManager.GetActiveScene().name == GameManager.Instance.menuSceneName)
        {
            MainMenuHandler.Instance.LoggedIn();
        }
        Debug.Log("Logged in successfully with email!");
       
        GetUserData();
    }
    private void OnEmailLoginFailure(PlayFabError error, string email, string password)
    {
        PlayerPrefs.DeleteKey("email");
        PlayerPrefs.DeleteKey("password");
        PlayerPrefs.Save();
        if (error.Error == PlayFabErrorCode.AccountNotFound)
        {
            Debug.Log("Account not found, proceeding with registration.");
            
        }
        else
        {
            Debug.LogError("Error logging in with email: " + error.GenerateErrorReport());
            Debug.LogError("Error logging in with email: " + error.Error);
           
        }
        if (SceneManager.GetActiveScene().name == GameManager.Instance.menuSceneName)
        {
            MainMenuHandler.Instance.errorSignInTxt.text = error.Error.ToString();
        }
    }
    #endregion
    #region Sign Up


    public void RegisterWithEmail(string email, string password,string userName)
    {
        
            var request = new RegisterPlayFabUserRequest
            {
                Email = email,
                Password = password,
                RequireBothUsernameAndEmail = false,
                Username = userName,
                DisplayName = userName
            };

            PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailure);
       
    
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        EmailLoggedIn = true;
        if (SceneManager.GetActiveScene().name == GameManager.Instance.menuSceneName)
        {
            MainMenuHandler.Instance.LoggedIn();
        }
        UpdateDisplayName(result.Username);
       
        Debug.Log("Registered and logged in successfully!");
       // LinkGuestAccountToEmail(result.PlayFabId, result.Username);
    }

    private void OnRegisterFailure(PlayFabError error)
    {
        PlayerPrefs.DeleteKey("email");
        PlayerPrefs.DeleteKey("password");
        PlayerPrefs.Save();
        Debug.LogError("Error registering: " + error.GenerateErrorReport());
        if (SceneManager.GetActiveScene().name == GameManager.Instance.menuSceneName)
        {
            MainMenuHandler.Instance.errorSignUpTxt.text=error.Error.ToString();
        }
    }
    #endregion
    
    

    #region User Data
    public void SaveUserData()
    {
        string jsonData = JsonConvert.SerializeObject(gameData);

        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { "GameData", jsonData }
            }
        };

        PlayFabClientAPI.UpdateUserData(request, OnDataSendSuccess, OnDataSendFailure);
    }

    private void OnDataSendSuccess(UpdateUserDataResult result)
    {
        Debug.Log("User data updated successfully!");
    }

    private void OnDataSendFailure(PlayFabError error)
    {
        Debug.LogError("Error updating user data: " + error.GenerateErrorReport());
    }

    public void GetUserData()
    {
        var request = new GetUserDataRequest();

        PlayFabClientAPI.GetUserData(request, OnDataReceived, OnDataReceiveFailure);
    }

    private void OnDataReceived(GetUserDataResult result)
    {
        if (result.Data != null && result.Data.ContainsKey("GameData"))
        {
            string jsonData = result.Data["GameData"].Value;
            GameData gData = JsonConvert.DeserializeObject<GameData>(jsonData);

            gameData.PlayerName=gData.PlayerName;
            gameData.PlayerSurname = gData.PlayerSurname;
            gameData.PlayerEmail = gData.PlayerEmail;
            gameData.PlayerAgeRange = gData.PlayerAgeRange;
            gameData.PlayerNationality = gData.PlayerNationality;
            gameData.PlayerUsername = gData.PlayerUsername;
            gameData.levelCompleted = gData.levelCompleted;
            gameData.levelPercentage = gData.levelPercentage;
            Debug.Log("User data retrieved successfully: " + jsonData);
        }
        else
        {
            Debug.Log("No user data found, initializing with default values.");

            
        }
        GetAccountInfo();
        isLoggedIn = true;
        
    }

    private void OnDataReceiveFailure(PlayFabError error)
    {
        Debug.LogError("Error retrieving user data: " + error.GenerateErrorReport());
    }

    #endregion

    #region Display Name
    public void UpdateDisplayName(string displayName)
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = displayName
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdateSuccess, OnDisplayNameUpdateFailure);
    }

    private void OnDisplayNameUpdateSuccess(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Display name updated successfully: " + result.DisplayName);
        gameData.PlayerName = result.DisplayName;
        if (SceneManager.GetActiveScene().name == GameManager.Instance.menuSceneName)
        {
            MainMenuHandler.Instance.DisplayNameUpdated();
        }
        SaveUserData();
    }

    private void OnDisplayNameUpdateFailure(PlayFabError error)
    {
        Debug.LogError("Error updating display name: " + error.GenerateErrorReport());
    }
    #endregion

    #region Account Info

    private void GetAccountInfo()
    {
        var request = new GetAccountInfoRequest();

        PlayFabClientAPI.GetAccountInfo(request, OnGetAccountInfoSuccess, OnGetAccountInfoFailure);
    }

    private void OnGetAccountInfoSuccess(GetAccountInfoResult result)
    {
        string displayName = result.AccountInfo.TitleInfo.DisplayName;
        gameData.PlayerName = displayName;
        Debug.Log("Display name: " + displayName);
        if (SceneManager.GetActiveScene().name == GameManager.Instance.menuSceneName)
        {
            MainMenuHandler.Instance.DisplayNameUpdated();
        }
        SaveUserData();
    }

    private void OnGetAccountInfoFailure(PlayFabError error)
    {
        Debug.LogError("Error retrieving account info: " + error.GenerateErrorReport());
    }
    #endregion


}
