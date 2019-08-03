using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private GameObject[] carManagers;
    public int score;
    public Text scoreObject;
    public bool bombExploded;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        carManagers = GameObject.FindGameObjectsWithTag("CarManager");
        score = 0;
        bombExploded = false;
    }

    // Update is called once per frame
    void Update()
    { 
        if (scoreObject)
            scoreObject.text = score.ToString();
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
}
