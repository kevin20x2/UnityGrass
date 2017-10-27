using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
public class putGrass : MonoBehaviour {

    // Use this for initialization
    Mesh mesh;
	void Start () {
        mesh = GetComponent<MeshFilter>().mesh;
       // mesh.SetIndices()
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
