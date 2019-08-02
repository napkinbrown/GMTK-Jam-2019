using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCheckpoint : MonoBehaviour {
	
	void Start () {
        MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mesh in meshes)
        {
            mesh.enabled = false;
        }
	}
}
