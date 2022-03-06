using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class KillingTrigger : MonoBehaviour
{
    [SerializeField] private Farmer _farmer;
    [SerializeField] private PigController _pigController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ICharacter>() != null)
        {
            if (other.GetComponent<PlayerMovement>())
            {
                _farmer.ChangeTarget(_pigController.SetNewPlayer());
            }
            Destroy(other.GetComponent<ICharacter>() as Component);
            other.gameObject.SetActive(false);
        }
    }
}
