using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject objectToFollow;
    public float speed = 5;
    private Camera camera;
    
    private Vector3 offset = Vector3.zero;
    private Vector2 deltaMovement = Vector2.zero;

    private Quaternion initialRotation = Quaternion.identity;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        if (!objectToFollow)
        {
            Debug.LogError("No object to follow");
        }

        initialRotation = camera.transform.rotation;
        offset = camera.transform.position - objectToFollow.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      //  LerpCameraTo(objectToFollow.transform.position, speed, camera);
        LerpCameraRotationTo(objectToFollow.transform, speed, camera);
    }
    
        
    public void LerpCameraTo(Vector3 _objectToFollowPos, float _speed, Camera camera)
    {
        camera.transform.position = Vector3.Lerp(camera.transform.position,new Vector3(_objectToFollowPos.x + offset.x,camera.transform.position.y,_objectToFollowPos.z + offset.z),_speed*Time.deltaTime) ;
    }
    
    public void DeltaMovementHandler(Vector2 _deltaMovement)
    {
        deltaMovement = _deltaMovement;
    }
    
    public float distance = 10.0f; // La distance entre votre objet et votre caméra
    public float height = 5.0f; // La hauteur à laquelle votre caméra doit se trouver au-dessus de votre objet
    public float smoothSpeed = 0.125f; // La vitesse de déplacement de votre caméra
    public float lookAhead = 5.0f; // La distance devant votre objet à laquelle votre caméra doit regarder

    private Vector3 smoothVelocity = Vector3.zero;
    
    public void LerpCameraRotationTo(Transform _objectToFollowPos, float _speed, Camera camera)
    {
        // Calcule le vecteur direction entre notre objet et le point cible
       // Vector3 direction = new Vector3(+deltaMovement.x,camera.transform.rotation.y,deltaMovement.y);
     //   Quaternion lookRotation = Quaternion.LookRotation(direction);
   
        // Effectue une lerp sur l'axe Y uniquement
       // Quaternion newRotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.eulerAngles.x, lookRotation.eulerAngles.y, transform.rotation.eulerAngles.z), speed * Time.deltaTime);

        // Applique la nouvelle rotation à la caméra
        //transform.rotation = newRotation;

        // Déplace la caméra vers le joueur
        //transform.position = target.position - transform.forward * 10f;
        
        // Calcule la position cible pour votre caméra
        Vector3 targetPosition = _objectToFollowPos.position - _objectToFollowPos.forward * distance + Vector3.up * height;

        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref smoothVelocity, smoothSpeed);

        // Applique la nouvelle position à votre caméra
        transform.position = smoothedPosition;

        // Calcule la direction dans laquelle se déplace votre objet
        Vector3 direction = _objectToFollowPos.position + _objectToFollowPos.forward * lookAhead - transform.position;

        // Calcule la nouvelle rotation pour votre caméra
        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, currentRotation.y, direction.z));
        Quaternion smoothedRotation = Quaternion.Lerp(currentRotation, targetRotation, smoothSpeed);

        // Applique la nouvelle rotation à votre caméra
        transform.rotation = smoothedRotation;
    }
}
