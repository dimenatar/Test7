using UnityEngine;


public class TouchController : MonoBehaviour 
{
	public delegate void TouchDelegate(Vector2 value);
	public event TouchDelegate TouchEvent;

	public delegate void TouchStateDelegate(bool touchPresent);
	public event TouchStateDelegate TouchStateEvent;

	[SerializeField] private RectTransform _joystickArea;
	[SerializeField] private PlayerMovement _playerMovement;

	private bool _touchPresent = false;
	private Vector2 _movementVector;

    private void Start()
    {
		TouchEvent += _playerMovement.MovePlayer;
    }

    public Vector2 GetTouchPosition
	{
		get { return _movementVector;}
	}

	public void SetPlayerMovement(PlayerMovement playerMovement)
    {
		TouchEvent -= _playerMovement.MovePlayer;
		_playerMovement = playerMovement;
		TouchEvent += _playerMovement.MovePlayer;
	}

	public void BeginDrag()
	{
		_touchPresent = true;
		TouchStateEvent?.Invoke(_touchPresent);
	}

	public void EndDrag()
	{
		_touchPresent = false;
		_movementVector = _joystickArea.anchoredPosition = Vector2.zero;
		TouchStateEvent?.Invoke(_touchPresent);
		TouchEvent?.Invoke(Vector2.zero);
	}

	public void OnValueChanged(Vector2 value)
	{
		if(_touchPresent)
		{
			// convert the value between 1 0 to -1 +1
			_movementVector.x = ((1 - value.x) - 0.5f) * 2f;
			_movementVector.y = ((1 - value.y) - 0.5f) * 2f;

			if(TouchEvent != null)
			{
				TouchEvent(_movementVector);
			}
		}
	}
}
