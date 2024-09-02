using PlayFab;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{
    public static MainMenuHandler Instance;
    public GameData gameData;

    [Header("Panels")]
    public GameObject mainPanel;
    public GameObject leaderBoardPanel;
    public GameObject knowledgePanel;
    public GameObject userPanel;
    public GameObject settingsPanel;
    public GameObject loginPanel;
    public GameObject SignUpPanel;
    public List<GameObject> allPanels;

    [Header("Main Panel")]

    public GameObject[] levelTicks;
    public TextMeshProUGUI[] levelPercentage;
  
   


    [Header("User Page")]
    public TMP_InputField userNameField;
    public TextMeshProUGUI rankTxt;
    public string[] badges;
    public string[] achievements;

    [Header("Settings")]
    public Slider soundSlider;
    public Slider musicSlider;
    public AudioMixer mixer;

 
    [Header("Login Mail")]
    public TextMeshProUGUI errorSignInTxt;
    public TMP_InputField emailSignIn;
    public TMP_InputField passwordSignIn;
 

    [Space]
    public TextMeshProUGUI errorSignUpTxt;
    public TMP_InputField nameSignUp;
    public TMP_InputField surnameSignUp;
    public TMP_InputField emailSignUp;
    public TMP_Dropdown ageRangeSignUp;
    public TMP_Dropdown nationalitySignUp;
    public TMP_InputField usernameSignUp;
    public TMP_InputField passwordSignUp;
    public TMP_InputField passwordConfirmSignUp;
    public Toggle termsAndConditionsSignUp;
    public Toggle newsAndLetterSignUp;
    

    [Header("Game Rooms")]
    public GameObject createRoomPanel;
    public Transform containerRooms;
    public GameObject roomPrefab;
    public GameObject noRoomsObj;
    public GameObject createBtn_Rooms;
    public GameObject joinBtnBtn_Rooms;

    [Header("Join Room")]
    public GameObject joinRoomPanel;
    public TMP_InputField roomNameField;
    public GameObject joinBtn_JoinRoom;
    public TextMeshProUGUI detailTxt_joinRoom;

    [Header("MultiPlayer Waiting")]
    public GameObject multiPlayerWaitingPanel;
    public TextMeshProUGUI detailTxt_MultiPlayerWaiting;
    public GameObject startBtn_MultiPlayerWaiting;
    public GameObject backBtn_MultiPlayerWaiting;




    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        for (int i = 0; i < levelTicks.Length; i++)
        {
            levelTicks[i].SetActive(gameData.levelCompleted[i]);
            levelPercentage[i].text = gameData.levelPercentage[i] + "%";
        }
        // Init();
    }

    public void Init()
    {
    
    
        DisplayNameUpdated();
        
     
        ChangeMusic(GameManager.Instance.music);
        ChangeSound(GameManager.Instance.sound);
        musicSlider.value = GameManager.Instance.music;
        soundSlider.value = GameManager.Instance.sound;
    
    }
    public void OpenAccountsPanel()
    {
        if (PlayFabHandler.Instance.EmailLoggedIn)
        {

            ActivatePanel(userPanel);
        }
        else
        {
          ActivatePanel(loginPanel);
        }
    }
  
 
    public void DisplayNameUpdate()
    {
        if (userNameField.text.Length > 3)
        {
            ClearDetailTxts();
            PlayFabHandler.Instance.UpdateDisplayName(userNameField.text);
        }
    }
    public void DisplayNameUpdated()
    {
        //userNameField.text = GameManager.Instance.gameData.PlayerName;
        //playerNameTxt.text = GameManager.Instance.gameData.PlayerName;
    }
    public void ChangeSound(float val)
    {
        GameManager.Instance.sound = val;
        mixer.SetFloat("SFX", ConvertSoundAndMusicValue(val));
        mixer.SetFloat("UI", ConvertSoundAndMusicValue(val));
    }

    public void ChangeMusic(float val)
    {
        GameManager.Instance.music = val;
        mixer.SetFloat("BGM", ConvertSoundAndMusicValue(val));
    }
    public float ConvertSoundAndMusicValue(float input)
    {
        input = 83 - input;
        input = input * (-1);
        return input;
    }
 
 
 
    public void LoginEmail()
    {
        if (emailSignIn.text.Length < 3)
        {
            errorSignInTxt.text = "Email Length Should be Greater Than 3";
            return;
        }
        else if (passwordSignIn.text.Length < 3)
        {
            errorSignInTxt.text = "Password Length Should be Greater Than 3";
            return;
        }
        ClearDetailTxts();
        PlayerPrefs.SetString("email", emailSignIn.text);
        PlayerPrefs.SetString("password", passwordSignIn.text);
        PlayerPrefs.Save();
        PlayFabHandler.Instance.SignInWithEmail(emailSignIn.text, passwordSignIn.text);

    }
    public void SignUpEmail()
    {
    

        if (nameSignUp.text.Length < 3)
        {
            errorSignUpTxt.text = "Name Length Should be Greater Than 3";
            return;
        }
        if (surnameSignUp.text.Length < 3)
        {
            errorSignUpTxt.text = "Surname Length Should be Greater Than 3";
            return;
        }
        if (emailSignUp.text.Length < 3)
        {
            errorSignUpTxt.text = "Email Length Should be Greater Than 3";
            return;
        }
        if (ageRangeSignUp.value==0)
        {
            errorSignUpTxt.text = "Select Age Range";
            return;
        }
        if (nationalitySignUp.value == 0)
        {
            errorSignUpTxt.text = "Select Nationality";
            return;
        }
        if (usernameSignUp.text.Length < 3)
        {
            errorSignUpTxt.text = "Username Length Should be Greater Than 3";
            return;
        }
       if (passwordSignUp.text.Length < 3)
        {
            errorSignUpTxt.text = "Password Length Should be Greater Than 3";
            return;
        }  
       if (passwordConfirmSignUp.text.Length < 3)
        {
            errorSignUpTxt.text = "Confirm Password Length Should be Greater Than 3";
            return;
        }
        if (passwordConfirmSignUp.text != passwordSignUp.text)
        {
            errorSignUpTxt.text = "Password Does Not Match";
            return;
        }
        if (termsAndConditionsSignUp.isOn==false)
        {
            errorSignUpTxt.text = "Accept Terms And Conditions";
            return;
        }
        ClearDetailTxts();
        PlayerPrefs.SetString("email", emailSignUp.text);
        PlayerPrefs.SetString("password", passwordSignUp.text);
        PlayerPrefs.Save();
        PlayFabHandler.Instance.RegisterWithEmail(emailSignUp.text, passwordSignUp.text, nameSignUp.text);
    }
    public void LoggedIn()
    {
      ActivatePanel(mainPanel);
       // loginBtn.SetActive(false);
       
        ClearDetailTxts();
     
    }

  


    public void Play(int num)
    {
    
        SceneManager.LoadScene(GameManager.Instance.gamePlaySceneName[num]);
    }

    public void ClearDetailTxts()
    {
        //detailTxt_joinRoom.text = "";
        //detailTxt_MultiPlayerWaiting.text = "";
     
        //errorSignInTxt.text = "";
        //errorSignUpTxt.text = "";

    }
    public void ActivatePanel(GameObject panel)
    {
        foreach (GameObject g in allPanels)
        {
            g.SetActive(false);
        }
        allPanels.Find(pred => pred.gameObject == panel).SetActive(true);
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
