using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Content Bundle", menuName = "Content Bundle", order = 43)]
public class CellContentBundle : ScriptableObject
{
    [SerializeField] List<CellContent> _cellContents;

    public List<CellContent> CellContents => _cellContents;
}
