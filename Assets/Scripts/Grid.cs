using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private CellBundle _cellBundle;
    [SerializeField] private CellFactory _cellFactory;
    [SerializeField] private int _rowCount;
    [SerializeField] private int _columnCount;
    [SerializeField] private float _cellXOffset;
    [SerializeField] private float _cellZOffset;
    [SerializeField] private float _cellRotation;
    [SerializeField] private float _newRowXOffset;

    public Cell GetCellByCoordinates(int row, int column)
    {
        if (row > 0 && column > 0 && row < _cellBundle.Cells.Count / _rowCount && column < _cellBundle.Cells.Count / _columnCount)
        {
            return _cellBundle.Cells[row * _rowCount + column];
        }
        else
        {
            throw new System.Exception("Cell index out of bounds!");
        }
    }
    private void Start()
    {
        _cellFactory.SetUpCells(_rowCount, _columnCount, _cellXOffset, _cellZOffset, _cellRotation, _newRowXOffset, Vector3.zero);
    }
}
