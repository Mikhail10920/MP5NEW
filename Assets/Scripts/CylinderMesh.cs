using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderMesh : MonoBehaviour
{
    // Start is called before the first frame update
    public float radius;
    public int resolution;

    public float degrees;

    public int upResolution;
    public float squareHeight;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Mesh theMesh = GetComponent<MeshFilter>().mesh;  
        theMesh.Clear();
        Vector3[] normalArray = new Vector3[(upResolution + 1) * (resolution + 1)];
        Vector3[] vertexArray = MakeVertices(ref normalArray);
        int[] triangleArray = new int[(upResolution * resolution) * 2 * 3];
        MakeTriangles(ref triangleArray);
        theMesh.vertices = vertexArray;
        theMesh.triangles = triangleArray;
        theMesh.normals = normalArray;
    }
    Vector3[] MakeVertices(ref Vector3[] normalArray)
    {
        Vector3[] vertexArray = new Vector3[(upResolution + 1) * (resolution + 1)];
        for(int i = 0; i < resolution + 1; i++)
        {
            for(int j = 0; j < upResolution + 1; j++)
            {
                Vector3 xzPosition = new Vector3(radius * Mathf.Cos(i * (Mathf.Deg2Rad * degrees/resolution)), 0, radius * Mathf.Sin(i * (Mathf.Deg2Rad * degrees/resolution)));
                Vector3 yPosition = new Vector3(0, j * squareHeight, 0);
                vertexArray[j + i * (upResolution + 1)] = xzPosition + yPosition;
                //Debug.Log(vertexArray[j + i * (upResolution + 1)]);
                normalArray[j + i * (upResolution + 1)] = xzPosition.normalized;
            }
        }
        return vertexArray;
    }
    void MakeTriangles(ref int[] triangleArray)
    {
        /*
        for(int i = 0; i < vertexArray.Length; i += 2)
        {
            vertexArray[i] = point1 + new Vector3(0, i * squareHeight, 0);
            vertexArray[i+1] = point2 + new Vector3(0, i * squareHeight, 0); 
        }
        */
        for(int j = 0; j < resolution; j++)
        {

        
            for(int i = 0; i < upResolution; i++)
            {
                triangleArray[i * 6 + j * (upResolution) * 6] = 0 + i + j * (upResolution + 1);
                triangleArray[i * 6 + j * (upResolution) * 6 + 1] = 1 + i + j * (upResolution + 1);
                triangleArray[i * 6 + j * (upResolution)* 6 + 2] = 1 + i + (j + 1) * (upResolution + 1);

                triangleArray[i * 6 + j * (upResolution)* 6 + 3] = 0 + i + j * (upResolution + 1);
                triangleArray[i * 6 + j * (upResolution)* 6 + 4] = 1 + i + (j + 1) * (upResolution + 1);
                triangleArray[i * 6 + j * (upResolution) * 6 + 5] = 0 + i + (j + 1) * (upResolution + 1);
                
            }
        }
    }
}
