using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class tailgateManager : MonoBehaviour
{
    public GameObject winPanel;
    public GameObject employee;
    public GameObject thief;

    private Vector3 thiefStartPos;
    private Vector3 empStartPos;

    public int checker;
    public int Score;
    public int counter;

    public TMP_Text scoreText;

    public bool thiefin;
    public bool employeein;


    private void Awake()
    {
        Time.timeScale = 1;
        thiefStartPos = thief.transform.position;
        empStartPos = employee.transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (counter >= 10)
        {
            Time.timeScale = 0;
            winPanel.SetActive(true);
        }

        scoreText.text = "Earned Points : " + Score.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Thief")
        {
            Debug.Log("Thief is here");
            if (employeein)
            {

                thiefin = true;
                thief = collision.gameObject;
                checker = 1;
                thief.SetActive(false);
                StartCoroutine(checkForBust());

            }
            else
            {
                thiefin = true;
                thief = collision.gameObject;
                checker = 0;
                thief.SetActive(false);

            }
        }
        if (collision.gameObject.tag == "Employee")
        {
            Debug.Log("Employee is here");

            employeein = true;
            employee = collision.gameObject;
            employee.SetActive(false);
        }
    }

    
    public void bustedBtn()
    {
        StopAllCoroutines();
        if (checker == 1 && (employeein && thiefin))
        {
            Score += 1;
            checker = 0;
            thiefin = false;
            employeein = false;
            StartCoroutine(startin());
            counter += 1;
        }
        else
        {
            counter += 1;
            Score += 0;
            checker = 0;
            thiefin = false;
            employeein = false;
            StartCoroutine(startin());
        }
    }
    public void MenuBtn()
    {
        SceneManager.LoadScene(0);
    }
    public void RetryBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void StartGame()
    {
        thief.gameObject.SetActive(true);
        employee.gameObject.SetActive(true);
        StartCoroutine(startin());
    }

    IEnumerator startin()
    {
        employee.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.12f);
        thiefin = false;
        employeein = false;
        thief.GetComponent<Animator>().enabled = false;
        employee.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(3.5f);
        thief.gameObject.SetActive(true);
        thief.GetComponent<Animator>().enabled = true;

    }

    IEnumerator checkForBust()
    {
        yield return new WaitForSeconds(1.5f);
        Debug.Log("not busted");
        counter += 1;

        thiefin = false;
        employeein = false;
        checker = 0;
        Score += 0;
        StartCoroutine(startin());
    }
}
