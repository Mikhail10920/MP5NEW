using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MyMesh : MonoBehaviour {
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
        int[] temp = CalculateTriangles(m, n);
        Vector3[] triNormal = new Vector3[m * n * 2];
        for (int i = 0; i < triNormal.Length; i++)
        {
            triNormal[i] = FaceNormal(v, temp[0 + 3 * i], temp[1 + 3 * i], temp[2 + 3 * i]);
        }
        for(int i = 0; i < normal.Length; i++)
        {
            
            normal[i] = TrianglesAround(i, m, n, triNormal);
            
            
            
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
                int tri1 = (m * 2) - 1;
                return triNormal[tri1].normalized;
            }
            else
            {
                int tri1 = (column - 1) * 2 + 1;
                int tri2 = tri1 + 1;
                int tri3 = tri2 + 1;
                return (triNormal[tri1] + triNormal[tri2] + triNormal[tri3]).normalized;
            }
        }
        else if(row == n)
        {
            if(column == 0)
            {
                int tri1 = m * 2 * (n - 1);
                return triNormal[tri1].normalized;
            }
            else if(column == m)
            {
                int tri1 = m * 2 * (n - 1) + (m - 1) * 2;
                int tri2 = tri1 + 1;
                return (triNormal[tri1] + triNormal[tri2]).normalized;
            }
            else
            {
                int tri1 = (column - 1) * 2 + (row - 1) * m * 2 + 1;
                int tri2 = tri1 + 1;
                int tri3 = tri2 + 1;
                return (triNormal[tri1] + triNormal[tri2] + triNormal[tri3]).normalized;
            }
        }
        else if(column == 0 && row != 0 && row != n)
        {
            int tri1 = (row - 1) * m * 2;
            int tri2 = tri1 + m * 2;
            int tri3 = tri2 + 1;
            return (triNormal[tri1] + triNormal[tri2] + triNormal[tri3]).normalized;
        }
        else if(column == m && row != 0 && row != n)
        {
            int tri1 = (column - 1) * 2 + (row - 1) * m * 2;
            int tri2 = tri1 + 1;
            int tri3 = tri2 + m * 2;
            return (triNormal[tri1] + triNormal[tri2] + triNormal[tri3]).normalized;
        }
        //Triangles for when point is in the middle
        else
        {
            int tri1 = (column - 1) * 2 + (row - 1) * m * 2; //Bot left square top triangle
            int tri2 = tri1 + 1;
            int tri3 = tri2 + 1;
            int tri4 = tri2 + m * 2; //Top left square top triangle
            int tri5 = tri4 + 1;
            int tri6 = tri5 + 1;
            return (triNormal[tri1] + triNormal[tri2] + triNormal[tri3] + triNormal[tri4] + triNormal[tri5] + triNormal[tri6]).normalized;
        }
        

        
    }
}
