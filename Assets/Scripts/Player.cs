using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ICharacter
{
    [SerializeField] private float _health;
    public event ICharacter.Died OnDied;

    private void OnDestroy()
    {
        OnDied?.Invoke(this.gameObject);
    }
}
