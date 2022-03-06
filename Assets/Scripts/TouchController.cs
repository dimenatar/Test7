using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class TouchController : MonoBehaviour 
{

	public delegate void TouchDelegate(Vector2 value);
	public event TouchDelegate TouchEvent;

	public delegate void TouchStateDelegate(bool touchPresent);
	public event TouchStateDelegate TouchStateEvent;

	[SerializeField] private RectTransform joystickArea;
	[SerializeField] private PlayerMovement _playerMovement;

	private bool touchPresent = false;
	private Vector2 movementVector;

    private void Start()
    {
		TouchEvent += _playerMovement.MovePlayer;
    }

    public Vector2 GetTouchPosition
	{
		get { return movementVector;}
	}

	public void SetPlayerMovement(PlayerMovement playerMovement)
    {
		TouchEvent -= _playerMovement.MovePlayer;
		_playerMovement = playerMovement;
		TouchEvent += _playerMovement.MovePlayer;
	}

	public void BeginDrag()
	{
		touchPresent = true;
		TouchStateEvent?.Invoke(touchPresent);
	}

	public void EndDrag()
	{
		touchPresent = false;
		movementVector = joystickArea.anchoredPosition = Vector2.zero;
		TouchStateEvent?.Invoke(touchPresent);
		TouchEvent?.Invoke(Vector2.zero);
	}

	public void OnValueChanged(Vector2 value)
	{
		if(touchPresent)
		{
			// convert the value between 1 0 to -1 +1
			movementVector.x = ((1 - value.x) - 0.5f) * 2f;
			movementVector.y = ((1 - value.y) - 0.5f) * 2f;

			if(TouchEvent != null)
			{
				TouchEvent(movementVector);
			}
		}
	}
}
