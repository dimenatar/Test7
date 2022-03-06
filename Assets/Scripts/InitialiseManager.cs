using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialiseManager : MonoBehaviour
{
    [SerializeField] private Grid _grid;
    [SerializeField] private List<MovementByCells> _movementByCells;

    private void Start()
    {
        _grid.Initialise();
        foreach (var item in _movementByCells)
        {
            item.Initialise();
        }
    }
}
