using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UnityHelpers
{
  public class HexGrid : MonoBehaviour
  {
    #region Properties / Fields

    public int width;
    public int height;

    public HexCell cellPrefab;
    public TextMeshProUGUI cellLabelPrefab;
    public HexOrientation orientation;

    public Color defaultColor;
    public Color selectedColor;

    private HexMesh hexMesh { get; set; }
    private Canvas gridCanvas { get; set; }
    private HexCell[] cells { get; set; }

    #endregion

    #region Unity Methods

    private void Awake()
    {
      gridCanvas = GetComponentInChildren<Canvas>();
      hexMesh = GetComponentInChildren<HexMesh>();
      hexMesh.SetOrientation(orientation);

      cells = new HexCell[height * width];

      for (int z = 0, i = 0; z < height; z++)
      {
        for (int x = 0; x < width; x++)
        {
          CreateCell(x, z, i++);
        }
      }
    }

    private void Start()
    {
      hexMesh.Triangulate(cells);
    }

    private void Update()
    {
      if(Input.GetMouseButton(0))
      {
        HandleInput();
      }
    }

    #endregion

    #region Public Methods

    #endregion

    #region Private Methods

    private void CreateCell(int x, int z, int i)
    {
      Vector3 position;
      position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
      position.y = 0f;
      position.z = z * (HexMetrics.outerRadius * 1.5f);

      HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
      cell.transform.SetParent(transform, false);
      cell.transform.localPosition = position;
      cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
      cell.color = defaultColor;

      LabelCell(position.x, position.z, x, z, cell);
    }

    private void LabelCell(float xPos, float zPos, int xIdx, int zIdx, HexCell cell)
    {
      TextMeshProUGUI label = Instantiate(cellLabelPrefab);
      label.rectTransform.SetParent(gridCanvas.transform, false);
      label.rectTransform.anchoredPosition = new Vector2(xPos, zPos);
      label.text = cell.coordinates.ToStringLineBreak();
    }

    private void HandleInput()
    {
      Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;
      if(Physics.Raycast(inputRay, out hit))
      {
        TouchCell(hit.point);
      }
    }

    private void TouchCell(Vector3 position)
    {
      position = transform.InverseTransformPoint(position);
      HexCoordinates coordinates = HexCoordinates.FromPosition(position);
      HexCell touchedCell = HexCoordinatesToCell(coordinates);
      touchedCell.color = selectedColor;
      hexMesh.Triangulate(cells);
      Debug.Log("Touched at " + coordinates.ToString());
    }

    private HexCell HexCoordinatesToCell(HexCoordinates coordinates)
    {
      int index = coordinates.x + coordinates.z * width + coordinates.z / 2;
      return cells[index];
    }

    #endregion
  }
}