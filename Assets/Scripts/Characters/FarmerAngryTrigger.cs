using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class FarmerAngryTrigger : MonoBehaviour
{
    [SerializeField] private Farmer _farmer;

    public List<ICharacter> _pigList = new List<ICharacter>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ICharacter>() != null)
        {
            _farmer.ChangeTarget(other.gameObject);
            _farmer.FoundPig(other.GetComponent<ICharacter>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ICharacter>() != null)
        {
            _farmer.RemovePig(other.GetComponent<ICharacter>());
        }
    }
}
