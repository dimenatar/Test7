using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float _timeToDetonate;
    [SerializeField] private float _damage;
    [SerializeField] private float _explosionArea;

    private void Start()
    {
        Invoke(nameof(Detonate), _timeToDetonate);
    }

    private void Detonate()
    {
        MakeDamageInArea(FindCharacters());
        Destroy(gameObject);
    }

    private List<Collider> FindCharacters()
    {
        return Physics.OverlapSphere(transform.position, _explosionArea).ToList().Where(charecter => charecter.GetComponent<ICharacter>() != null).ToList();
    }

    private void MakeDamageInArea(List<Collider> characters)
    {
        Vector3 explosionPosition = transform.position;
        float proximity, effect;
        foreach (Collider enemy in characters)
        {
            proximity = (explosionPosition - enemy.transform.position).magnitude;
            effect = 1 - (proximity / _explosionArea);
            enemy.GetComponent<ICharacter>().TakeDamage(Mathf.RoundToInt(effect * _damage));
        }
    }
}
