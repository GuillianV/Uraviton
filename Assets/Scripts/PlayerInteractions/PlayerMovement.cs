using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    private PlayerInput _playerInput;
    private bool _isLeftClicked = false;
    public UnityEvent<Vector2> absoluteDeltaMovementEvent;

    private Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
        _camera =Camera.main;
        Application.targetFrameRate = 120;
        _playerInput = GetComponent<PlayerInput>();
        #if UNITY_EDITOR
        _playerInput.currentActionMap = _playerInput.actions.FindActionMap("Ball_PC");
        #else
        _playerInput.currentActionMap = _playerInput.actions.FindActionMap("Ball_Phone");
        #endif

        _playerInput.ActivateInput();
    }



    public void OnMove(InputValue input)
    {
        Vector2 inputVec = input.Get<Vector2>();


        #if UNITY_EDITOR
        if (_isLeftClicked)
        {
            absoluteDeltaMovementEvent.Invoke(inputVec);
        }
        #else
        movementEvent.Invoke(mov);
        #endif
        
    }

    #if UNITY_EDITOR
    public void OnTriggerMove(InputValue input)
    {
        _isLeftClicked = input.isPressed;
    }
    #endif
}