using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class KillingTrigger : MonoBehaviour
{
    [SerializeField] private Farmer _farmer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ICharacter>() != null)
        {
            if (other.GetComponent<PlayerMovement>() != null)
            {
                _farmer.ChangeTarget();
            }
            Destroy(other.gameObject);
        }
    }
}
