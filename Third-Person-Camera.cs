using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    //Setup vars
    [SerializeField] float distance = 6f;
    [SerializeField] float rotationSpeed = 3f;
    [SerializeField] Transform player;
    [SerializeField] bool lockCursor = true;
    [SerializeField] float pitchMin = -90, pitchMax = 90;
    [SerializeField] float rotatioSmoothTime = .12f;
    //cached game object
    float pitch, yaw;
    Vector3 currentRotation, rotatioSmoothVelocity;

    private void Start(){
        if(lockCursor){
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    void Update(){
    }

    void LateUpdate(){
        ThirdPersonCamera();
    }

    private void FollowPlayer(){
        Vector3 lookOnPlayer = player.position - transform.position;
        transform.forward = lookOnPlayer.normalized;
        
        Vector3 playerLastPosition = player.position - distance*lookOnPlayer.normalized;
        playerLastPosition.y = player.position.y + distance/4;
        transform.position = playerLastPosition;
    }

    private void RotateWithMouse(){
        pitch += rotationSpeed*Input.GetAxis("Mouse Y");
        yaw += rotationSpeed*Input.GetAxis("Mouse X");

        pitch = Mathf.Clamp(pitch, -90, 90);
        yaw += (yaw < 0f)? 360 : ((yaw > 360)? -360: 0f);
        transform.eulerAngles = new Vector3(-pitch, yaw, 0f);
    }

    private void ThirdPersonCamera(){
        yaw += rotationSpeed*Input.GetAxis("Mouse X");
        pitch -= rotationSpeed*Input.GetAxis("Mouse Y"); 
        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotatioSmoothVelocity, rotatioSmoothTime);
        transform.eulerAngles = currentRotation;

        transform.position = player.position - transform.forward*distance;
    }
}
