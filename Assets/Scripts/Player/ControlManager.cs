using UnityEngine;
using UnityEngine.InputSystem;
using InputManagement;
using State;

public class ControlManager : MonoBehaviour
{
TouchControls touchControls;
[SerializeField]Player player;
StateController stateController;
[SerializeField]float touchDeadZone = 300f;
Vector2 touchStart;
private void Awake(){
    touchControls = new TouchControls();
}
private void OnEnable(){
    touchControls.Enable();
}
public void OnDisable(){
    touchControls.Disable();
}
void Start(){
    GetReferences();
    touchControls.Touch.TouchPress.started += ctx => StartTouch(ctx);
    touchControls.Touch.TouchPress.canceled += ctx => EndTouch(ctx);
}
void GetReferences(){
    DependencyManager dependencyManager = FindObjectOfType<DependencyManager>();
    stateController = dependencyManager.GetManagersRepo().GetStateController();
}
void StartTouch(InputAction.CallbackContext context){
    InputData input = new InputData();
    if(!stateController.GetState(StateType.canMove)){
        return;
    }
    Vector2 touchLocation = Camera.main.ScreenToWorldPoint(touchControls.Touch.TouchPosition.ReadValue<Vector2>());
    touchStart = touchControls.Touch.TouchPosition.ReadValue<Vector2>();
    if(touchLocation.x > 0){
        input.action = ActionType.Boost;
    }
    if(touchLocation.x < 0){
        input.action = ActionType.Jump;
    }
    player.ManageInput(input);
}
void EndTouch(InputAction.CallbackContext context){
    Vector2 touchLocation = Camera.main.ScreenToWorldPoint(touchControls.Touch.TouchPosition.ReadValue<Vector2>());
    if(touchControls.Touch.TouchPosition.ReadValue<Vector2>().y < touchStart.y - touchDeadZone && touchLocation.x < 0){
        InputData input = new InputData();
        input.action = ActionType.Fall;
        player.ManageInput(input);
    }
    touchStart = new Vector2(0f,0f);
}
}
