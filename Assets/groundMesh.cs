using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
public class groundMesh : MonoBehaviour {

    // Use this for initialization
    public Texture2D height_tex;// = Texture2D.blackTexture;
    private void Awake()
    {
     //   height_tex = Texture2D.blackTexture;
        
    }
    void Start () {
        GetComponent<MeshFilter>().mesh = ground_mesh_gen(100,height_tex);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    float step = 0.4f;
    Mesh ground_mesh_gen(int point_number,Texture2D height_tex)
    {
        Mesh mesh = new Mesh();
        List<Vector3> vertex = new List<Vector3>();
        List<Vector2> uv = new List<Vector2>();
        List<int> index = new List<int>();
        
        for(int i = 0;i<point_number;++i)
        {
            for(int j = 0;j<point_number;++j)
            {
                float dx = 0.2f*Random.Range(0,step);
                float dy = 0.2f*Random.Range(0,step);
                float dz = 0.2f*Random.Range(0,step);
                vertex.Add(new Vector3(i*step+dx,height_tex.GetPixel(i,j).grayscale*5+dy,j*step+dz));
                uv.Add(new Vector2(1.0f*i/(point_number-1),1.0f*j/(point_number-1)));
                if (i == 0 || j == 0) continue;
                index.Add(i * point_number + j);
                index.Add(i * point_number + j - 1);
                index.Add((i - 1) * point_number + (j - 1));

                index.Add(i * point_number +j);
                index.Add((i - 1) * point_number + (j - 1));
                index.Add((i - 1) * point_number + j);
            }
        }
        mesh.vertices = vertex.ToArray();
        mesh.SetIndices(index.ToArray(), MeshTopology.Points, 0);
        //mesh.uv = uv.ToArray();
        //mesh.triangles = index.ToArray();
        //mesh.RecalculateNormals();
        //mesh.RecalculateTangents();
        return mesh;
    }
}