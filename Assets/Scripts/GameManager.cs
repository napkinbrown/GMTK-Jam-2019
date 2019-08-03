using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private GameObject carManager;
    public int score;
    public Text scoreObject;
    public bool bombExploded;

    // Start is called before the first frame update
    void Start()
    {
        carManager = GameObject.FindGameObjectWithTag("CarManager");
        //scoreObject = GetComponent<T
        score = 0;
        bombExploded = false;
        //scoreObject = GetComponent<Text>();
        StartCoroutine(MoveCar());
    }

    IEnumerator MoveCar()
    {
        while (!bombExploded) {
            yield return new WaitForSeconds(5);
            carManager.GetComponent<CarManager>().MoveToNextCheckpoint();
        }
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
        GameObject.FindGameObjectWithTag("CarManager").GetComponent<CarManager>().explode = true;
        GameObject.FindGameObjectWithTag("CarManager").GetComponent<CarManager>().OnExplode();
    }
}
