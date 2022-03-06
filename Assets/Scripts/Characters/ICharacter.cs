using UnityEngine;

public interface ICharacter
{
    public delegate void Died(GameObject gameObject);
    public event Died OnDied;

    public delegate void DamageTaken(int amount);
    public event DamageTaken OnDamageTaken;

    public void TakeDamage(int amount);
}
