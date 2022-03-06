using UnityEngine;

public class FarmerView : CharacterView
{
    [SerializeField] private Farmer _farmer;

    private void Awake()
    {
        _farmer.OnDamageTaken += _healthBar.SetSliderValue;
    }

    private void Start()
    {
        _healthBar.SetSliderMaxValue(_farmer.Health);
    }
}
