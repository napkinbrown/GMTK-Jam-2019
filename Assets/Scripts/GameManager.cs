using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private GameObject[] carManagers;
    public int score;
    public Text scoreText;
    public Text objectiveText;
    public Text holyShitText;
    //public Animation holyShitAnimator;

    public bool bombExploded;

    public int numLeft;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        carManagers = GameObject.FindGameObjectsWithTag("CarManager");
        score = 0;
        numLeft = 10;
        bombExploded = false;
        //holyShitAnimator.Play();
        holyShitText.color = new Color(holyShitText.color.r, holyShitText.color.g, holyShitText.color.b, 0);
        objectiveText.text = "" + numLeft-- + " buildings left";
    }

    // Update is called once per frame
    void Update()
    { 
        scoreText.text = score.ToString();

    }

    public void BuildingDestroyed() {
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
        //yield return new WaitForSeconds(3);
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
