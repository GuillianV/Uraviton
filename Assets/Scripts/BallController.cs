using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class BallController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 1;

    private Rigidbody _rigidbody;
    private SphereCollider _sphereCollider;
    private PlayerMovement _playerMovement;
    private int frameRate = 120;
    private Vector2 _absoluteDeltaMovement = Vector2.zero;
    private Vector2 _deltaMovement = Vector2.zero;

    private Camera _camera;
    void Start()
    {
        
        _rigidbody = GetComponent<Rigidbody>();
        _sphereCollider = GetComponent<SphereCollider>();
        _camera = Camera.main;
    }

    public void DeltaMovementHandler(Vector2 absoluteDeltaMovement)
    {
        _absoluteDeltaMovement = absoluteDeltaMovement;
        float theta = _camera.transform.eulerAngles.y;
        Vector2 initialVector = _absoluteDeltaMovement; // vecteur initial à faire pivoter

        float thetaRad = Mathf.Deg2Rad * ( theta + 180); // conversion degrés vers radians
        float cosTheta = Mathf.Cos(thetaRad);
        float sinTheta = Mathf.Sin(thetaRad);

// Application de la formule de rotation
        float xPrime = initialVector.x * cosTheta - initialVector.y * sinTheta;
        float yPrime = initialVector.x * sinTheta + initialVector.y * cosTheta;

     
        _deltaMovement = new Vector2(xPrime,yPrime ).normalized;


    }

    // Update is called once per frame
    void Update()
    {

        float xVel = 0;
        float yVel = 0;

        xVel = _deltaMovement.x * speed * Time.deltaTime * frameRate;
        yVel = _deltaMovement.y * speed * Time.deltaTime * frameRate;

        
        
        _rigidbody.AddForce(new Vector3(xVel, 0,yVel ));
     
    
    }


}
