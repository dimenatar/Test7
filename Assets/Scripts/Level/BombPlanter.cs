using UnityEngine;

public class BombPlanter : MonoBehaviour
{
    [SerializeField] private PigController _pigController;
    [SerializeField] private GameObject _bombPrefab;

    public void PlantBomb()
    {
        Transform bombPosition = _pigController.GetPlayer().transform;
        GameObject bomb = Instantiate(_bombPrefab, bombPosition.position, _bombPrefab.transform.rotation);
    }

}
