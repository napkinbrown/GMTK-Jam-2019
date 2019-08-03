using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCheckpoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mesh in meshes)
        {
            mesh.enabled = false;
        }
    }
}
