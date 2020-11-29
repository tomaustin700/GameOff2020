using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed;
    public float JumpHeight = 3f;
    public PlayerAnimationState PlayerAnimationState;
    public LayerMask groundLayers;
    public LayerMask CameraRotateLayerMask;
    public bool AllowPlayerMovementInAir = false;
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
    public bool onGround = false;
    private Vector3 currentVelocity;
    private GameObject model;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        model = transform.GetComponentInChildren<Animator>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if ((horizontal != 0 || vertical != 0))
        {
            if (vertical > 0)
            {
                NotificationManager.CompleteNotification(EventName.MoveForwards);
            }
            if (vertical < 0)
            {
                NotificationManager.CompleteNotification(EventName.MoveBackwards);
            }
            if (horizontal > 0)
            {
                NotificationManager.CompleteNotification(EventName.TurnRight);
            }
            if (horizontal < 0)
            {
                NotificationManager.CompleteNotification(EventName.TurnLeft);
            }
            if (!AllowPlayerMovementInAir && onGround)
            {
                var transformTarget = Camera.main.GetComponent<CameraFollow>().CameraPoint.gameObject.transform;
                model.transform.rotation = Quaternion.Slerp(model.transform.rotation, Quaternion.Euler(model.transform.rotation.eulerAngles.x, transformTarget.rotation.eulerAngles.y, model.transform.rotation.eulerAngles.z), 0.2f);
            }
            else if (AllowPlayerMovementInAir)
            {
                var transformTarget = Camera.main.GetComponent<CameraFollow>().CameraPoint.gameObject.transform;
                model.transform.rotation = Quaternion.Slerp(model.transform.rotation, Quaternion.Euler(model.transform.rotation.eulerAngles.x, transformTarget.rotation.eulerAngles.y, model.transform.rotation.eulerAngles.z), 0.2f);
            }

        }

        animator.SetInteger(nameof(PlayerAnimationState), (int)PlayerAnimationState);
        onGround = Physics.CheckSphere(transform.position, 0.4f, groundLayers);
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        var yVelocity = Mathf.Abs(GetComponent<Rigidbody>().velocity.y);
        if (yVelocity >= 0.5f && !onGround)
        {
            isFloating = true;
            PlayerAnimationState = PlayerAnimationState.Floating;
        }
        else
        {
            isFloating = false;
        }

        if (onGround && !isJumping && !isFloating && vertical == 0)
        {
            PlayerAnimationState = PlayerAnimationState.Idle;
        }
        else
        {
            if ((horizontal != 0 || vertical != 0))
            {
                if (!isRunning && onGround && !isJumping && !isFloating)
                {
                    PlayerAnimationState = PlayerAnimationState.Walking;
                }
                else if (isRunning && onGround && !isJumping && !isFloating)
                {
                    PlayerAnimationState = PlayerAnimationState.Running;
                }

            }
        }
        if (Input.GetKey(KeyCode.Space) && !isJumping && onGround && !isFloating)
        {
            queueJump = true;
            NotificationManager.CompleteNotification(EventName.Jump);

        }
    }

    private void FixedUpdate()
    {
        Vector3 movementVector = model.transform.forward * vertical + model.transform.right * 0;
        movementVector.y = 0;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            rigidBody.MovePosition(transform.position + movementVector * Time.deltaTime * (Speed * 1.5f));
        }
        else
        {
            isRunning = false;
            rigidBody.MovePosition(transform.position + movementVector * Time.deltaTime * Speed);
        }


        if (queueJump && !isJumping)
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
