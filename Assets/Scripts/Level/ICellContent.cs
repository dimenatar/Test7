using UnityEngine;

public interface ICellContent 
{
    public CellTypes CellType { get;}
    public GameObject CreateContet();
}
