using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerScript : MonoBehaviour
{

    public float walkSpeed = 2F;
    public float strafeSpeed = 2F;

    public float realignSpeed = 1.0F;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveIfKeyDown();
    }

    private void MoveIfKeyDown()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!IsRightsideUp())
            {
                RealignBody();
            }
        }

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
    }

    private void RealignBody()
    {
        this.transform.SetPositionAndRotation(this.transform.position, Quaternion.Euler(Vector3.zero));
    }

    private bool IsRightsideUp()
    {
        if (this.transform.rotation.eulerAngles == Vector3.zero)
            return true;
        return false;
    }
}
