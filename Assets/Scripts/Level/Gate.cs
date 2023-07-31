using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private Transform _leftDoor;
    [SerializeField] private Transform _rightDoor;
    [SerializeField] private Transform _leftDoorOpenPosition;
    [SerializeField] private Transform _rightDoorOpenPosition;
    private Vector3 _leftDoorClosePosition;
    private Vector3 _rightDoorClosePosition;
    private float _doorSpeed = 1;
    private bool _isGateOpen = false;
    private bool _gateOpening;
    private bool _gateClosing;
    private void Start()
    {
        _leftDoorClosePosition = _leftDoor.localPosition;
        _rightDoorClosePosition = _rightDoor.localPosition;
    }
    private void Update()
    {
        if(_gateOpening)
        {
            _leftDoor.localPosition = Vector3.MoveTowards(_leftDoor.localPosition, _leftDoorOpenPosition.localPosition, _doorSpeed * Time.deltaTime);
            _rightDoor.localPosition = Vector3.MoveTowards(_rightDoor.localPosition, _rightDoorOpenPosition.localPosition, _doorSpeed * Time.deltaTime);
            if(_leftDoor.localPosition == _leftDoorOpenPosition.localPosition && _rightDoor.localPosition == _rightDoorOpenPosition.localPosition)
            {
                _gateOpening = false;
                _isGateOpen = true;
            }
        }
        if(_gateClosing)
        {
            _leftDoor.localPosition = Vector3.MoveTowards(_leftDoor.localPosition, _leftDoorClosePosition, _doorSpeed * Time.deltaTime);
            _rightDoor.localPosition = Vector3.MoveTowards(_rightDoor.localPosition, _rightDoorClosePosition, _doorSpeed * Time.deltaTime);
            if(_leftDoor.localPosition == _leftDoorClosePosition && _rightDoor.localPosition == _rightDoorClosePosition)
            {
                _gateClosing = false;
                _isGateOpen = false;
            }
        }
    }
    public void OpenGate()
    {
        if (_isGateOpen) return;
        _gateOpening = true;
        _gateClosing = false;
    }
    public void CloseGate()
    {
        if (!_isGateOpen) return;
        _gateClosing = true;
        _gateOpening = false;
    }
}
