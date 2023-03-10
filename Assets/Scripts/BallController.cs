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
    private Vector2 deltaMovement = Vector2.zero;
    
    void Start()
    {
        
        _rigidbody = GetComponent<Rigidbody>();
        _sphereCollider = GetComponent<SphereCollider>();
    }

    public void DeltaMovementHandler(Vector2 _deltaMovement)
    {
        deltaMovement = _deltaMovement;
    }

    // Update is called once per frame
    void Update()
    {

        float xVel = 0;
        float yVel = 0;

        xVel = deltaMovement.x * speed * Time.deltaTime * frameRate;
        yVel = deltaMovement.y * speed * Time.deltaTime * frameRate;

        
        _rigidbody.AddForce(new Vector3(xVel, 0,yVel ));
     
    
    }


}
