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
    public bool scored;
    // Start is called before the first frame update
    void Start()
    {
        parentTower = transform.parent.gameObject;
        scored = false;
        //score.Invoke(this.gameObject);
        GameObject gmObject = GameObject.FindGameObjectWithTag("GameManager");
        if (gmObject != null)
            gm = gmObject.GetComponent<GameManager>();
        else
            Debug.Log("Could not find game manager!");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!scored && gm.bombExploded && collision.gameObject.name != "Player" && collision.gameObject.name != "Ground") {
            scored = true;
            Vector3 velocity = collision.relativeVelocity;
            if(velocity.magnitude > 2)
                gm.score += 10;

            parentTower.GetComponent<TowerScript>().blocksHit();
            //Debug.Log("Hit with velocity of " + velocity + ", score is " + gm.score);
        } else {
            //Debug.Log("Hit by " + collision.);
        }
    }
}
