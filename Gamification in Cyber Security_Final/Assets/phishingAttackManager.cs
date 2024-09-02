using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;

public class phishingAttackManager : MonoBehaviour
{
    public GameObject EndPanel;
    // Public TextMeshPro Text Variables
    public TMP_Text emailText1;
    public TMP_Text emailText2;
    public TMP_Text emailText3;
    public TMP_Text emailText4;

    // Input Field
    public TMP_InputField inputField;
    public TMP_Text resultText;

    // Arrays of emails
    string[] originalCertifiedEmails1 = {
        "user@example.com",
        "john.doe@example.com",
        "jane.smith@example.com",
        "mark.johnson@example.com",
        "susan.davis@example.com",
        "robert.green@example.com",
        "emily.white@example.com",
        "michael.brown@example.com",
        "sophia.miller@example.com",
        "david.wilson@example.com"
    };

    string[] originalCertifiedEmails2 = {
        "info@company.com",
        "support@service.com",
        "billing@organization.com",
        "contact@domain.com",
        "admin@website.com",
        "feedback@company.com",
        "sales@shop.com",
        "marketing@business.com",
        "helpdesk@service.com",
        "newsletter@company.com"
    };

    string[] originalCertifiedEmails3 = {
        "ceo@company.com",
        "manager@organization.com",
        "director@company.com",
        "president@company.com",
        "founder@startup.com",
        "owner@business.com",
        "executive@company.com",
        "admin@company.com",
        "operations@company.com",
        "support@company.com"
    };

    string[] phishingEmails = {
        "phishing@example.com",
        "scam@fake.com",
        "fraud@scam.com",
        "hack@steal.com",
        "spam@malware.com",
        "phish@fake.com",
        "virus@trojan.com",
        "spoof@phish.com",
        "identity@theft.com",
        "alert@fraud.com"
    };

    // Variables to store the selected emails
    string[] selectedEmails = new string[4];
    string mainPhishingEmail;

    void Start()
    {
        Time.timeScale = 1;

        // Randomly select emails and assign them to the selectedEmails array
        selectedEmails[0] = originalCertifiedEmails1[Random.Range(0, originalCertifiedEmails1.Length)];
        selectedEmails[1] = originalCertifiedEmails2[Random.Range(0, originalCertifiedEmails2.Length)];
        selectedEmails[2] = originalCertifiedEmails3[Random.Range(0, originalCertifiedEmails3.Length)];
        selectedEmails[3] = phishingEmails[Random.Range(0, phishingEmails.Length)];
        mainPhishingEmail = selectedEmails[3];
        // Shuffle the selectedEmails array
        for (int i = 0; i < selectedEmails.Length; i++)
        {
            string temp = selectedEmails[i];
            int randomIndex = Random.Range(0, selectedEmails.Length);
            selectedEmails[i] = selectedEmails[randomIndex];
            selectedEmails[randomIndex] = temp;
            Debug.Log("Selected email index : " + randomIndex + " Selected Email : " + selectedEmails[randomIndex]);
        }

        // Display the selected emails
        emailText1.text = selectedEmails[0];
        emailText2.text = selectedEmails[1];
        emailText3.text = selectedEmails[2];
        emailText4.text = selectedEmails[3];
    }

    public void OnSubmit()
    {
        int index;
        if (int.TryParse(inputField.text, out index) && index >= 1 && index <= 4)
        {
            // Check if the selected index corresponds to a phishing email
            /*Debug.Log("Phishing Email : " + selectedEmails[3]);
            Debug.Log("Selected Email : " + selectedEmails[index-1]);*/
            Debug.Log("Selected Email : " + selectedEmails[index - 1]);
            if (selectedEmails[index - 1] == mainPhishingEmail)
            {
                GameManager.Instance.gameData.levelCompleted[0]= true;
                GameManager.Instance.gameData.levelPercentage[0]= 100;
                resultText.text = "The email is a phishing attack.";
            }
            else
            {
                resultText.text = "The email is certified.";
            }
        }
        else
        {
            resultText.text = "Invalid number. Please enter a number between 1 and 4.";
        }

        EndPanel.SetActive(true);
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
