using System;
using UnityEngine;

[Serializable]
public class CellContent
{
    [SerializeField] private GameObject _contentPrefab;
    [SerializeField] private CellTypes _cellType;

    public GameObject ContentPrefab => _contentPrefab;
    public CellTypes CellType => _cellType;
}
