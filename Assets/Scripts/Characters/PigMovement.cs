using UnityEngine;

public class PigMovement : MovementByCells
{
    private void Awake()
    {
        OnPointReached += PointReached;
        _rigidbody = GetComponent<Rigidbody>();
    }

    public override void Initialise()
    {
        SpawnObjectOnCoordinates(_startCellPositionY, _startCellPositionX, _grid);
        StartMoveToCell(GetNewRandomTargetCell(_grid));
    }

    private void PointReached()
    {
        _currentCell = _targetCell;
        StartMoveToCell(GetNewRandomTargetCell(_grid));
    }
}
