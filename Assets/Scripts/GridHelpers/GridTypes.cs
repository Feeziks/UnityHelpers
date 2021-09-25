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
    HexagonalOffsetCoordinates
  }

  #endregion

  #region Structs

  [System.Serializable]
  public struct HexCoordinates
  {
    public int x { get; private set; }
    public int z { get; private set; }

    public HexCoordinates(int m_x, int m_z)
    {
      x = m_x;
      z = m_z;
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
      new Vector3(-innerRadius, 0f, 0.5f * outerRadius),
      new Vector3(0f, 0f, outerRadius),
      new Vector3(innerRadius, 0f, 0.5f * outerRadius),
      new Vector3(innerRadius, 0f, -0.5f * outerRadius),
      new Vector3(0f, 0f, -outerRadius),
      new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
      new Vector3(-innerRadius, 0f, 0.5f * outerRadius)
    };

    public static readonly Dictionary<HexOrientation, Vector3[]> corners = new Dictionary<HexOrientation, Vector3[]>
    {
      {HexOrientation.PointUp, PointUpCorners },
      {HexOrientation.FlatUp, FlatUpCorners }
    };

  }

  #endregion
}
