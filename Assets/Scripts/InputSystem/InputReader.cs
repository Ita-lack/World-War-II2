using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
public class InputReader : ScriptableObject, PlayerInputMap.IPlayerActions
{
    private PlayerInputMap _playersInput;
    public event UnityAction ActionEvent = delegate { };
    public event UnityAction<Vector3> MoveEvent = delegate { };
    public event UnityAction<Vector2> MoveMouseEvent = delegate { };

    private void OnEnable(){
        if (_playersInput == null){
            _playersInput = new PlayerInputMap();
            _playersInput.Player.SetCallbacks(this);
        }
        EnableAllInput();}
    private void OnDisable(){
        DisableAllInput();
    }
    public void EnableAllInput()
    {
        _playersInput.Player.Enable();
    }
    public void DisableAllInput(){
        _playersInput.Player.Disable();
    }
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        MoveEvent.Invoke(context.ReadValue<Vector3>());
    }
    public void OnActionInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed) ActionEvent.Invoke();
    }
    public void OnMoveMouse(InputAction.CallbackContext context)
    {
        MoveMouseEvent.Invoke(context.ReadValue<Vector2>());
    }
}
