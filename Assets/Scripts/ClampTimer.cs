﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClampTimer : MonoBehaviour
{
    public Text timeLabel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 namePos = Camera.main.WorldToScreenPoint(this.transform.position);
        timeLabel.transform.position = namePos;
        timeLabel.transform.rotation = this.transform.rotation;
    }
}
