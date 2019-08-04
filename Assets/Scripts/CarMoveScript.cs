
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarMoveScript : MonoBehaviour {

	// Transforms to act as start and end markers for the journey.
        private Transform nextPoint;
        private Vector3 startPoint;

        // Movement speed in units/sec.
        public float speed = 5.0F, delay = 1.0F;
        public float rotateSpeed = 0.1F;
       // public GameObject gm;
        private GameManager gameManager;
        private GameObject player;
        public bool exploded, ready;
        public AudioSource audioSource;

        void Start()
        {
            GameObject gmObject = GameObject.FindGameObjectWithTag("GameManager");
            GameObject carParts = GameObject.FindGameObjectWithTag("Car Parts");
            if (gmObject != null)
                gameManager = gmObject.GetComponent<GameManager>();
            else
            Debug.Log("Could not find game manager!");
            nextPoint = transform.parent.GetChild(1);
            //transform.LookAt(nextPoint.position);
            exploded = false;
            ready = false;
            startPoint = this.transform.position;
            //audioSource.Play();

            StartCoroutine(startDriving(delay));
        }

        IEnumerator startDriving(float delay) {
            
            yield return new WaitForSeconds(delay);

            ready = true;

        }

        void OnTriggerEnter(Collider collider) {
            ready = false;
        }

        void OnTriggerExit(Collider collider) {
            StartCoroutine(startDriving(1));
        }
        
        public void SetNextPoint(Transform checkpoint) {
            Debug.Log("Cam: Setting next point to " + checkpoint.name);
            //nextPoint = checkpoint;
            //transform.LookAt(nextPoint.position);
        }

        // Follows the target position like with a spring
        void Update()
        {
            
            player = GameObject.FindGameObjectWithTag("Player");
            float distToPlayer = Vector3.Distance(player.transform.position, transform.position);

            // GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");

            // foreach(GameObject car in cars) {
            //     float distToCar = Vector3.Distance(car.transform.position, transform.position);
            //     if (distToCar < 0.1F) {
            //         Debug.Log("Distance to car " + distToCar);
            //         return;
            //     }
            // }

            if (distToPlayer < 5)
                return;

            if (nextPoint == null || exploded || !ready) {
                if (exploded) {
                    audioSource.Stop();
                }
                Debug.Log("Halting car movement");
                return;
            }
            float step = speed * Time.deltaTime;


             // Move the object forward along its z axis 1 unit/second.
            float dist = Vector3.Distance(nextPoint.transform.position, transform.position);
        
            if (dist > 20)
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            else
                this.transform.position = startPoint;

        
        }
}

