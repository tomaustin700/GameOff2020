﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed;
    public float JumpHeight = 3f;
    public PlayerAnimationState PlayerAnimationState;
    public LayerMask groundLayers;
    public LayerMask CameraRotateLayerMask;
    private float horizontal;
    private float vertical;
    private Rigidbody rigidBody;
    [SerializeField]
    private bool queueJump = false;
    [SerializeField]
    private bool isJumping = false;
    private Animator animator;
    [SerializeField]
    private bool isRunning = false;
    [SerializeField]
    private bool isFloating = false;
    [SerializeField]
    private bool onGround = false;
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rigidBody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        animator.SetInteger(nameof(PlayerAnimationState), (int)PlayerAnimationState);
        onGround = Physics.CheckSphere(transform.position, 0.2f, groundLayers);
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        var yVelocity = Mathf.Abs(GetComponent<Rigidbody>().velocity.y);
        if (yVelocity >= 0.5f)
        {
            isFloating = true;
            PlayerAnimationState = PlayerAnimationState.Floating;
        }
        else
        {
            isFloating = false;
        }

        if(onGround && !isJumping && !isFloating && horizontal == 0 && vertical == 0)
        {
            PlayerAnimationState = PlayerAnimationState.Idle;
        }
        else
        {
            if ((horizontal != 0 || vertical != 0) && onGround && !isJumping && !isFloating)
            {
                if (!isRunning)
                {
                    PlayerAnimationState = PlayerAnimationState.Walking;
                }
                else if (isRunning)
                {
                    PlayerAnimationState = PlayerAnimationState.Running;
                }

            }
        }
        if (Input.GetKey(KeyCode.Space) && !isJumping && onGround && !isFloating)
        {
            queueJump = true;
        }
    }

    private void FixedUpdate()
    {
        Vector3 movementVector = (transform.forward * vertical) + (transform.right * horizontal);
        movementVector.y = 0;
        if(Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            rigidBody.MovePosition(transform.position + movementVector * Time.deltaTime * (Speed * 1.5f));
        }
        else
        {
            isRunning = false;
            rigidBody.MovePosition(transform.position + movementVector * Time.deltaTime * Speed);
        }
      

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit))
        {
            var newPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(newPosition);
        }

        if(queueJump && !isJumping)
        {
            queueJump = false;
            StartCoroutine(StartJump());
        }
    }
    IEnumerator StartJump()
    {
        isJumping = true;
        PlayerAnimationState = PlayerAnimationState.Jumping;
        yield return new WaitForSeconds(0.5f);
        if (rigidBody.velocity.y <= 0 && Physics.CheckSphere(transform.position, 0.5f, groundLayers))
        {
            rigidBody.AddForce((Vector3.up * JumpHeight) + rigidBody.velocity, ForceMode.Impulse);
        }
        isJumping = false;
      



    }
}
