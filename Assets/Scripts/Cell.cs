using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Cell
{
    [SerializeField] private CellTypes _cellType;

    public CellTypes CellType => _cellType; 
}
