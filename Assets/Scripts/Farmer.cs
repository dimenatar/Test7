using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : MonoBehaviour, ICharacter
{
    [SerializeField] private int _health;
    [SerializeField] private FarmerMovement _farmerMovement;
    [SerializeField] private SpriteDirectionController _spriteController;
    [SerializeField] private FarmerSprites _farmerSprites;
    [SerializeField] private float _angrySpeedMultiplier;
    [SerializeField] private PigController _pigController;

    private bool _isAngry;

    public int Health => _health;

    public event ICharacter.Died OnDied;
    public event ICharacter.DamageTaken OnDamageTaken;

    public void ChangeTarget()
    {
        _farmerMovement.UpdatePlayer(_pigController.SetNewPlayer());
    }

    public void KilledPig(GameObject pig)
    {
        _pigController.ReducePigCount(pig);
    }

    public void BecomeAngry()
    {
        if (!_isAngry)
        {
            _isAngry = true;
            _farmerMovement.SetMultiplier(_angrySpeedMultiplier);
            _spriteController.SetSpriteBundle(_farmerSprites.AngryTopView, _farmerSprites.AngryRightView, _farmerSprites.AngryBottomView, _farmerSprites.AngryLeftView);
        }
    }

    public void StopBeingAngry()
    {
        _isAngry = false;
        _farmerMovement.SetMultiplier(1);
        _spriteController.SetSpriteBundle(_farmerSprites.NormalTopView, _farmerSprites.NormalRightView, _farmerSprites.NormalBottomView, _farmerSprites.NormalLeftView);
    }

    public void TakeDamage(int amount)
    {
        _health -= amount;
        OnDamageTaken?.Invoke(_health);
        if (_health < 0)
        {
            OnDied?.Invoke(gameObject);
        }
    }

    private void SetNewTarget()
    {

    }
}