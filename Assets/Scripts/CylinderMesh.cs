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

    [SerializeField] GameObject emptyObj;
    void Start()
    {
        GameObject ObjectsInside = Instantiate(emptyObj);
        ObjectsInside.transform.parent = this.transform;

        ChangeResolution();
    }

    // Update is called once per frame
    void Update()
    {
        
        Mesh theMesh = GetComponent<MeshFilter>().mesh;
        Vector3[] v = theMesh.vertices;
        Vector3[] normal = theMesh.normals;
        for (int i = 0; i<mControllers.Length; i++)
        {
            v[i] = mControllers[i].transform.localPosition;
        }

        ComputeNormals(v, normal, resolution, upResolution);

        theMesh.vertices = v;
        theMesh.normals = normal;
        
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
    Vector3[] MakeVertices360(ref Vector3[] normalArray)
    {
        Vector3[] vertexArray = new Vector3[(upResolution + 1) * (resolution)];
        for(int i = 0; i < resolution; i++)
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
    int[] MakeTriangles()
    {
        /*
        for(int i = 0; i < vertexArray.Length; i += 2)
        {
            vertexArray[i] = point1 + new Vector3(0, i * squareHeight, 0);
            vertexArray[i+1] = point2 + new Vector3(0, i * squareHeight, 0); 
        }
        */
        int[] triangleArray = new int[(upResolution * resolution) * 2 * 3];

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
        return triangleArray;
    }
    int[] MakeTriangles360()
    {
        /*
        for(int i = 0; i < vertexArray.Length; i += 2)
        {
            vertexArray[i] = point1 + new Vector3(0, i * squareHeight, 0);
            vertexArray[i+1] = point2 + new Vector3(0, i * squareHeight, 0); 
        }
        */
        int[] triangleArray = new int[(upResolution * resolution) * 2 * 3];

        for(int j = 0; j < resolution - 1; j++)
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
        for(int i = 0; i < upResolution; i++)
            {
                triangleArray[i * 6 + (resolution - 1) * (upResolution) * 6] = 0 + i + (resolution - 1) * (upResolution + 1);
                triangleArray[i * 6 + (resolution - 1) * (upResolution) * 6 + 1] = 1 + i + (resolution - 1) * (upResolution + 1);
                triangleArray[i * 6 + (resolution - 1) * (upResolution)* 6 + 2] = 1 + i;

                triangleArray[i * 6 + (resolution - 1) * (upResolution)* 6 + 3] = 0 + i + (resolution - 1) * (upResolution + 1);
                triangleArray[i * 6 + (resolution - 1) * (upResolution)* 6 + 4] = 1 + i;
                triangleArray[i * 6 + (resolution - 1) * (upResolution) * 6 + 5] = 0 + i;
                
            }
        return triangleArray;
    }
    public void ChangeResolution()
    {
        DestroyChildern();
        Mesh theMesh = GetComponent<MeshFilter>().mesh;  
        theMesh.Clear();
        Vector3[] normalArray;
        Vector3[] vertexArray;
        int[] triangleArray;
        if(degrees != 360)
        {
            normalArray = new Vector3[(upResolution + 1) * (resolution + 1)];
            vertexArray = MakeVertices(ref normalArray);
            triangleArray = MakeTriangles();
        }
        else
        {
            normalArray = new Vector3[(upResolution + 1) * (resolution)];
            vertexArray = MakeVertices360(ref normalArray);
            triangleArray = MakeTriangles360();
        }
        //MakeTriangles();
        theMesh.vertices = vertexArray;
        theMesh.triangles = triangleArray;
        theMesh.normals = normalArray;
        InitControllers(vertexArray);
        InitNormals(vertexArray, normalArray);
    }


    void DestroyChildern()
    {
        if (this.transform.childCount != 0)
        {
            Debug.Log(this.transform.GetChild(0).gameObject.name);
            GameObject chiled = this.transform.GetChild(0).gameObject;
            for (int i = chiled.transform.childCount - 1; i >= 0; i--)
            {
                //GameObject chiled = this.gameObject.transform.GetChild(i).gameObject;
                Destroy(chiled.gameObject.transform.GetChild(i).gameObject);
                Debug.Log("Sestroyed");
            }

            //Destroy(this.gameObject.transform.GetChild(0).gameObject);
        }
    }









    LineSegment[] mNormals;

    void InitNormals(Vector3[] v, Vector3[] n)
    {
        mNormals = new LineSegment[v.Length];
        for (int i = 0; i < v.Length; i++)
        {
            GameObject o = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            mNormals[i] = o.AddComponent<LineSegment>();
            mNormals[i].SetWidth(0.05f);
            mNormals[i].transform.SetParent(this.transform.GetChild(0));
        }
        UpdateNormals(v, n);
    }

    void UpdateNormals(Vector3[] v, Vector3[] n)
    {
        for (int i = 0; i < v.Length; i++)
        {
            mNormals[i].SetEndPoints(v[i], v[i] + 1.0f * n[i]);
        }
    }

    Vector3 FaceNormal(Vector3[] v, int i0, int i1, int i2)
    {
        Vector3 a = v[i1] - v[i0];
        Vector3 b = v[i2] - v[i0];
        return Vector3.Cross(a, b).normalized;
    }

    void ComputeNormals(Vector3[] v, Vector3[] normal,int m, int n)
    {
        int[] temp;
        if(degrees != 360)
        {
            temp = MakeTriangles();
        }
        else
        {
            temp = MakeTriangles360();
        }
        Vector3[] triNormal = new Vector3[m * n * 2];
        for (int i = 0; i < triNormal.Length; i++)
        {
            triNormal[i] = FaceNormal(v, temp[0 + 3 * i], temp[1 + 3 * i], temp[2 + 3 * i]);
        }
        if(degrees != 360)
        {
            //Debug.Log("not 360");
            for(int i = 0; i < normal.Length; i++)
            {
                
                normal[i] = TrianglesAround(i, m, n, triNormal);
                
                
                
            }
        }
        else
        {
            for(int i = 0; i < normal.Length; i++)
            {
                
                normal[i] = TrianglesAround360(i, m, n, triNormal);
                
            }
        }
        /*
        triNormal[0] = FaceNormal(v, 3, 4, 0);
        triNormal[1] = FaceNormal(v, 0, 4, 1);
        triNormal[2] = FaceNormal(v, 4, 5, 1);
        triNormal[3] = FaceNormal(v, 1, 5, 2);
        triNormal[4] = FaceNormal(v, 6, 7, 3);
        triNormal[5] = FaceNormal(v, 3, 7, 4);
        triNormal[6] = FaceNormal(v, 7, 8, 4);
        triNormal[7] = FaceNormal(v, 4, 8, 5);
        */

        /*
        normal[0] = (triNormal[0] + triNormal[1]).normalized;
        normal[1] = (triNormal[1] + triNormal[2] + triNormal[3]).normalized;
        normal[2] = triNormal[3].normalized;
        normal[3] = (triNormal[0] + triNormal[4] + triNormal[5]).normalized;
        normal[4] = (triNormal[0] + triNormal[1] + triNormal[2] + triNormal[5] + triNormal[6] + triNormal[7]).normalized;
        normal[5] = (triNormal[2] + triNormal[3]).normalized;
        normal[6] = triNormal[4].normalized;
        normal[7] = (triNormal[4] + triNormal[5] + triNormal[6]).normalized;
        normal[8] = (triNormal[6] + triNormal[7]).normalized;
        */
        UpdateNormals(v, normal);
    }
    Vector3 TrianglesAround360(int i, int m, int n, Vector3[] triNormal)
    {
        int row = i % (n+1);
        int column = i / (n+1);

        if(row == 0)
        {
            if(column == 0)
            {
                int tri1 = 0;
                int tri2 = 1;
                int tri3 = (n * 2) * (m - 1) + 1;
                return (triNormal[tri1] + triNormal[tri2] + triNormal[tri3]).normalized;
            }
            /*
            else if(column == m)
            {
                int tri1 = (n * 2) * (m - 1) + 1;
                int tri2 = 0;
                int tri3 = 1;
                return (triNormal[tri1] + triNormal[tri2] + triNormal[tri3]).normalized;
            }
            */
            else
            {
                int tri1 = (column - 1) * (n * 2) + 1;
                int tri2 = tri1 + n*2;
                int tri3 = tri2 - 1;
                return (triNormal[tri1] + triNormal[tri2] + triNormal[tri3]).normalized;
            }
        }
        else if(row == n)
        {
            if(column == 0)
            {
                int tri1 = (n - 1) * 2;
                int tri2 = n * 2 * (m - 1) + (n - 1) * 2;
                int tri3 = tri2 + 1;
                return (triNormal[tri1] + triNormal[tri2] + triNormal[tri3]).normalized;
            }
            /*
            else if(column == m)
            {
                int tri1 = n * 2 * (m - 1) + (n - 1) * 2;
                int tri2 = tri1 + 1;
                int tri3 = (n - 1) * 2;
                return (triNormal[tri1] + triNormal[tri2] + triNormal[tri3]).normalized;
            }
            */
            else
            {
                int tri1 = (row - 1) * 2 + (column - 1) * n * 2 + 1;
                int tri2 = tri1 + 1;
                int tri3 = tri1 + n*2;
                return (triNormal[tri1] + triNormal[tri2] + triNormal[tri3]).normalized;
            }
        }
        else if(column == 0 && row != 0 && row != n)
        {
            int tri1 = (row - 1) * 2;
            int tri2 = tri1 + 1;
            int tri3 = tri2 + 2;
            int tri4 = (row - 1) * 2 + (m - 1) * n * 2;
            int tri5 = tri4 + 1;
            int tri6 = tri5 + 2;
            return (triNormal[tri1] + triNormal[tri2] + triNormal[tri3] + triNormal[tri4] + triNormal[tri5] + triNormal[tri6]).normalized;
        }
        /*
        else if(column == m && row != 0 && row != n)
        {
            int tri1 = (row - 1) * 2 + (column - 1) * n * 2;
            int tri2 = tri1 + 1;
            int tri3 = tri2 + 2;
            return (triNormal[tri1] + triNormal[tri2] + triNormal[tri3]).normalized;
        }
        */
        //Triangles for when point is in the middle
        else
        {
            int tri1 = (row - 1) * 2 + (column - 1) * n * 2; //Bot left square top triangle
            int tri2 = tri1 + 1;
            int tri3 = tri2 + 1;
            int tri4 = tri2 + n * 2; //Top left square top triangle
            int tri5 = tri4 + 1;
            int tri6 = tri5 + 1;
            return (triNormal[tri1] + triNormal[tri2] + triNormal[tri3] + triNormal[tri4] + triNormal[tri5] + triNormal[tri6]).normalized;
        }

    }
    Vector3 TrianglesAround(int i, int m, int n, Vector3[] triNormal)
    {
        //int[] temp = new int[6];
        int row = i % (n+1);
        int column = i / (n+1);


        if(row == 0)
        {
            if(column == 0)
            {
                int tri1 = 0;
                int tri2 = 1;
                return (triNormal[tri1] + triNormal[tri2]).normalized;
            }
            else if(column == m)
            {
                int tri1 = (n * 2) * (m - 1) + 1;
                return triNormal[tri1].normalized;
            }
            else
            {
                int tri1 = (column - 1) * (n * 2) + 1;
                int tri2 = tri1 + n*2;
                int tri3 = tri2 - 1;
                return (triNormal[tri1] + triNormal[tri2] + triNormal[tri3]).normalized;
            }
        }
        else if(row == n)
        {
            if(column == 0)
            {
                int tri1 = (n - 1) * 2;
                return triNormal[tri1].normalized;
            }
            else if(column == m)
            {
                int tri1 = n * 2 * (m - 1) + (n - 1) * 2;
                int tri2 = tri1 + 1;
                return (triNormal[tri1] + triNormal[tri2]).normalized;
            }
            else
            {
                int tri1 = (row - 1) * 2 + (column - 1) * n * 2 + 1;
                int tri2 = tri1 + 1;
                int tri3 = tri1 + n*2;
                return (triNormal[tri1] + triNormal[tri2] + triNormal[tri3]).normalized;
            }
        }
        else if(column == 0 && row != 0 && row != n)
        {
            int tri1 = (row - 1) * 2;
            int tri2 = tri1 + 1;
            int tri3 = tri2 + 2;
            return (triNormal[tri1] + triNormal[tri2] + triNormal[tri3]).normalized;
        }
        else if(column == m && row != 0 && row != n)
        {
            int tri1 = (row - 1) * 2 + (column - 1) * n * 2;
            int tri2 = tri1 + 1;
            int tri3 = tri2 + 2;
            return (triNormal[tri1] + triNormal[tri2] + triNormal[tri3]).normalized;
        }
        //Triangles for when point is in the middle
        else
        {
            int tri1 = (row - 1) * 2 + (column - 1) * n * 2; //Bot left square top triangle
            int tri2 = tri1 + 1;
            int tri3 = tri2 + 1;
            int tri4 = tri2 + n * 2; //Top left square top triangle
            int tri5 = tri4 + 1;
            int tri6 = tri5 + 1;
            return (triNormal[tri1] + triNormal[tri2] + triNormal[tri3] + triNormal[tri4] + triNormal[tri5] + triNormal[tri6]).normalized;
        }
        

        
    }











    public GameObject[] mControllers;

    void InitControllers(Vector3[] v)
    {
        mControllers = new GameObject[v.Length];
        for (int i =0; i<v.Length; i++ )
        {
            mControllers[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            mControllers[i].tag = "Sphere";
            mControllers[i].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            mControllers[i].transform.localPosition = v[i];
            mControllers[i].transform.parent = this.transform.GetChild(0);
        }
    }
}
