using UnityEngine;

public class PigFarmerConnector : MonoBehaviour
{
    [SerializeField] private PigController _pigController;
    [SerializeField] private Farmer _farmer;

    private void Awake()
    {
        _pigController.Pigs.ForEach(pig => pig.GetComponent<Pig>().OnDied += CheckPlayer);
        _pigController.Pigs.ForEach(pig => pig.GetComponent<Pig>().OnDied += _pigController.ReducePigCount);
    }

    private void CheckPlayer(GameObject gameObject)
    {
        if (gameObject.GetComponent<PlayerMovement>())
        {
            _farmer.ChangeTarget(_pigController.SetNewPlayer());
        }
        else
        {
            _farmer.ChangeTarget(_pigController.GetPlayer());
        }
        Destroy(gameObject.GetComponent<ICharacter>() as Component);
        gameObject.SetActive(false);
    }
}
