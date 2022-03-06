using System;
using UnityEngine;

[Serializable]
public class Cell
{
    [SerializeField] private CellTypes _cellType;
    private Vector3 _cellPosition;
    private int[] _cellCoordinates;

    public Vector3 CellPosition { get => _cellPosition; set => _cellPosition = value; }
    public int[] CellCoordinates { get => _cellCoordinates; set => _cellCoordinates = value; }
    public CellTypes CellType => _cellType; 
}
