using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private GameObject[] carManagers;
    public int score;
    public Text scoreText, timerText;
    public Text objectiveText;
    public Text holyShitText;
    //public Animation holyShitAnimator;

    public bool bombExploded, gameIsOver;

    public int numLeft;
    private float timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        carManagers = GameObject.FindGameObjectsWithTag("CarManager");
        score = 0;
        numLeft = 10;
        timeLeft = 60;
        bombExploded = false;
        gameIsOver = false;
        //holyShitAnimator.Play();
        holyShitText.color = new Color(holyShitText.color.r, holyShitText.color.g, holyShitText.color.b, 0);
        objectiveText.text = "" + numLeft-- + " buildings left";
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
        
        timerText.text = ((int)timeLeft).ToString();

    }

    public void gameOver() {
        gameIsOver = true;
        holyShitText.text = "You lost! George Bush blew up more people than you did, sweaty.";
        StartCoroutine(FadeTextToFullAlpha(2, holyShitText));
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
            objectiveText.text = "Um... wow you're actually a terrorist, congratulations.";
            holyShitText.text = "You won! Everyone's dead and it's all your fault!";
            StartCoroutine(FadeTextToFullAlpha(1, holyShitText));
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
