using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPartsScript : MonoBehaviour
{
    private List<Rigidbody> rbs;
    // Start is called before the first frame update
    void Start()
    {
        rbs = new List<Rigidbody>();
        this.gameObject.GetComponentsInChildren<Rigidbody>(true, rbs);
        
        foreach(Rigidbody rb in rbs) {
            rb.isKinematic = true;
            rb.WakeUp();
        }
    }

    public void disableKinematics() {
        Debug.Log("Made it to disable rigies");
        rbs = new List<Rigidbody>();
        this.gameObject.GetComponentsInChildren<Rigidbody>(true, rbs);
        foreach(Rigidbody rb in rbs) {
            rb.isKinematic = false;
            rb.WakeUp();
        }
    }

}
