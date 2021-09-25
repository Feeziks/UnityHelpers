using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityHelpers
{
  [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
  public class HexMesh : MonoBehaviour
  {
    #region Properties / Fields

    Mesh hexMesh;
    List<Vector3> vertices;
    List<int> triangles;
    HexOrientation orientation;

    #endregion

    #region Unity Methods

    private void Awake()
    {
      GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
      hexMesh.name = "Hex mesh";
      vertices = new List<Vector3>();
      triangles = new List<int>();
    }

    #endregion

    #region Public Methods

    public void Triangulate(HexCell[] cells)
    {
      hexMesh.Clear();
      vertices.Clear();
      triangles.Clear();

      for(int i = 0; i < cells.Length; i++)
      {
        Triangulate(cells[i]);
      }

      hexMesh.vertices = vertices.ToArray();
      hexMesh.triangles = triangles.ToArray();
      hexMesh.RecalculateNormals();
    }

    public void SetOrientation(HexOrientation o)
    {
      orientation = o;
    }

    #endregion

    #region Private Methods

    private void Triangulate (HexCell cell)
    {
      Vector3 center = cell.transform.localPosition;
      for(int i = 0; i < 6; i++)
      {
        AddTriangle(center, center + HexMetrics.corners[orientation][i], center + HexMetrics.corners[orientation][i + 1]);
      }
      
    }

    private void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
    {
      int vertexIndex = vertices.Count;
      vertices.Add(v1);
      vertices.Add(v2);
      vertices.Add(v3);
      triangles.Add(vertexIndex);
      triangles.Add(vertexIndex + 1);
      triangles.Add(vertexIndex + 2);
    }

    #endregion
  }
}
