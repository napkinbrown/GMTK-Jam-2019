using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    public GameObject checkpointManager;
    public GameObject car;

    public float turnDistance;

	// Use this for initialization
	void Start () {
		turnDistance = 1F;
	}

	public void MoveToNextCheckpoint() {
        Debug.Log("CamManager: Getting Next Checkpoint");
        Transform nextCheckpoint = checkpointManager.GetComponent<CheckpointManager>().NextCheckpoint();
    
        Debug.Log("CamManager: Setting Camera Checkpoint");
        car.GetComponent<CarMoveScript>().SetNextPoint(nextCheckpoint);

    }
}
