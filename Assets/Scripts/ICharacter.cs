using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    public delegate void Died(GameObject gameObject);
    public event Died OnDied;
}
