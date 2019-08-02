
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public GameObject checkpointManager;
    public GameObject cam;

    public float turnDistance;

	// Use this for initialization
	void Start () {
		turnDistance = 1F;
	}

    public void CameraHit() {
        cam.GetComponent<camMoveScript>().RedFlash();
    }
	
	public void MoveToNextCheckpoint() {
        Debug.Log("CamManager: Getting Next Checkpoint");
        Transform nextCheckpoint = checkpointManager.GetComponent<CheckpointManager>().NextCheckpoint();
    
        Debug.Log("CamManager: Setting Camera Checkpoint");
        cam.GetComponent<camMoveScript>().SetNextPoint(nextCheckpoint);

    }
}

