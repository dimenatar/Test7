using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Cell Bundle", menuName = "Cell Bundle", order = 42)]
public class CellBundle : ScriptableObject
{
    [SerializeField] private List<Cell> _cells;

    public List<Cell> Cells => _cells;
}
