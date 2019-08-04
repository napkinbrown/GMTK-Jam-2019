using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    private GameManager gm;
    public List<GameObject> blocks;
    public int numHit, numBlocks;
    private bool isDestroyed = false;
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
         numBlocks = blocks.Count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void blocksHit() {
        numHit++;
        Debug.Log(this.gameObject + " has lost " + numHit + " blocks.");
        if (numHit > numBlocks / 3)
            gm.score += 100;
        if (numHit > numBlocks * .85) {
            gm.score += 200;
            if(!isDestroyed)
            {
                MarkAsDestroyed();
            }
        }
        if (numHit >= numBlocks) {
            gm.score += 500;
        }
    }

    private void MarkAsDestroyed()
    {
        gm.BuildingDestroyed();
        isDestroyed = true;
    }
}
