using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New grid data", menuName = "Grid data", order = 41)]
public class GridData : ScriptableObject
{
    [SerializeField] private int _rowCount;
    [SerializeField] private int _columnCount;

    public int RowCount => _rowCount;
    public int ColumnCount => _columnCount;
}
