using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheckpoint : MonoBehaviour {

    public List<Transform> GetSpawnLocationsAtCheckpoint()
    {
        List<Transform> spawns = new List<Transform>();
        GetComponentsInChildren<Transform>(spawns);
        spawns.RemoveAt(0); // Getting rid of the Checkpoint's own transform
        return spawns;
    }

}
