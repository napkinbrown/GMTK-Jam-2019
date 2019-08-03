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


    public void OnExplode() {
        Debug.Log("Expldode@!!!");
        if (explode)
            carParts.GetComponent<CarPartsScript>().disableKinematics();
        Debug.Log("Expldode@!!! Done");
    }
}
