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
    public UnityEvent<Vector2> relativeDeltaMovementEvent;

    
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


    private Vector2 RotateVectorRelativeToCamera(Vector2 initialInput)
    {
        
        float theta = _camera.transform.eulerAngles.y;
        Vector2 initialVector = initialInput; 

        float thetaRad = Mathf.Deg2Rad * ( - theta );
        float cosTheta = Mathf.Cos(thetaRad);
        float sinTheta = Mathf.Sin(thetaRad);

        float xPrime = initialVector.x * cosTheta - initialVector.y * sinTheta;
        float yPrime = initialVector.x * sinTheta + initialVector.y * cosTheta;


        return new Vector2(xPrime,yPrime );
        
    }

    public void OnMove(InputValue input)
    {
        Vector2 absoluteDeltaMovement = input.Get<Vector2>();
      
        
        #if UNITY_EDITOR
        if (_isLeftClicked)
        {
         
            relativeDeltaMovementEvent.Invoke(RotateVectorRelativeToCamera(absoluteDeltaMovement));
            absoluteDeltaMovementEvent.Invoke(absoluteDeltaMovement);
        }
        #else
        relativeDeltaMovementEvent.Invoke(RotateVectorRelativeToCamera(absoluteDeltaMovement));
        absoluteDeltaMovementEvent.Invoke(absoluteDeltaMovement);
        #endif
        
    }

    #if UNITY_EDITOR
    public void OnTriggerMove(InputValue input)
    {
        _isLeftClicked = input.isPressed;
    }
    #endif
}