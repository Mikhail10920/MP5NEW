using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TexturePlacement : MonoBehaviour
{

    public Vector2 Offset = Vector2.zero;
    public Vector2 Scale = Vector2.one;
    public float rotation = 0;

    private Vector2[] initUV = null;

    private void Start()
    {
        //Mesh theMesh = GetComponent<MeshFilter>().mesh;
        //initUV = theMesh.uv;
    }

    public void SetUV(Vector2[] uv)
    {
        initUV = uv;
    }

    // Update is called once per frame
    void Update()
    {
        Matrix3x3 TexMatrix = Matrix3x3Helpers.CreateTRS(Offset, rotation, Scale);
        Mesh theMesh = GetComponent<MeshFilter>().mesh;
        Vector2[] uv = theMesh.uv;
        for (int i = 0; i < uv.Length; i++)
        {
            uv[i] = Matrix3x3.MultiplyVector2(TexMatrix, initUV[i]);
        }
        theMesh.uv = uv;

    }
}
