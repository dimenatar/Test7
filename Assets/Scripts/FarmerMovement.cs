using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FarmerMovement : MovementByCells
{
    [SerializeField] private GameObject _target;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        OnPointReached += PointReached;
    }

    private void Start()
    {
        SpawnObjectOnCoordinates(_startCellPositionY, _startCellPositionX, _grid);
        StartMoveToCell(GetClosestCellToPlayer(GetDirectionToPlayer(_target), _target));
    }

    public void UpdatePlayer(GameObject player)
    {
        _target = player;
    }

    private void PointReached()
    {
        Debug.LogWarning("point reached:" + _targetCell.CellCoordinates[0] + " " + _targetCell.CellCoordinates[1]);
        _currentCell = _targetCell;
        StartMoveToCell(GetClosestCellToPlayer(GetDirectionToPlayer(_target), _target));
    }

    private Dictionary<Cell, Directions> TryAddCell(Dictionary<Cell, Directions> directions, Cell cell, Directions direction)
    {
        if (cell != null)
        {
            directions.Add(cell, direction);
        }
        return directions;
    }

    private Dictionary<Cell, Directions> GetDirectionToPlayer(GameObject player)
    {
        Vector3 direction = player.transform.position - transform.position;
        Dictionary<Cell, Directions> directionToCells = new Dictionary<Cell, Directions>();
        if (direction.x < 0)
        {
            directionToCells = TryAddCell(directionToCells, GetNextCellByDirection(_grid, _currentCell, Directions.Left), Directions.Left);
            
        }
        else
        {
            directionToCells = TryAddCell(directionToCells, GetNextCellByDirection(_grid, _currentCell, Directions.Right), Directions.Right);
        }
        if (direction.z < 0)
        {
            directionToCells = TryAddCell(directionToCells, GetNextCellByDirection(_grid, _currentCell, Directions.Bottom), Directions.Bottom);
        }
        else
        {
            directionToCells = TryAddCell(directionToCells, GetNextCellByDirection(_grid, _currentCell, Directions.Top), Directions.Top);
        }
        directionToCells.RemoveAll((key, value) => key.CellType == CellTypes.Stone);

        Debug.Log("directions: ");
        foreach (var item in directionToCells.Keys)
        {
            Debug.Log(item.CellCoordinates[0] + " " + item.CellCoordinates[1]);
        }

        return directionToCells;
    }

    private Cell GetClosestCellToPlayer(Dictionary<Cell, Directions> neighbourCells, GameObject player)
    {
        float minDistance = Vector3.Distance(neighbourCells.ElementAt(0).Key.CellPosition, player.transform.position);
        Cell minDistanceCell = neighbourCells.ElementAt(0).Key;
        Directions minDirection = neighbourCells.ElementAt(0).Value;
        for (int i = 1; i < neighbourCells.Count; i++)
        {
            float distance = Vector3.Distance(neighbourCells.ElementAt(i).Key.CellPosition, player.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                minDistanceCell = neighbourCells.ElementAt(i).Key;
                minDirection = neighbourCells.ElementAt(i).Value;
            }
        }
        //Debug.Log(minDistanceCell.CellCoordinates[0] + " " + minDistanceCell.CellCoordinates[1]);
        OnDirectionChanged?.Invoke(minDirection);
        return minDistanceCell;
    }
}
