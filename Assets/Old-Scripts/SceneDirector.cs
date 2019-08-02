using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDirector : MonoBehaviour {

    //public CheckpointManager checkpointManager;
    public CameraManager cameraManager;
    public EnemyManager enemyManager;

    // Update is called once per frame
    private void Start()
    {
        cameraManager.MoveToNextCheckpoint();
        enemyManager.spawnEnemiesAtNextCheckpoint();
    }

    // Start

    void Update()
    {
        if (enemyManager.enemiesLeft <= 0) {
            cameraManager.MoveToNextCheckpoint();
            enemyManager.spawnEnemiesAtNextCheckpoint();
        }
    }
    // Camera goes to first camera checkpoint
    // Spawn enemies at first enemy spawnpoint
    // When spawn point tells me that all the enemies are dead, transition
    // Get next camera checkpoint, if the checkpoint has enemies associated with it, spawn enemies

    // If we run out of both 

}
