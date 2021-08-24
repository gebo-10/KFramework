using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testmesh : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Mesh mesh = GetComponent<MeshFilter>().mesh;
        //mesh.Clear();

        ////设置顶点
        //mesh.vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0) };
        ////设置三角形顶点顺序，顺时针设置
        //mesh.triangles = new int[] { 0, 1, 2 };
        ////mesh.normals = new Vector3[] { new Vector3(0, 0, 1) };
        //DrawCircle(1,5, new Vector3(0, 0, 0));

        DrawRing(1, 0.7f, 10, new Vector3(0, 0, 0));
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.RecalculateNormals();
    }

    #region 画三角形
    void DrawTriangle()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();

        //设置顶点
        mesh.vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0) };
        //设置三角形顶点顺序，顺时针设置
        mesh.triangles = new int[] { 0, 1, 2 };
    }
    #endregion

    #region 画正方形
    void DrawSquare()
    {

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();

        mesh.vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0), new Vector3(1, 0, 0) };
        mesh.triangles = new int[]
        { 0, 1, 2,
          0, 2, 3
        };
    }
    #endregion

    #region 画圆
    /// <summary>
    /// 画圆
    /// </summary>
    /// <param name="radius">圆的半径</param>
    /// <param name="segments">圆的分割数</param>
    /// <param name="centerCircle">圆心得位置</param>
    void DrawCircle(float radius, int segments, Vector3 centerCircle)
    {

        //顶点
        Vector3[] vertices = new Vector3[segments + 1];
        vertices[0] = centerCircle;
        float deltaAngle = Mathf.Deg2Rad * 360f / segments;
        float currentAngle = 0;
        for (int i = 1; i < vertices.Length; i++)
        {
            float cosA = Mathf.Cos(currentAngle);
            float sinA = Mathf.Sin(currentAngle);
            vertices[i] = new Vector3(cosA * radius + centerCircle.x, sinA * radius + centerCircle.y, 0);
            currentAngle += deltaAngle;
        }

        //三角形
        int[] triangles = new int[segments * 3];
        for (int i = 0, j = 1; i < segments * 3 - 3; i += 3, j++)
        {
            triangles[i] = 0;
            triangles[i + 1] = j + 1;
            triangles[i + 2] = j;
        }
        triangles[segments * 3 - 3] = 0;
        triangles[segments * 3 - 2] = 1;
        triangles[segments * 3 - 1] = segments;


        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }
    #endregion

    #region 画圆环
    /// <summary>
    /// 画圆环
    /// </summary>
    /// <param name="radius">圆半径</param>
    /// <param name="innerRadius">内圆半径</param>
    /// <param name="segments">圆的分个数</param>
    /// <param name="centerCircle">圆心坐标</param>
    void DrawRing(float radius, float innerRadius, int segments, Vector3 centerCircle)
    {

        //顶点
        Vector3[] vertices = new Vector3[segments * 2];
        float deltaAngle = Mathf.Deg2Rad * 360f / segments;
        float currentAngle = 0;
        for (int i = 0; i < vertices.Length; i += 2)
        {
            float cosA = Mathf.Cos(currentAngle);
            float sinA = Mathf.Sin(currentAngle);
            vertices[i] = new Vector3(cosA * innerRadius + centerCircle.x, sinA * innerRadius + centerCircle.y, 0);
            vertices[i + 1] = new Vector3(cosA * radius + centerCircle.x, sinA * radius + centerCircle.y, 0);
            currentAngle += deltaAngle;
        }

        //三角形
        int[] triangles = new int[segments * 6];
        for (int i = 0, j = 0; i < segments * 6; i += 6, j += 2)
        {
            triangles[i] = j;
            triangles[i + 1] = (j + 1) % vertices.Length;
            triangles[i + 2] = (j + 3) % vertices.Length;

            triangles[i + 3] = j;
            triangles[i + 4] = (j + 3) % vertices.Length;
            triangles[i + 5] = (j + 2) % vertices.Length;
        }

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }
    #endregion

}
