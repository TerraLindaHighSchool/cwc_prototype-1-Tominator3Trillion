using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipNormals : MonoBehaviour
{
    public bool flip = true;
    void Start()
    {
        if(!flip)
            return;
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        
        Vector3[] normals = mesh.normals;
        for (int i = 0; i < normals.Length; i++)
            normals[i] = -normals[i];
        mesh.normals = normals;

        for (int i = 0; i < mesh.subMeshCount; i++)
        {
            int[] tris = mesh.GetTriangles(i);
            for (int j = 0; j < tris.Length; j += 3)
            {
                int temp = tris[j];
                tris[j] = tris[j + 1];
                tris[j + 1] = temp;
            }
            mesh.SetTriangles(tris, i);
        }
    }

    void Update() {
        //base on distance of main camera change material alpha
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        if (distance < 1000)
        {
            GetComponent<Renderer>().material.color = new Color(1, 1, 1, distance / 1000);
        }
    }

}
