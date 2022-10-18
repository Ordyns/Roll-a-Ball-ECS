using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Entities;

[AlwaysSynchronizeSystem]
public partial class PlayerMovementInputSystem : SystemBase
{
    private PlayerInputActions _playerInputActions;

    protected override void OnCreate(){
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();
    }

    protected override void OnUpdate(){
        Vector2 input = _playerInputActions.Player.Move.ReadValue<Vector2>();

        Entities.ForEach((ref PlayerMovementData playerMovementData) => {
            playerMovementData.Direction = input;
        }).Run();   
    }
}
