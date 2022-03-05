using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteDirectionController : MonoBehaviour
{
    [SerializeField] private Sprite _topView;
    [SerializeField] private Sprite _rightView;
    [SerializeField] private Sprite _bottomView;
    [SerializeField] private Sprite _leftView;
    [SerializeField] private Directions _startDirection;
    [SerializeField] private Movement _movement;

    private SpriteRenderer _spriteRenderer;
    private Dictionary<Directions, Sprite> _spriteByDirecitons;
    private Directions _currentDirection; 

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _currentDirection = _startDirection;
        _movement.OnDirectionChanged += SetCurrentSprite;
    }

    private void Start()
    {
        SetSpriteBundle(_topView, _rightView, _bottomView, _leftView);
    }

    public void SetSpriteBundle(Sprite topView, Sprite rightView, Sprite bottomView, Sprite leftView)
    {
        Dictionary<Directions, Sprite> updatedDictionary = new Dictionary<Directions, Sprite>() { {Directions.Top, topView }, {Directions.Right, rightView },
        {Directions.Bottom, bottomView }, {Directions.Left, leftView } };
        if (_spriteByDirecitons != null)
        {
            _spriteRenderer.sprite = updatedDictionary.Values.ElementAt(_spriteByDirecitons.Keys.ToList().IndexOf(_currentDirection));
        }
        _spriteByDirecitons = updatedDictionary;
    }

    public void SetCurrentSprite(Directions direction)
    {
        if (direction == _currentDirection) return;
        switch (direction)
        {
            case Directions.Top:
                {
                    _spriteRenderer.sprite = _spriteByDirecitons.Where(key => key.Key == Directions.Top).Select(sprite => sprite.Value).FirstOrDefault();
                    break;
                }
            case Directions.Right:
                {
                    _spriteRenderer.sprite = _spriteByDirecitons.Where(key => key.Key == Directions.Right).Select(sprite => sprite.Value).FirstOrDefault();
                    break;
                }
            case Directions.Bottom:
                {
                    _spriteRenderer.sprite = _spriteByDirecitons.Where(key => key.Key == Directions.Bottom).Select(sprite => sprite.Value).FirstOrDefault();
                    break;
                }
            case Directions.Left:
                {
                    _spriteRenderer.sprite = _spriteByDirecitons.Where(key => key.Key == Directions.Left).Select(sprite => sprite.Value).FirstOrDefault();
                    break;

                }
            default:
                {
                    _spriteRenderer.sprite = _spriteByDirecitons.Where(key => key.Key == Directions.Top).Select(sprite => sprite.Value).FirstOrDefault();
                    break;
                }
        }
        _currentDirection = direction;
    }
}