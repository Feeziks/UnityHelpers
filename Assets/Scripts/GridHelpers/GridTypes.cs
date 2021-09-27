using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//https://catlikecoding.com/unity/tutorials/hex-map/part-1/

namespace UnityHelpers
{
  #region Enums

  public enum GridType
  {
    Square,
    Hex,
    Other
  }

  public enum GridOrientation
  {
    Perspective,
    Isometric
  }

  public enum HexOrientation
  {
    PointUp,
    FlatUp
  }

  public enum HexGridIndexType
  {
    SouthWest,
    SouthEast,
    NorthEast,
    NorthWest,
    Axial
  }

  #endregion

  #region Structs

  [System.Serializable]
  public struct HexCoordinates
  {
    [SerializeField]
    private int m_x, m_z;
    public int x { get { return m_x; } }
    public int z { get { return m_z; } }
    public int y { get { return -m_x - m_z; } }

    public HexCoordinates(int xx, int zz)
    {
      m_x = xx;
      m_z = zz;
    }

    public static HexCoordinates FromOffsetCoordinates(int x, int z)
    {
      return new HexCoordinates(x - z / 2, z);
    }

    public override string ToString()
    {
      return "(" + x.ToString() + ", " + y.ToString() + ", " + z.ToString() + ")";
    }

    public string ToStringLineBreak()
    {
      return x.ToString() + "\n" + y.ToString() + "\n" + z.ToString();
    }
  }

  #endregion

  #region Classes

  public static class HexMetrics
  {
    public const float outerRadius = 10f;
    public const float innerRadius = outerRadius * 0.866025404f;

    private static Vector3[] PointUpCorners =
    {
      new Vector3(0f, 0f, outerRadius),
      new Vector3(innerRadius, 0f, 0.5f * outerRadius),
      new Vector3(innerRadius, 0f, -0.5f * outerRadius),
      new Vector3(0f, 0f, -outerRadius),
      new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
      new Vector3(-innerRadius, 0f, 0.5f * outerRadius),
      new Vector3(0f, 0f, outerRadius)
    };

    private static Vector3[] FlatUpCorners =
    {
      new Vector3(0f, 0f, -outerRadius),
      new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
      new Vector3(-innerRadius, 0f, 0.5f * outerRadius),
      new Vector3(0f, 0f, outerRadius),
      new Vector3(innerRadius, 0f, 0.5f * outerRadius),
      new Vector3(innerRadius, 0f, -0.5f * outerRadius),
      new Vector3(0f, 0f, -outerRadius)
    };

    public static readonly Dictionary<HexOrientation, Vector3[]> corners = new Dictionary<HexOrientation, Vector3[]>
    {
      {HexOrientation.PointUp, PointUpCorners },
      {HexOrientation.FlatUp, FlatUpCorners }
    };

  }

  #endregion
}
