using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Farmer : MonoBehaviour, ICharacter
{
    [SerializeField] private int _health;
    [SerializeField] private FarmerMovement _farmerMovement;
    [SerializeField] private SpriteDirectionController _spriteController;
    [SerializeField] private FarmerSprites _farmerSprites;
    [SerializeField] private float _angrySpeedMultiplier;
    [SerializeField] private PigController _pigController;
    [SerializeField] private GameObject _player;

    private List<ICharacter> _foundPigs = new List<ICharacter>();
    private bool _isAngry;

    public int Health => _health;

    public event ICharacter.Died OnDied;
    public event ICharacter.DamageTaken OnDamageTaken;

    private void Awake()
    {
        _farmerMovement.UpdatePlayer(_player);
    }

    public void ChangeTarget(GameObject target)
    {
        if (target != null)
        {
            _farmerMovement.UpdatePlayer(target);
        }
    }

    public void FoundPig(ICharacter pig)
    {
        if (!_foundPigs.Contains(pig))
        {
            _foundPigs.Add(pig);
        }
        BecomeAngry();
    }

    public void RemovePig(ICharacter pig)
    {
        if (_foundPigs.Contains(pig))
        {
            _foundPigs.Remove(pig);
            if (_foundPigs.Count == 0)
            {
                StopBeingAngry();
            }
        }
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
}