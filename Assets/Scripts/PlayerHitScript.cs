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

    private void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Bomb")
            return;

        float velocity = collider.gameObject.GetComponent<Rigidbody>().velocity.magnitude;
        Debug.Log("Collision with player, " + velocity);
        if (velocity > 2) {
            Debug.Log("Player hit and dead");
            transform.parent.GetComponent<Rigidbody>().freezeRotation = false;
            killedEvent.Invoke();
        }
    }

}
