using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPoolableObject : UnityHelpers.I_poolable
{
  #region Properties / Fields

  public string objectName { get; private set; }
  public GameObject go { get; private set; }

  #endregion

  #region Public Methods

  public void Initialize()
  {
    objectName = "Test Poolable Object";
    go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    go.name = objectName;
    //GameObject.Instantiate(go);
    go.SetActive(false);
  }

  public TestPoolableObject()
  {

  }

  #endregion

  #region Private Methods

  #endregion
}
