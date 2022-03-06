using UnityEngine;

public class PigView : CharacterView
{
    [SerializeField] private Pig _pig;

    private void Awake()
    {
        _pig.OnDamageTaken += _healthBar.SetSliderValue;
    }

    private void Start()
    {
        _healthBar.SetSliderMaxValue(_pig.Health);
    }
}
