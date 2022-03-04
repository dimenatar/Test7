using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICellContent 
{
    public CellTypes CellType { get;}
    public GameObject CreateContet();
}
