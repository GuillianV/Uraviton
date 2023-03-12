using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject objectToFollow;
    [Header("Properties")]
    public float translationSpeed = 5;
    public float widthDistanceFromObject = 3f;
    public float heightDistanceFromObject = 3f;
    
    private Camera _camera;
    private Transform _cameraTransform;

    private Transform _objectToFollowTransform;
    
    private Vector2 _absoluteDeltaMovement = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        if (!_camera)
        {
            Debug.LogError("No camera with tag MainCamera found");
        }
        
        _cameraTransform = _camera.transform;
        
        if (!objectToFollow)
        {
            Debug.LogError("No object to follow");
        }

        _objectToFollowTransform = objectToFollow.transform;
        
        Vector3 cameraPosition = _cameraTransform.position;
        _cameraTransform.position = new Vector3(cameraPosition.x, objectToFollow.transform.position.y + heightDistanceFromObject, cameraPosition.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        //Position de l'objet a suivre
        Vector3 objectPos = _objectToFollowTransform.position;
        //Position de la main camera
        Vector3 cameraPosition = _cameraTransform.position;
        
        //Création de la force en fonction de la direction dans lequel l'objet va.
        float angle = Mathf.Atan2(_absoluteDeltaMovement.y, _absoluteDeltaMovement.x);
        //Puissance de la force + - élevé en fonction de la propriété widthDistanceFromObject
        float x = Mathf.Cos(angle) * widthDistanceFromObject;
        float z = Mathf.Sin(angle) * widthDistanceFromObject;
        Vector3 force = new Vector3(x, 0, z);
        
        //Application de la force à la position de l'objet suivi
        Vector3 cameraDestination = new Vector3(objectPos.x - force.x,cameraPosition.y, objectPos.z - force.z);

        //Movement délicat en direction de cet objet avec la propriété translationSpeed.
        _cameraTransform.position = Vector3.Lerp(cameraPosition,cameraDestination,translationSpeed*Time.deltaTime) ;
        
        transform.LookAt(_objectToFollowTransform.position);
        
    }
    
        
  
    
    public void DeltaMovementHandler(Vector2 absoluteDeltaMovement)
    {
        this._absoluteDeltaMovement = absoluteDeltaMovement;
    }
    
  
}
