using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace UnityHelpers
{
  [CustomPropertyDrawer(typeof(HexCoordinates))]
  public class HexCoordinatesDrawer : PropertyDrawer
  {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      HexCoordinates coordinates = new HexCoordinates(property.FindPropertyRelative("m_x").intValue, property.FindPropertyRelative("m_z").intValue);
      position = EditorGUI.PrefixLabel(position, label);
      GUI.Label(position, coordinates.ToString());
    }
  }
}