using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private GameObject[] carManagers;
    public int score;
    public Text scoreText;
    public Text objectiveText;
    public Text holyShitText, UICountdownText, restartText;
    private GameObject timerText;
    //public Animation holyShitAnimator;

    public bool bombExploded, gameIsOver;

    public int numLeft;
    private float timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        timerText = GameObject.FindGameObjectWithTag("Timer");
        carManagers = GameObject.FindGameObjectsWithTag("CarManager");
        score = 0;
        numLeft = 15;
        timeLeft = 60;
        bombExploded = false;
        gameIsOver = false;
        //holyShitAnimator.Play();
        holyShitText.color = new Color(holyShitText.color.r, holyShitText.color.g, holyShitText.color.b, 0);
        restartText.color = new Color(holyShitText.color.r, holyShitText.color.g, holyShitText.color.b, 0);
        objectiveText.text = "" + numLeft-- + " buildings left";
        timerText.GetComponent<TextMesh>().text = "1:00";
        UICountdownText.text = "1:00";
    }

    // Update is called once per frame
    void Update()
    { 
        scoreText.text = score.ToString();

        if (timeLeft > 0 && numLeft > 0)
            timeLeft -= Time.deltaTime;
        else if (timeLeft <= 0 && numLeft > 0) {
            timeLeft = 0;
            gameOver();
        } 

        if (timeLeft < 60) {
            timerText = GameObject.FindGameObjectWithTag("Timer");
            timerText.GetComponent<TextMesh>().text = "0:" + ((int)timeLeft).ToString();
            UICountdownText.text = "0:" + ((int)timeLeft).ToString();
        }

        if (gameIsOver)
            StartCoroutine(restartGame());
    }

    public void gameOver() {
        gameIsOver = true;
        holyShitText.text = "You lost! George Bush blew up more people than you did, sweaty.";
        StartCoroutine(FadeTextToFullAlpha(2, holyShitText));
        StartCoroutine(FadeTextToFullAlpha(2, restartText));
    }

    IEnumerator restartGame() {
        yield return new WaitForSeconds(2);
        if(Input.anyKeyDown)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name,  LoadSceneMode.Single);
    }

    public void BuildingDestroyed() {
        if (gameIsOver)
            return;
        //make sure text resets to invisible so it doesn't get stuck
        if (holyShitText.color.a > 0)
            holyShitText.color = new Color(holyShitText.color.r, holyShitText.color.g, holyShitText.color.b, 0);
        
        if (numLeft > 0) {
            objectiveText.text = "" + numLeft-- + " buildings left";
            StartCoroutine(FadeTextToFullAlpha(1, holyShitText));
        } else {
            gameIsOver = true;
            objectiveText.text = "Um... wow you're actually a terrorist, congratulations.";
            holyShitText.text = "You won! Everyone's dead and it's all your fault!";
            StartCoroutine(FadeTextToFullAlpha(2, holyShitText));
            StartCoroutine(FadeTextToFullAlpha(2, restartText));
        }
    }

    public void onScoreEvent(GameObject gameObject) {
        Debug.Log(gameObject);
    }

    public void onBombExploadEvent()
    {
        bombExploded = true; // Don't know if this is necessary?
        carManagers = GameObject.FindGameObjectsWithTag("CarManager");
        foreach (GameObject carManager in carManagers) {
            Debug.Log("Exploding car manager");
            carManager.GetComponent<CarManager>().explode = true;
            carManager.GetComponent<CarManager>().OnExplode();
        }
    }

    public void onPlayerHitEvent()
    {
        gameOver();
    }

    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
        //The game over text uses t to avoid removing the text display
        if (t == 1)
            StartCoroutine(FadeTextToZeroAlpha(t, i));
    }
 
    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
