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

}
