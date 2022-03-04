using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CellFactory : MonoBehaviour
{
    [SerializeField] private CellBundle _cellBundle;
    [SerializeField] private CellContentBundle _cellContentBundle;
    [SerializeField] private GameObject _firstCube;

    public void SetUpCells(int rowCount, int columnCount, float cellXOffset, float cellYOffset, float rotation, float newrowXOffset, Vector3 firstCubePosition)
    {
        if (_cellBundle.Cells.Count < rowCount * columnCount) throw new System.Exception("not enough cells");
        Cell cell;
        GameObject cellBox;
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                cell = _cellBundle.Cells[i * columnCount + j];
                cellBox = Instantiate(_cellContentBundle.CellContents.Where(content => content.CellType == cell.CellType).Select(pref => pref.ContentPrefab).FirstOrDefault(), new Vector3(j * cellXOffset + _firstCube.transform.position.x + newrowXOffset * i, 0, i * cellYOffset + _firstCube.transform.position.z), Quaternion.Euler(0, rotation, 0));
                cellBox.name = "Cell " + (i * columnCount + j).ToString();
            }
        }
    }
}
