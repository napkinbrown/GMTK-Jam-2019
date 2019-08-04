using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    public GameObject checkpointManager;
    private GameManager gameManager;
    //public GameObject gmObject;
    public GameObject car;
    public GameObject carParts;

    public float turnDistance;
    public bool explode;

	// Use this for initialization
	void Start () {
		turnDistance = 1F;
        GameObject gmObject = GameObject.FindGameObjectWithTag("GameManager");
        GameObject carParts = GameObject.FindGameObjectWithTag("Car Parts");
        if (gmObject != null)
            gameManager = gmObject.GetComponent<GameManager>();
        else
            Debug.Log("Could not find game manager!");

	}

	public void MoveToNextCheckpoint() {
        Debug.Log("CamManager: Getting Next Checkpoint");
        Transform nextCheckpoint = checkpointManager.GetComponent<CheckpointManager>().NextCheckpoint();
    
        Debug.Log("CamManager: Setting Camera Checkpoint");
        //if (!explode)
            car.GetComponent<CarMoveScript>().SetNextPoint(nextCheckpoint);

    }

    void Update() {
        if (gameManager.bombExploded)
            OnExplode();
    }

    public void OnExplode() {
        //Debug.Log("Expldode@!!!");
        GameObject bomb = GameObject.FindGameObjectWithTag("Bomb");
        float dist = Vector3.Distance(bomb.transform.position, transform.GetChild(0).position);
        //Debug.Log("distance from bomb: " + dist);
        if (explode && dist < 50)
            carParts.GetComponent<CarPartsScript>().disableKinematics();
        car.GetComponent<CarMoveScript>().exploded = true;
        //Debug.Log("Expldode@!!! Done");
    }
}
