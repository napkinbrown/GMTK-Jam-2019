
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarMoveScript : MonoBehaviour {

	// Transforms to act as start and end markers for the journey.
        private Transform nextPoint;

        // Movement speed in units/sec.
        public float speed = 5.0F;
        public float rotateSpeed = 0.1F;
       // public GameObject gm;
        private GameManager gameManager;
        public bool exploded;

        void Start()
        {
            GameObject gmObject = GameObject.FindGameObjectWithTag("GameManager");
            GameObject carParts = GameObject.FindGameObjectWithTag("Car Parts");
            if (gmObject != null)
                gameManager = gmObject.GetComponent<GameManager>();
            else
            Debug.Log("Could not find game manager!");
            nextPoint = GameObject.Find("Checkpoint 1").transform;
            transform.LookAt(nextPoint.position);
            exploded = false;
        }
        
        public void SetNextPoint(Transform checkpoint) {
            Debug.Log("Cam: Setting next point to " + checkpoint.name);
            nextPoint = checkpoint;
            //transform.LookAt(nextPoint.position);
        }

        // Follows the target position like with a spring
        void Update()
        {
            if (nextPoint == null || exploded) {
                Debug.Log("Ending car movement");
                return;
            }
            float step = speed * Time.deltaTime;

        //     // Move our position a step closer to the target.
        //     transform.position = Vector3.MoveTowards(transform.position, nextPoint.position, step);

        //    // if ((nextPoint.position - transform.position).magnitude <= 1) {

        //         //create rotation
        //         Quaternion wantedRotation = nextPoint.transform.rotation;
    
        //         //then rotate
        //         transform.rotation = Quaternion.Lerp(transform.rotation, wantedRotation, Time.deltaTime * rotateSpeed);
            
        //     //}
             // Move the object forward along its z axis 1 unit/second.
            transform.Translate(Vector3.forward * Time.deltaTime * speed);

        
        }
}

