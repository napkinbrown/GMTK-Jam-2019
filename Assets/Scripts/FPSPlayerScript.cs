﻿using System;
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
    private GameManager gameManager;

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

    private bool canPickUpBomb = true, gameOver = false, readyToThrow = true;

    public float povRotationMin;
    public float povRotationMax;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        GameObject gmObject = GameObject.FindGameObjectWithTag("GameManager");
        if (gmObject != null)
            gameManager = gmObject.GetComponent<GameManager>();
        else
            Debug.Log("Could not find game manager!");

        if(!rb) Debug.LogError("Could not find Rigidbody", this);
        if (!pov) Debug.LogError("Could not find POV", this);
        if (!bombProp) Debug.LogError("Could not find BombObject", this);
    }

    void FixedUpdate()
    {
        gameOver = gameManager.gameIsOver;
        
        if (gameOver) {
            Debug.Log("Player thinks game is over");
            return;
        }

        CheckPlayerMovement();
        ClampMouseRotation();
        InteractWithBomb();
        StabilizePlayer();
    }

    private void ClampMouseRotation()
    {
        Quaternion rotation = pov.transform.localRotation;
        float xRotation = rotation.eulerAngles.x;
        if (xRotation >= 180)
            xRotation -= 360;

        float clampedX = Mathf.Clamp(xRotation, povRotationMin, povRotationMax);

        pov.transform.localRotation = Quaternion.Euler(new Vector3(clampedX, 0, 0));
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
        if (Input.GetMouseButtonDown(0))
        {
            if (holdingBomb && readyToThrow)
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
        holdingBomb = false;
        Vector3 tossVector = tossMagnitude * Vector3.up;
        GameObject thrownBomb = Instantiate(bombThrowable, bombProp.transform.position, this.transform.rotation);
        thrownBomb.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * bombThrowForce);
        thrownBomb.GetComponent<Rigidbody>().AddRelativeForce(tossVector);

        pickedUpBombEvent.AddListener(thrownBomb.GetComponent<BombController>().onBombPickup);
        thrownBomb.GetComponent<BombController>().exploadEvent.AddListener(playerCamera.GetComponent<BombCameraShake>().onExplosionEvent);

        bombProp.SetActive(false);
        canPickUpBomb = false;

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
            readyToThrow = false;
            StartCoroutine(waitToThrow());
        }
    }

    IEnumerator waitToThrow() {
        yield return new WaitForSeconds(1);
        readyToThrow = true;
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
        pov.transform.Rotate(Input.GetAxisRaw("Mouse Y") * ySwivelSpeed * Time.deltaTime, 0, 0);
    }

    private void ApplyXMouseRotation()
    {
        float xSwivelSpeed = xMouseSensitivity * Time.deltaTime;
        this.transform.Rotate(0, Input.GetAxisRaw("Mouse X") * xSwivelSpeed * Time.deltaTime, 0);
    }

    private void CheckRightStrafe()
    {
        if (Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.W))
                rb.velocity += this.transform.right * strafeSpeed;
            else
                rb.velocity = this.transform.right * strafeSpeed;
        }
    }

    private void CheckLeftStrafe()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if(Input.GetKey(KeyCode.W))
                rb.velocity += this.transform.right * strafeSpeed * -1;
            else
                rb.velocity = this.transform.right * strafeSpeed * -1;
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
                rb.velocity = new Vector3(this.transform.forward.x * sprintSpeed, rb.velocity.y, this.transform.forward.z * sprintSpeed);
            }
            else
            {
                rb.velocity = new Vector3(this.transform.forward.x * walkSpeed, rb.velocity.y, this.transform.forward.z * walkSpeed);
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
        yield return new WaitForSeconds(pickUpBombDelay);
        canPickUpBomb = true;
    }

}
