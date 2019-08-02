
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class camMoveScript : MonoBehaviour {

	// Transforms to act as start and end markers for the journey.
        private Transform nextPoint;

        public Image bloodImage;

        // Movement speed in units/sec.
        public float speed = 5.0F;
        public float rotateSpeed = 0.1F;

        private bool getDamage;
        private Color originalColor;

        void Start()
        {
            //nextPoint = GameObject.Find("Checkpoint 1").transform;
            //transform.LookAt(nextPoint.position);
            originalColor = bloodImage.color;
            bloodImage.GetComponent<Image>().color = Color.clear;
        }
        
        public void SetNextPoint(Transform checkpoint) {
            Debug.Log("Cam: Setting next point to " + checkpoint.name);
            nextPoint = checkpoint;
        }

        public void RedFlash() {
            Debug.Log("flash");
        
            StartCoroutine(Pulse());

        }

        private IEnumerator Pulse() {
            
            Debug.Log("pulse");
            bloodImage.GetComponent<Image>().color = Color.red;
            yield return new WaitForSeconds(.1f);
            bloodImage.GetComponent<Image>().color = Color.clear;
            yield return new WaitForSeconds(.1f);
            bloodImage.GetComponent<Image>().color = Color.red;
            yield return new WaitForSeconds(.1f);
            bloodImage.GetComponent<Image>().color = Color.clear;
            yield return new WaitForSeconds(.1f);
            bloodImage.GetComponent<Image>().color = Color.red;
            yield return new WaitForSeconds(.2f);
            bloodImage.GetComponent<Image>().color = Color.clear;
            yield return new WaitForSeconds(.2f);
            bloodImage.GetComponent<Image>().color = Color.red;
            yield return new WaitForSeconds(.2f);
            bloodImage.GetComponent<Image>().color = Color.clear;

        }

        // Follows the target position like with a spring
        void Update()
        {
            if (nextPoint == null)
                return;
            float step = speed * Time.deltaTime;

            // Move our position a step closer to the target.
            transform.position = Vector3.MoveTowards(transform.position, nextPoint.position, step);

            if ((nextPoint.position - transform.position).magnitude <= 1) {

                //create rotation
                Quaternion wantedRotation = nextPoint.transform.rotation;
    
                //then rotate
                transform.rotation = Quaternion.Lerp(transform.rotation, wantedRotation, Time.deltaTime * rotateSpeed);
            
            }
        
        }
}

