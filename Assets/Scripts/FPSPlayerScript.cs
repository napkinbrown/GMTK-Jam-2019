using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FPSPlayerScript : MonoBehaviour
{
    public UnityEvent pickedUpBombEvent;

    public Rigidbody rb;
    public GameObject pov;
    public GameObject bombProp;
    public GameObject bombThrowable;
    public Camera playerCamera;

    private bool holdingBomb = true;

    public float walkSpeed = 15.0F;
    public float sprintSpeed = 150.0F;
    public float strafeSpeed = 15.0F;

    public float xMouseSensitivity = 10;
    public float yMouseSensitivity = 10;

    public float bombThrowForce = 10.0F;
    public float tossMagnitude = 20.0F;
    public float bombGrabLength = 5.0f;
    public float pickUpBombDelay = 1.0f;

    private bool canPickUpBomb = true;


    void Start()
    {
        rb = this.GetComponent<Rigidbody>();

        if(!rb) Debug.LogError("Could not find Rigidbody", this);
        if (!pov) Debug.LogError("Could not find POV", this);
        if (!bombProp) Debug.LogError("Could not find BombObject", this);
    }

    void FixedUpdate()
    {
        CheckPlayerMovement();
        InteractWithBomb();
        StabilizePlayer();
    }

    private void StabilizePlayer()
    {
        Quaternion rot = rb.rotation;
        rot[0] = 0; //null rotation X
        rot[2] = 0; //null rotation Z
        rb.rotation = rot;
    }

    private void InteractWithBomb()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (holdingBomb)
            {
                ThrowBomb();
            }
            else if (canPickUpBomb)
            {
                TryPickingUpBomb();
            }
            
        }
    }

    private void ThrowBomb()
    {
        Vector3 tossVector = tossMagnitude * Vector3.up;
        GameObject thrownBomb = Instantiate(bombThrowable, bombProp.transform.position, this.transform.rotation);
        thrownBomb.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * bombThrowForce);
        thrownBomb.GetComponent<Rigidbody>().AddRelativeForce(tossVector);

        pickedUpBombEvent.AddListener(thrownBomb.GetComponent<BombController>().onBombPickup);
        thrownBomb.GetComponent<BombController>().exploadEvent.AddListener(playerCamera.GetComponent<BombCameraShake>().onExplosionEvent);

        bombProp.SetActive(false);
        holdingBomb = false;
        StartCoroutine(DelayEnablePickupBomb());
    }

    private void TryPickingUpBomb()
    {
        RaycastHit hitInfo;
        Vector3 rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

        Debug.DrawRay(rayOrigin, playerCamera.transform.forward);
        if (Physics.Raycast(rayOrigin, playerCamera.transform.forward, out hitInfo, bombGrabLength))
        {

        }
        if (hitInfo.collider != null && hitInfo.collider.gameObject.tag == "Bomb")
        {
            pickedUpBombEvent.Invoke();
            bombProp.SetActive(true);
            holdingBomb = true;
        }
    }

    private void CheckPlayerMovement()
    {
        CheckForwardMovement();
        CheckBackwardMovement();
        CheckLeftStrafe();
        CheckRightStrafe();
        ApplyRotationFromMouse();
        ApplyWalkingDrag();
    }

    private void ApplyRotationFromMouse()
    {
        ApplyXMouseRotation();
        ApplyYMouseRotation();
    }

    private void ApplyYMouseRotation()
    {
        float ySwivelSpeed = yMouseSensitivity * Time.deltaTime * -1;
        pov.transform.Rotate(Input.GetAxisRaw("Mouse Y") * ySwivelSpeed, 0, 0);
    }

    private void ApplyXMouseRotation()
    {
        float xSwivelSpeed = xMouseSensitivity * Time.deltaTime;
        this.transform.Rotate(0, Input.GetAxisRaw("Mouse X") * xSwivelSpeed, 0);
    }

    private void CheckRightStrafe()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = this.transform.right * strafeSpeed;
        }
    }

    private void CheckLeftStrafe()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = this.transform.right * strafeSpeed  * -1;
        }
    }

    private void CheckBackwardMovement()
    {
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = this.transform.forward * walkSpeed * -1;
        }
    }

    private void CheckForwardMovement()
    {

        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                rb.velocity = this.transform.forward * sprintSpeed;
            }
            else
            {
                rb.velocity = this.transform.forward * walkSpeed;
            }
        }
    }

    private bool HoldingMoveKey()
    {
        return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);
    }

    private void ApplyWalkingDrag()
    {
        if(!HoldingMoveKey())
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
    }

    IEnumerator DelayEnablePickupBomb()
    {
        canPickUpBomb = false;
        yield return new WaitForSeconds(pickUpBombDelay);
        canPickUpBomb = true;
    }

}
