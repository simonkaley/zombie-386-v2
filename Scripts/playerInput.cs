using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Unity has an existing input system and a new one
/// They can not be used simultaneously. To use the new
/// input system, you must open the package manager window
/// then add "Input System" from the Unity Registry
/// </summary>
using UnityEngine.InputSystem;

public class playerInput : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D jumpingBody, movingBody;
    [SerializeField]
    bool useForce = true;
    [SerializeField]
    float jumpPower = 6f, moveSpeed = 2.3f;
    float sprintMultiplier = 1f;
    float physicsModifier = 100f;

    private bool isSprinting;


    Vector2 moveDir = Vector2.zero;
    void Start()
    {

        
    }

    
    
    void FixedUpdate()
    {

        

        if(movingBody) 
        if(useForce) 
            movingBody.AddForce(moveDir*moveSpeed*Time.deltaTime*physicsModifier, ForceMode2D.Force);
        else
            movingBody.MovePosition(movingBody.position+(moveDir*moveSpeed*sprintMultiplier*Time.deltaTime));

    }

    //This function will provide movement using the new input system
    public void OnMove(InputAction.CallbackContext context)
    {
        moveDir = context.ReadValue<Vector2>() * moveSpeed;
        //Debug.Log(moveDir);
    }
    
    public void OnSprint(InputAction.CallbackContext context)
    {
        
        isSprinting = context.ReadValueAsButton();

        if (isSprinting)
        {
            //start sprinting 
            sprintMultiplier = 2f;
        }
        else
        {
            //stop sprinting
            sprintMultiplier = 1f;
        }
    }


   
    void OnJump()
    {
        if(jumpingBody) jumpingBody.AddForce(Vector2.up*jumpPower,ForceMode2D.Impulse);
        //Debug.Log("jump");
    }
}
