using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] protected float _speed;

    public float Speed { get => _speed; set => _speed = value >= 0 ? value : 0; }

    public delegate void DirectionChanged(Directions direction);
    public DirectionChanged OnDirectionChanged;

}
