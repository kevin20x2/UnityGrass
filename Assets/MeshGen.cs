using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
public class MeshGen : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<MeshFilter>().mesh = get_grass_mesh(6);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    float step = 1.0f;
    float width = 0.2f;
    Mesh get_grass_mesh(int lod)
    {
        Mesh mesh = new Mesh();
        List<Vector3> point_list = new List<Vector3>();
        List<Vector2> uv_list = new List<Vector2>();
        List<int> index_list = new List<int>();
        float height = step * 5.0f;
        float one_step = height / lod;
        for(int i = 0;i<lod;++i)
        {
            point_list.Add(new Vector3(-width/2,0,i*one_step));
            point_list.Add(new Vector3(width / 2, 0, i *one_step));
            uv_list.Add(new Vector2(0.0f, i * 1.0f / (lod-1)));
            uv_list.Add(new Vector2(1.0f, i * 1.0f / (lod-1)));
            if(i>0)
            {
                index_list.Add(2 * i);
                index_list.Add(2 * i+1);
                index_list.Add(2 * i - 2);
                index_list.Add(2 * i + 1);
                index_list.Add(2 * i - 1);
                index_list.Add(2 * i - 2);
            }
        }
        mesh.vertices = point_list.ToArray();
        mesh.SetIndices(index_list.ToArray(), MeshTopology.Points, 0);
        //mesh.uv = uv_list.ToArray();
        //mesh.triangles = index_list.ToArray();
     //   mesh.RecalculateNormals();
      //  mesh.RecalculateTangents();


        return mesh;
    }
}
