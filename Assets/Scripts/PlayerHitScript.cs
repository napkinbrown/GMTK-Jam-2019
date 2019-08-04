using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHitScript : MonoBehaviour
{
    public UnityEvent killedEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnCollisionEnter(Collision collision) {
        Debug.Log("Collision with player, " + collision.relativeVelocity.magnitude);
        if (collision.relativeVelocity.magnitude > 2) {
            Debug.Log("Player hit and dead");
            killedEvent.Invoke();
        }
        transform.parent.GetComponent<Rigidbody>().freezeRotation = false;
    }

}
