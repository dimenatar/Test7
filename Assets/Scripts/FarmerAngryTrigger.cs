using System.Collections;
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
            _farmer.BecomeAngry();
            if (!_pigList.Contains(other.GetComponent<ICharacter>()))
            {
                _pigList.Add(other.GetComponent<ICharacter>());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ICharacter>() != null)
        {
            _pigList.Remove(other.GetComponent<ICharacter>());
            if (_pigList.Count <= 0)
            {
                _farmer.StopBeingAngry();
            }
        }
    }
}
