﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    private GameManager gm;
    public List<GameObject> blocks;
    public int numHit;
    // Start is called before the first frame update
    void Start()
    {
        GameObject gmObject = GameObject.FindGameObjectWithTag("GameManager");
        if (gmObject != null)
            gm = gmObject.GetComponent<GameManager>();
        else
            Debug.Log("Could not find game manager!");


        numHit = 0;
        foreach (Transform child in transform)
         {
             if (child.tag == "Block")
             {
                 blocks.Add(child.gameObject);
             }
         }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void blocksHit() {
        numHit++;
        Debug.Log("Tower has lost " + numHit + " blocks.");
        if (numHit > 10)
            gm.score += 100;
        if (numHit > 20)
            gm.score += 200;
        if (numHit > 30)
            gm.score += 500;
    }
}