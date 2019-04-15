using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //configs
    [Header ("Player configs")]
    [SerializeField] float speed = 4f;
    [SerializeField] float runAceleration = 5f, groundDeceleration = 5f, airAceleration = 4f;
    [SerializeField] float heightJump = 10f;
    [Header ("Camera prefab")]
    [SerializeField] new Transform camera;

    //cached gameobjects
    Rigidbody rgbd;

    //Auxiliar variables
    Vector3 velocity;
    float moveXInput, moveZInput;
    bool IsJumping = false;

    void Start(){
        rgbd = GetComponent<Rigidbody>();
    }

    void Update(){
        MoveCamera();
        //MovePlayer();
        Jump();
    }

    private void MovePlayer(){
        float aceleration = (IsJumping? airAceleration : runAceleration);
        float deceleration = (IsJumping? 0 : groundDeceleration);
        moveXInput = Input.GetAxis("Horizontal");
        moveZInput = Input.GetAxis("Vertical");
        if(Mathf.Abs(moveXInput) > Mathf.Epsilon)
            velocity.x = Mathf.MoveTowards(velocity.x, speed*moveXInput, aceleration*Time.deltaTime);
        else
            velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration*Time.deltaTime);
        if(Mathf.Abs(moveZInput) > Mathf.Epsilon)
            velocity.z = Mathf.MoveTowards(velocity.z, speed*moveZInput, aceleration*Time.deltaTime);
        else
            velocity.z = Mathf.MoveTowards(velocity.z, 0, deceleration*Time.deltaTime);
        transform.Translate(velocity*Time.deltaTime); 
    }
    
    private void Jump(){
        if(IsJumping)
            return;
        if(Input.GetButtonDown("Jump")){
            float jumpVelocity = Mathf.Sqrt(2*heightJump*Mathf.Abs(Physics.gravity.y));
            rgbd.velocity = new Vector3(rgbd.velocity.x, jumpVelocity, rgbd.velocity.z);
        }
    }

    private void OnCollisionEnter(Collision otherObject){
        IsJumping = false;
    }

    private void OnCollisionExit(Collision collision){
        IsJumping = true;
    }

    private void MoveCamera(){
        Vector3 cameraToward = transform.position - camera.position;
        cameraToward.y = transform.position.y;
        moveXInput = Input.GetAxisRaw("Horizontal");
        moveZInput  = Input.GetAxisRaw("Vertical");
        if(Mathf.Abs(moveZInput) > Mathf.Epsilon){
            cameraToward.y = 0f;
            transform.position += Mathf.Sign(moveZInput) * cameraToward.normalized * Time.deltaTime * speed;
        }
        if(Mathf.Abs(moveXInput) > Mathf.Epsilon){
            Vector3 horizontalMove = (Mathf.Sign(moveXInput) == -1)? 
                new Vector3(-cameraToward.z, transform.position.y, cameraToward.x) : 
                new Vector3(cameraToward.z, transform.position.y, -cameraToward.x);
            horizontalMove.y = 0f;
            transform.position += horizontalMove.normalized * speed * Time.deltaTime; 
        }
    }
}
