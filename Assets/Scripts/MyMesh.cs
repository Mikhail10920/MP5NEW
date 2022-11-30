using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MyMesh : MonoBehaviour {

	// Use this for initialization
    public int m = 2;
    public int n = 2;

    [SerializeField] GameObject emptyObj;
    [SerializeField] GameObject axisContrl;

    void Start () {

        GameObject ObjectsInside = Instantiate(emptyObj);
        ObjectsInside.transform.parent = this.transform;

        ChangeResolution( m, n);

        //Vector3[] arrOne;
        //Vector3[,] arrTwo;
        //Vector2[] arrThree;
        //Vector3[] normalArray = new Vector3[(m + 1)* (n + 1)];
        
        


        //Mesh theMesh = GetComponent<MeshFilter>().mesh;   // get the mesh component
        //theMesh.Clear();    // delete whatever is there!!

        //CalculateVertice(out arrOne, out arrTwo, out arrThree, m, n);
        //theMesh.vertices = arrOne;

        //theMesh.triangles = CalculateTriangles(m, n);
        //for(int i = 0; i < normalArray.Length; i++)
        //{
        //    normalArray[i] = new Vector3(0, 1, 0);
        //}
        //theMesh.normals = normalArray;
        //theMesh.uv = arrThree;
        //theMesh.uv2 = arrThree;

        //InitControllers(arrOne);
        //InitNormals(arrOne, normalArray);

    }

    // Update is called once per frame
    void Update () {
        
        Mesh theMesh = GetComponent<MeshFilter>().mesh;
        Vector3[] v = theMesh.vertices;
        Vector3[] normal = theMesh.normals;
        for (int i = 0; i<mControllers.Length; i++)
        {
            v[i] = mControllers[i].transform.localPosition;
        }

        ComputeNormals(v, normal, m, n);

        theMesh.vertices = v;
        theMesh.normals = normal;
        

    }

    void CalculateVertice(out Vector3[] arrOne, out Vector3[,] arrTwo, out Vector2[] arrThree, float m, float n)
    {
        int size = (int)((m + 1) * (n + 1));
        arrOne = new Vector3[size];
        arrTwo = new Vector3[(int)(m+1), (int)(n + 1)];
        arrThree = new Vector2[size];
        int counter = 0;

        for (int i = 0; i < m + 1; i++)
        {
            for (int b = 0; b < n + 1; b++)
            {
                float x = -1 + (2 / m) * (i);
                float y = -1 + (2 / n) * (b);

                Vector3 newVect = new Vector3(x, 0, y);
                Vector2 newVect2 = new Vector2((x + 1) / 2, (y + 1) / 2);
                arrThree[counter] = newVect2;
                arrOne[counter] = newVect;
                arrTwo[i, b] = newVect;
                counter++;
            }
        }
    }

    int[] CalculateTriangles( int m, int n)
    {
        int[] t = new int[(m * n) * 2 * 3];

        int counter = 0;

        for (int i = 0; i < n; i++)
        {
            for (int b = 0; b < m; b++)
            {
                t[counter] = i + b * (n + 1);
                //Debug.Log(counter + ", " + t[counter] + ", " + i + ", " + b);
                counter++;
                t[counter] = (i + 1) + b * (n + 1);
                //Debug.Log(counter + ", " + t[counter] + ", " + i + ", " + b);
                counter++;
                t[counter] = (i + 1) + (b + 1) * (n + 1);
                //Debug.Log(counter + ", " + t[counter] + ", " + i + ", " + b);
                counter++;
                
                

                t[counter] = i + b * (n + 1);
                //Debug.Log(counter + ", " + t[counter] + ", " + i + ", " + b);
                counter++;
                t[counter] = (i + 1) + (b + 1) * (n + 1);
                //Debug.Log(counter + ", " + t[counter] + ", " + i + ", " + b);
                counter++;
                t[counter] = i + (b + 1) * (n + 1);
                //Debug.Log(counter + ", " + t[counter] + ", " + i + ", " + b);
                counter++;
            }
        }
        return t;
    }

    public void ChangeResolution(int m, int n)
    {
        DestroyChildern();

        Debug.Log(this.transform.childCount);

        Vector3[] arrOne;
        Vector3[,] arrTwo;
        Vector2[] arrThree;
        Vector3[] normalArray = new Vector3[(m + 1) * (n + 1)];

        Mesh theMesh = GetComponent<MeshFilter>().mesh;   // get the mesh component
        theMesh.Clear();    // delete whatever is there!!

        CalculateVertice(out arrOne, out arrTwo, out arrThree, m, n);
        theMesh.vertices = arrOne;

        theMesh.triangles = CalculateTriangles(m, n);
        for (int i = 0; i < normalArray.Length; i++)
        {
            normalArray[i] = new Vector3(0, 1, 0);
        }
        theMesh.normals = normalArray;
        theMesh.uv = arrThree;
        theMesh.uv2 = arrThree;

        GetComponent<TexturePlacement>().SetUV(arrThree);
        /*
        GameObject ObjectsInside = Instantiate(emptyObj);
        ObjectsInside.transform.parent = this.transform;
        */

        GameObject axisControlerObj = Instantiate(axisContrl);
        axisControlerObj.transform.parent = this.transform.GetChild(0);

        InitControllers(arrOne);
        InitNormals(arrOne, normalArray);

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
}
