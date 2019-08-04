using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombHighlightController : MonoBehaviour
{
    void Start()
    {
        
    }

    public void OnBombHighlight()
    {
        Debug.Log("I hear ya");
        this.GetComponent<MeshRenderer>().enabled = false;
    }
}
