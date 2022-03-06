using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        Vector3 direction;
        if (player)
        {
            direction = player.transform.position - transform.position;
        }
        else
        {
            direction = _target.transform.position - transform.position;
        }
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
        OnDirectionChanged?.Invoke(minDirection);
        return minDistanceCell;
    }
}
