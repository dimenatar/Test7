using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : Movement
{
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void MovePlayer(Vector2 direction)
    {
        _rigidbody.velocity = new Vector3(direction.x, 0, direction.y) * _speed;
        GetDirection(direction);
    }

    private void GetDirection(Vector2 force)
    {
        if (force == Vector2.zero) return;
        if (force.x > 0)
        {
            if (force.y > 0)
            {
                if (force.x > force.y)
                {
                    OnDirectionChanged?.Invoke(Directions.Right);
                }
                else
                {
                    OnDirectionChanged?.Invoke(Directions.Top);
                }
            }
            else
            {
                if (force.x > Mathf.Abs(force.y))
                {
                    OnDirectionChanged?.Invoke(Directions.Right);
                }
                else
                {
                    OnDirectionChanged?.Invoke(Directions.Bottom);
                }
            }
        }
        else
        {
            if (force.y > 0)
            {
                if (Mathf.Abs(force.x) > force.y)
                {
                    OnDirectionChanged?.Invoke(Directions.Left);
                }
                else
                {
                    OnDirectionChanged?.Invoke(Directions.Top);
                }
            }
            else
            {
                if (Mathf.Abs(force.x) > Mathf.Abs(force.y))
                {
                    OnDirectionChanged?.Invoke(Directions.Left);
                }
                else
                {
                    OnDirectionChanged?.Invoke(Directions.Bottom);
                }
            }
        }
    }
}
