using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;

public class passwordComplexityManager : MonoBehaviour
{
    public GameObject winPanel;

    public bool passwordLength;
    public bool hasUpperCase;
    public bool hasSpecialChars;
    public bool hasLets;
    public bool hasNums;

    public TMP_InputField pass1Text;
    public TMP_InputField pass2Text;
    public TMP_InputField pass3Text;

    public TMP_Text pass1Score;
    public TMP_Text pass2Score;
    public TMP_Text pass3Score;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool IsStringLengthTwelve(string input)
    {
        return input.Length >= 12;
    }

    public bool HasUppercaseLetters(string input)
    {
        return input.Any(char.IsUpper);
    }

    public bool HasSpecialCharacters(string input)
    {
        return input.Any(ch => !char.IsLetterOrDigit(ch));
    }
    public bool HasLetters(string input)
    {
        return input.Any(char.IsLetter);
    }

    public bool HasNumbers(string input)
    {
        return input.Any(char.IsDigit);
    }


    public void SubmitBtn()
    {
        /*Debug.Log("Pass 1 has 12 chars : " + IsStringLengthTwelve(pass1Text.text));
        Debug.Log("Pass 1 has Uppercase chars : " + HasUppercaseLetters(pass1Text.text));
        Debug.Log("Pass 1 has special chars : " + HasSpecialCharacters(pass1Text.text));
        Debug.Log("Pass 1 has letters : " + HasLetters(pass1Text.text));
        Debug.Log("Pass 1 has Numbers : " + HasNumbers(pass1Text.text));*/
        if (IsStringLengthTwelve(pass1Text.text) && HasUppercaseLetters(pass1Text.text) && HasSpecialCharacters(pass1Text.text) && HasLetters(pass1Text.text) && HasNumbers(pass1Text.text))
        {
            pass1Score.text = "Password 1 is perfect.";
        }
        if (IsStringLengthTwelve(pass1Text.text) && HasUppercaseLetters(pass1Text.text) && !HasSpecialCharacters(pass1Text.text) && HasLetters(pass1Text.text) && HasNumbers(pass1Text.text))
        {
            pass1Score.text = "Password 1 is strong.";
        }
        if (IsStringLengthTwelve(pass1Text.text) && !HasUppercaseLetters(pass1Text.text) && !HasSpecialCharacters(pass1Text.text) && HasLetters(pass1Text.text) && HasNumbers(pass1Text.text))
        {
            pass1Score.text = "Password 1 is average.";
        }
        if (!IsStringLengthTwelve(pass1Text.text) && !HasUppercaseLetters(pass1Text.text) && !HasSpecialCharacters(pass1Text.text) && HasLetters(pass1Text.text) && HasNumbers(pass1Text.text))
        {
            pass1Score.text = "Password 1 is average.";
        }
        if (!IsStringLengthTwelve(pass1Text.text) && !HasUppercaseLetters(pass1Text.text) && !HasSpecialCharacters(pass1Text.text) && !HasLetters(pass1Text.text) && HasNumbers(pass1Text.text))
        {
            pass1Score.text = "Password 1 is simple.";
        }
        if (!IsStringLengthTwelve(pass1Text.text) && !HasUppercaseLetters(pass1Text.text) && !HasSpecialCharacters(pass1Text.text) && HasLetters(pass1Text.text) && !HasNumbers(pass1Text.text))
        {
            pass1Score.text = "Password 1 is weak.";
        }
    }

    public void SubmitBtn2()
    {

        if (IsStringLengthTwelve(pass2Text.text) && HasUppercaseLetters(pass2Text.text) && HasSpecialCharacters(pass2Text.text) && HasLetters(pass2Text.text) && HasNumbers(pass2Text.text))
        {
            pass2Score.text = "Password 2 is perfect.";
        }
        if (IsStringLengthTwelve(pass2Text.text) && HasUppercaseLetters(pass2Text.text) && !HasSpecialCharacters(pass2Text.text) && HasLetters(pass2Text.text) && HasNumbers(pass2Text.text))
        {
            pass2Score.text = "Password 2 is strong.";
        }
        if (IsStringLengthTwelve(pass2Text.text) && !HasUppercaseLetters(pass2Text.text) && !HasSpecialCharacters(pass2Text.text) && HasLetters(pass2Text.text) && HasNumbers(pass2Text.text))
        {
            pass2Score.text = "Password 2 is average.";
        }
        if (!IsStringLengthTwelve(pass2Text.text) && !HasUppercaseLetters(pass2Text.text) && !HasSpecialCharacters(pass2Text.text) && HasLetters(pass2Text.text) && HasNumbers(pass2Text.text))
        {
            pass2Score.text = "Password 2 is average.";
        }
        if (!IsStringLengthTwelve(pass2Text.text) && !HasUppercaseLetters(pass2Text.text) && !HasSpecialCharacters(pass2Text.text) && !HasLetters(pass2Text.text) && HasNumbers(pass2Text.text))
        {
            pass2Score.text = "Password 2 is simple.";
        }
        if (!IsStringLengthTwelve(pass2Text.text) && !HasUppercaseLetters(pass2Text.text) && !HasSpecialCharacters(pass2Text.text) && HasLetters(pass2Text.text) && !HasNumbers(pass2Text.text))
        {
            pass2Score.text = "Password 2 is weak.";
        }
    }

    public void SubmitBtn3()
    {

        if (IsStringLengthTwelve(pass3Text.text) && HasUppercaseLetters(pass3Text.text) && HasSpecialCharacters(pass3Text.text) && HasLetters(pass3Text.text) && HasNumbers(pass3Text.text))
        {
            pass3Score.text = "Password 3 is perfect.";
        }
        if (IsStringLengthTwelve(pass3Text.text) && HasUppercaseLetters(pass3Text.text) && !HasSpecialCharacters(pass3Text.text) && HasLetters(pass3Text.text) && HasNumbers(pass3Text.text))
        {
            pass3Score.text = "Password 3 is strong.";
        }
        if (IsStringLengthTwelve(pass3Text.text) && !HasUppercaseLetters(pass3Text.text) && !HasSpecialCharacters(pass3Text.text) && HasLetters(pass3Text.text) && HasNumbers(pass3Text.text))
        {
            pass3Score.text = "Password 3 is average.";
        }
        if (!IsStringLengthTwelve(pass3Text.text) && !HasUppercaseLetters(pass3Text.text) && !HasSpecialCharacters(pass3Text.text) && HasLetters(pass3Text.text) && HasNumbers(pass3Text.text))
        {
            pass3Score.text = "Password 3 is average.";
        }
        if (!IsStringLengthTwelve(pass3Text.text) && !HasUppercaseLetters(pass3Text.text) && !HasSpecialCharacters(pass3Text.text) && !HasLetters(pass3Text.text) && HasNumbers(pass3Text.text))
        {
            pass3Score.text = "Password 3 is simple.";
        }
        if (!IsStringLengthTwelve(pass3Text.text) && !HasUppercaseLetters(pass3Text.text) && !HasSpecialCharacters(pass3Text.text) && HasLetters(pass3Text.text) && !HasNumbers(pass3Text.text))
        {
            pass3Score.text = "Password 3 is weak.";
        }

    }

    public void EndGame()
    {
        if(pass3Score.text.Contains("perfect") && pass2Score.text.Contains("perfect") && pass1Score.text.Contains("perfect"))
        {
            GameManager.Instance.gameData.levelCompleted[1] = true;
            GameManager.Instance.gameData.levelPercentage[1] = 100;
        }
        winPanel.SetActive(true);
    }

    public void MenuBtn()
    {
        SceneManager.LoadScene(0);
    }
    public void RetryBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
