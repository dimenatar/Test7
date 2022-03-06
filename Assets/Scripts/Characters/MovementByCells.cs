using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class MovementByCells : Movement, IGroundMove
{
    [SerializeField] protected Grid _grid;
    [SerializeField] protected int _startCellPositionX;
    [SerializeField] protected int _startCellPositionY;
    [SerializeField] protected List<CellTypes> _allowedCellTypesToSpawn;
    [SerializeField] private float _distance;

    private float _speedMultiplier = 1;

    public Cell _currentCell;
    public Cell _targetCell;
    protected Rigidbody _rigidbody;

    public event Action OnPointReached;

    public virtual void Initialise()
    {

    }

    public void SpawnObjectOnCoordinates(int row, int column, Grid grid)
    {
        _currentCell = grid.GetCellByCoordinates(row, column);
        if (_currentCell.CellType == CellTypes.Stone)
        {
            _currentCell = grid.GetFreeCell(row, column, grid, _allowedCellTypesToSpawn);
            if (_currentCell == null)
            {
                throw new System.Exception("cant find allowed cell to spawn!");
            }
        }
        transform.position = _currentCell.CellPosition;
    }

    public Directions GetRandomDirection()
    {
        return (Directions)Random.Range(0, 3);
    }

    public Cell GetNextCellByDirection (Grid grid, Cell current, Directions direction)
    {
        
        switch (direction)
        {
            case Directions.Top:
                {
                    return grid.GetCellByCoordinates(current.CellCoordinates[0] - 1, current.CellCoordinates[1]);
                }
            case Directions.Right:
                {
                    return grid.GetCellByCoordinates(current.CellCoordinates[0], current.CellCoordinates[1] + 1);
                }
            case Directions.Bottom:
                {
                    return grid.GetCellByCoordinates(current.CellCoordinates[0] + 1, current.CellCoordinates[1]);
                }
            case Directions.Left:
                {
                    return grid.GetCellByCoordinates(current.CellCoordinates[0], current.CellCoordinates[1] - 1);
                }
            default:
                {
                    return null;
                }
        }
    }

    public bool CheckRoute(Directions direction, Cell currentCell, Grid grid)
    {
        switch (direction)
        {
            case Directions.Top:
                {
                    if (currentCell.CellCoordinates[0] - 1 < 0)
                    {
                        return false;
                    }
                    else
                    {
                        if (grid.GetCellByCoordinates(currentCell.CellCoordinates[0]-1, currentCell.CellCoordinates[1]).CellType == CellTypes.Stone)
                        {
                            return false;
                        }
                    }
                    return true;
                }
            case Directions.Right:
                {
                    if (currentCell.CellCoordinates[1] + 1 >= grid.ColumnCount)
                    {
                        return false;
                    }
                    else
                    {
                        if (grid.GetCellByCoordinates(currentCell.CellCoordinates[0], currentCell.CellCoordinates[1] + 1).CellType == CellTypes.Stone)
                        {
                            return false;
                        }
                    }
                    return true;
                }
            case Directions.Bottom:
                {
                    if (currentCell.CellCoordinates[0] + 1 >= grid.RowCount)
                    {
                        return false;
                    }
                    else
                    {
                        if (grid.GetCellByCoordinates(currentCell.CellCoordinates[0] + 1, currentCell.CellCoordinates[1]).CellType == CellTypes.Stone)
                        {
                            return false;
                        }
                    }
                    return true;
                }
            case Directions.Left:
                {
                    if (currentCell.CellCoordinates[1] - 1 < 0)
                    {
                        return false;
                    }
                    else
                    {
                        if (grid.GetCellByCoordinates(currentCell.CellCoordinates[0], currentCell.CellCoordinates[1] - 1).CellType == CellTypes.Stone)
                        {
                            return false;
                        }
                    }
                    return true;
                }
                default:
                {
                    return false;
                }
        }

    }

    public void SetMultiplier(float multiplier)
    {
        _speedMultiplier = multiplier;
    }

    public Cell GetNewRandomTargetCell(Grid grid)
    {
        int maxIterations = 10000;
        int currentIterations = 0;
        Directions direction = GetRandomDirection();
        while (!CheckRoute(direction, _currentCell, _grid))
        {
            direction = GetRandomDirection();
            currentIterations++;
            if (currentIterations >= maxIterations) throw new System.Exception("while true");
        }
        OnDirectionChanged?.Invoke(direction);
        return GetNextCellByDirection(grid, _currentCell, direction);
    }

    public void StartMoveToCell(Cell cell)
    {
        _targetCell = cell;
        StartCoroutine(nameof(MoveToPoint));
    }

    public IEnumerator MoveToPoint()
    {
        while (true)
        {
            if (Mathf.Abs(Vector3.Distance(transform.position, _targetCell.CellPosition)) > _distance)
            {
                _rigidbody.velocity = (_targetCell.CellPosition - transform.position).normalized * _speed * _speedMultiplier;
            }
            else
            {
                StopCoroutine(nameof(MoveToPoint));
                OnPointReached?.Invoke();
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
