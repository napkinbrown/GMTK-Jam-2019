using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{
    
//    public ParticleSystem flashParticle;
//    public GameObject crosshair;

    public float walkSpeed = 2;
    public float strafeSpeed = 2;
    public float mouseSensitivity = 30;
//
//    public float reloadForSeconds;
//
//    public static int initialBullets = 6;
//    public int currentBullets = 0;
//
//    private bool reloading = false;

//    private GameManager gm;
//    public Transform gunPosition;

    void Start()
    {
//        currentBullets = initialBullets;
//        Debug.Log(currentBullets);
//
//        /*
//         * Getting the Game Manager
//         */
//        GameObject gmObject = GameObject.FindGameObjectWithTag("GameManager");
//        if (gmObject != null)
//            gm = gmObject.GetComponent<GameManager>();
//        else
//            Debug.LogError("Could not find Game Manager! Please make sure there is one in the scene!", this);
//
//        /*
//         * Getting the gun object
//         */
//        GameObject[] children = GetComponentsInChildren<GameObject>();
//        foreach (GameObject child in children) {
//            if (child.name == "Gun")
//                gunPosition = child.transform;
//        }
//        if (gunPosition == null)
//            Debug.LogError("Couldn't find player gun object!", this);
        /* !!! Do not put anything below here in Update. Unity is stupid and won't do what you want !!! */
    }

    void Update()
    {
        
            /*
             * 
             * Mouse movement 
             * 
             */
            this.transform.Rotate(0, Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime, 0);

            /* 
             * 
             * Keyboard controlls 
             * 
             */
            Vector3 walkVector = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
            {
                walkVector += new Vector3(walkSpeed * Time.deltaTime, 0);
            }

            if (Input.GetKey(KeyCode.S))
            {
                walkVector += new Vector3(-walkSpeed * Time.deltaTime, 0);
            }

            if (Input.GetKey(KeyCode.A))
            {
                walkVector += new Vector3(0, 0, strafeSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D))
            {
                walkVector += new Vector3(0, 0, -strafeSpeed * Time.deltaTime);
            }

            this.transform.Translate(walkVector, Space.Self);

            /*
             * 
             * Firing
             * 
             */
            // if (Input.GetButtonDown("Fire1"))
            // {
            //     if (currentBullets > 1)
            //     {
            //         FireGun();
            //         flashParticle.Play();
            //     }
            //     else
            //     {
            //         FireGun();
            //         StartCoroutine("Reload");
            //     }
            // }

            if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.R))
            {
                //StartCoroutine("Reload");
            }
//            RaycastHit hitInfo = GetHit();
//            if (hitInfo.collider.CompareTag("CameraMan"))
//            {
//                crosshair.transform.position = new Vector3(hitInfo.point.x, crosshair.transform.position.y, crosshair.transform.position.z);
//            }
        
    }

    // private void FireGun()
    // {
    //     if (currentBullets > 0)
    //     {
    //         currentBullets--;
    //         Debug.Log(currentBullets);
    //         RaycastHit hitInfo = GetHit();
    //         if (hitInfo.collider != null)
    //         {
    //             gm.PlayerShotObject(hitInfo, currentBullets);
    //         }
    //     }
    // }

    // private RaycastHit GetHit() {
    //     RaycastHit hitInfo;

    //         float gunRotation = gunPosition.rotation.eulerAngles.y; //In degrees
    //         float xComponent = Mathf.Cos(gunRotation * Mathf.Deg2Rad);
    //         float zComponent = -Mathf.Sin(gunRotation * Mathf.Deg2Rad);
    //         Vector3 direction = new Vector3(xComponent, 0, zComponent);

    //         // Layer mask determines what gets hit. Here i'm telling it to hit everything except the Player layer
    //         // It uses bitshifting, which is a weird way to do it, but w/e
    //         int layerMask = 1 << 8;
    //         layerMask = ~layerMask;

    //         Debug.DrawRay(gunPosition.position, direction, Color.black, 10f);
    //         Physics.Raycast(gunPosition.position, direction, out hitInfo, Mathf.Infinity, layerMask, QueryTriggerInteraction.Collide);


    //         return hitInfo;
    // }

    // IEnumerator Reload() {
    //     if (!reloading && (currentBullets < initialBullets))
    //     {
    //         reloading = true;
    //         gm.PlayerIsReloading();

    //         yield return new WaitForSeconds(reloadForSeconds);

    //         currentBullets = initialBullets;
    //         reloading = false;
    //         gm.PlayerIsDoneReloading();
    //     }
    // }
}
