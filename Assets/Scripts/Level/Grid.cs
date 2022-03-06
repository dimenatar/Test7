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

    public int RowCount => _rowCount;
    public int ColumnCount => _columnCount;

    public Cell GetCellByCoordinates(int row, int column)
    {
        if (row >= 0 && column >= 0 && row < _rowCount && column < _columnCount)
        {
            return _cellBundle.Cells[row * _columnCount + column];
        }
        else
        {
            return null;
        }
    }

    public Cell GetFreeCell(int row, int column, Grid grid, List<CellTypes> allowedCellTypes)
    {
        if (row < 0 || column < 0) return null;
        if (!allowedCellTypes.Contains(grid.GetCellByCoordinates(row, column).CellType))
        {
            if (row == 0)
            {
                return GetFreeCell(grid.RowCount - 1, column - 1, grid, allowedCellTypes);
            }
            else
            {
                return GetFreeCell(row - 1, column, grid, allowedCellTypes);
            }
        }
        else
        {
            return grid.GetCellByCoordinates(row, column);
        }
    }

    private void Start()
    {
        _cellFactory.SetUpCells(_rowCount, _columnCount, _cellXOffset, _cellZOffset, _cellRotation, _newRowXOffset, Vector3.zero);
    }
}
