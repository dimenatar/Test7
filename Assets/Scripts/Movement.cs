using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] protected float _speed;

    public delegate void DirectionChanged(Directions direction);
    public DirectionChanged OnDirectionChanged;

}
