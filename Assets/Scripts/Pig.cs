using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour, ICharacter
{
    [SerializeField] private float _health;
    public event ICharacter.Died OnDied;
    public event ICharacter.DamageTaken OnDamageTaken;

    public void TakeDamage(int amount)
    {
        OnDamageTaken?.Invoke(amount);
        _health -= amount;
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
