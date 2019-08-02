using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    private List<GameObject> enemyCheckpoints;
    private List<GameObject> enemies;

    private List<GameObject> currentEnemies;
    public int enemiesLeft;

    // Use this for initialization
    void Awake() {
    
        for (int i = 0; i < this.transform.childCount; i++)
        {
            GameObject child = this.transform.GetChild(i).gameObject;
            if (child.tag == "Enemies") {
                enemies = getListOfChildren(child);
            }     
            if (child.tag == "EnemyCheckpointList")
                enemyCheckpoints = getListOfChildren(child);
        }

    }

    void Update() {
        foreach (GameObject enemy in currentEnemies)
        {
            if (!enemy.activeInHierarchy)
            {
                currentEnemies.Remove(enemy);
                enemiesLeft--;
            }

            Debug.Log(enemiesLeft);
        }
    }

    private List<GameObject> getListOfChildren(GameObject child) {
        List<GameObject> listOfChildren = new List<GameObject>();
        for (int i = 0; i < child.transform.childCount; i++)
        {
            listOfChildren.Add(child.transform.GetChild(i).gameObject);
        }

        return listOfChildren;
    }

    public GameObject getNextEnemyCheckpoint()
    {
        GameObject checkpoint = enemyCheckpoints[0];
        enemyCheckpoints.RemoveAt(0);
        return checkpoint;
    }

    public List<GameObject> getEnemies(bool includeDeactivated)
    {
        if (includeDeactivated)
            return enemies;

        List<GameObject> activeEnemies = new List<GameObject>();
        foreach (GameObject enemy in enemies)
        {
            if (enemy.activeInHierarchy)
                activeEnemies.Add(enemy);
        }

        return activeEnemies;
    }

    public List<GameObject> getNonActiveEnemies() {
        List<GameObject> nonActiveEnemies = new List<GameObject>();
        foreach (GameObject enemy in enemies)
        {
            if (!enemy.activeInHierarchy)
                nonActiveEnemies.Add(enemy);
        }
        return nonActiveEnemies;
    }

    public void spawnEnemiesAtNextCheckpoint() {
        currentEnemies = new List<GameObject>();
        

        GameObject checkpointObj = getNextEnemyCheckpoint();
        EnemyCheckpoint checkpoint = checkpointObj.GetComponent<EnemyCheckpoint>();

        List<Transform> spawns = checkpoint.GetSpawnLocationsAtCheckpoint();
        List<GameObject> asleepEnemies = getNonActiveEnemies();
        foreach (Transform spawn in spawns)
        {
            GameObject enemy = Instantiate(enemies[0]);

            enemy.GetComponent<EnemyController>().SetupEnemy(spawn);
            currentEnemies.Add(enemy);
            asleepEnemies.RemoveAt(0);
        }

        enemiesLeft = currentEnemies.Count;
    }

    public List<GameObject> getEnemies()
    {
        return getEnemies(true);
    }

}
