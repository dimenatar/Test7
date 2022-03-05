using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerSprites : MonoBehaviour
{
    [SerializeField] private Sprite _normalTopView;
    [SerializeField] private Sprite _normalRightView;
    [SerializeField] private Sprite _normalBottomView;
    [SerializeField] private Sprite _normalLeftView;

    [SerializeField] private Sprite _angrylTopView;
    [SerializeField] private Sprite _angryRightView;
    [SerializeField] private Sprite _angryBottomView;
    [SerializeField] private Sprite _angryLeftView;

    public Sprite NormalTopView => _normalTopView;
    public Sprite NormalRightView => _normalRightView;
    public Sprite NormalBottomView => _normalBottomView;
    public Sprite NormalLeftView => _normalLeftView;

    public Sprite AngryTopView => _angrylTopView;
    public Sprite AngryRightView => _angryRightView;
    public Sprite AngryBottomView => _angryBottomView;
    public Sprite AngryLeftView => _angryLeftView;
}
