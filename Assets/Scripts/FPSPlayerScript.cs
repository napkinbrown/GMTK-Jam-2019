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
    public Camera camera;

    private bool holdingBomb = true;

    public float walkSpeed = 15.0F;
    public float sprintSpeed = 150.0F;
    public float strafeSpeed = 15.0F;

    public float xMouseSensitivity = 10;
    public float yMouseSensitivity = 10;

    public float bombThrowForce = 10.0F;

    public float tossMagnitude = 20.0F;

    public float bombGrabLength = 5.0f;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();

        if(!rb) Debug.LogError("Could not find Rigidbody", this);
        if (!pov) Debug.LogError("Could not find POV", this);
        if (!bombProp) Debug.LogError("Could not find BombObject", this);
    }

    void Update()
    {
        CheckPlayerMovement();
        InteractWithBomb();

        // TODO: Check Detenate Bomb
    }

    private void InteractWithBomb()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (holdingBomb)
            {
                ThrowBomb();
            }
            else
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

        bombProp.SetActive(false);
        holdingBomb = false;
    }

    private void TryPickingUpBomb()
    {
        RaycastHit hitInfo;
        Vector3 rayOrigin = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

        Debug.DrawRay(rayOrigin, camera.transform.forward);
        if (Physics.Raycast(rayOrigin, camera.transform.forward, out hitInfo, bombGrabLength))
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
            rb.AddRelativeForce(Vector3.right * strafeSpeed);
        }
    }

    private void CheckLeftStrafe()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddRelativeForce(Vector3.left * strafeSpeed);
        }
    }

    private void CheckBackwardMovement()
    {
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddRelativeForce(Vector3.back * walkSpeed);
        }
    }

    private void CheckForwardMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                rb.AddRelativeForce(Vector3.forward * sprintSpeed);
            }
            else
            {
                rb.AddRelativeForce(Vector3.forward * walkSpeed);
            }
        }
    }
}
