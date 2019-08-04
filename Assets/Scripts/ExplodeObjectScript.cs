using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable] public class _UnityEventGameObject:UnityEvent<GameObject> {}

public class ExplodeObjectScript : MonoBehaviour
{

    private GameManager gm;
    private GameObject parentTower;
    //public _UnityEventGameObject score;
    public bool scored, ready;
    // Start is called before the first frame update
    void Start()
    {
        
        
        parentTower = transform.parent.gameObject;
        if(!parentTower) 
            Debug.LogError("ParentTower not attached", this);
        
        
        scored = false;
        ready = false;
        //score.Invoke(this.gameObject);
        GameObject gmObject = GameObject.FindGameObjectWithTag("GameManager");
        if (gmObject != null)
            gm = gmObject.GetComponent<GameManager>();
        //else
            //Debug.Log("Could not find game manager!");
        StartCoroutine(waitToScore());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator waitToScore() {
        yield return new WaitForSeconds(2);
        ready = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        bool esplode = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().bombExploded;
        if (ready && !scored && collision.gameObject.name != "Player") {
            scored = true;
            Vector3 velocity = collision.relativeVelocity;
            if(velocity.magnitude > 2)
                gm.score += 10;

            parentTower.GetComponent<TowerScript>().blocksHit();
            //Debug.Log("Hit with velocity of " + velocity + ", score is " + gm.score);
        } else {
            //Debug.Log("Hit by " + collision.gameObject);
        }
    }
}
