using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour {

    List<GameObject> checkpointList;
    GameObject[] checkpoints;
    private int current;

	// Use this for initialization
	void Start () {
        //skip the starting checkpoint
		current = 1;

        //create array of checkpoints from manager's children
        checkpointList = new List<GameObject>();
        foreach (Transform child in transform) checkpointList.Add(child.gameObject);
        checkpoints = checkpointList.ToArray();
	}
	
	public Transform NextCheckpoint() {
        if (current <= checkpoints.Length)
            return checkpoints[current++].transform;
        else return null;
    }
}
