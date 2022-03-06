using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PigController : MonoBehaviour
{
    public event Action OnPigsEmpty;

    [SerializeField] private List<GameObject> _pigs;
    [SerializeField] private TouchController _touchController;
    [SerializeField] private GameObject _markerPrefab;

    private GameObject _player;

    public List<GameObject> Pigs => _pigs;

    private void Start()
    {
        _player = _pigs.Where(movement => movement.GetComponent<PlayerMovement>() != null).FirstOrDefault();
    }

    public GameObject GetPlayer()
    {
        return _player;
    }

    public void ReducePigCount(GameObject pig)
    {
        _pigs.Remove(pig);
    }

    public GameObject SetNewPlayer()
    {
        _pigs.Remove(_player);
        if (_pigs.Count > 0)
        {
            GameObject newPlayer = _pigs[UnityEngine.Random.Range(0, _pigs.Count)];
            Destroy(newPlayer.GetComponent<MovementByCells>());
            newPlayer.AddComponent<PlayerMovement>();
            newPlayer.GetComponent<PlayerMovement>().Speed = _player.GetComponent<PlayerMovement>().Speed;
            newPlayer.GetComponent<PlayerMovement>().OnDirectionChanged += newPlayer.GetComponent<SpriteDirectionController>().SetCurrentSprite;
            GameObject marker = Instantiate(_markerPrefab, newPlayer.transform);
            marker.transform.localPosition = new Vector3(0, 0, 1);
            _touchController.SetPlayerMovement(newPlayer.GetComponent<PlayerMovement>());
            _player = newPlayer;
            return newPlayer;
        }
        else
        {
            OnPigsEmpty?.Invoke();
            return null;
        }
    }

    private Component CopyComponent(Component original, GameObject destination)
    {
        Type type = original.GetType();
        Component copy = destination.AddComponent(type);
        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }
        return copy;
    }
}
