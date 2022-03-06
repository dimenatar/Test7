using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour, ICharacter
{
    [SerializeField] private int _health;

    public event ICharacter.Died OnDied;
    public event ICharacter.DamageTaken OnDamageTaken;

    public int Health => _health;

    public void TakeDamage(int amount)
    {
        _health -= amount;
        OnDamageTaken?.Invoke(_health);
        if (_health <= 0)
        {
            OnDied?.Invoke(gameObject);
        }
    }

    private void OnDestroy()
    {
        OnDied?.Invoke(this.gameObject);
    }
}
