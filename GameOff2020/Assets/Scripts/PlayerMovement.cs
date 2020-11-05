using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed;
    public float JumpHeight = 3f;
    public LayerMask groundLayers;
    public LayerMask CameraRotateLayerMask;
    private float horizontal;
    private float vertical;
    private Rigidbody rigidBody;
    private bool queueJump = false;
    private bool isJumping = false;
    private Animator animator;
    private bool isRunning = false;
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        Debug.Log(animator != null);
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        bool onGround = Physics.CheckSphere(transform.position, 0.5f, groundLayers);
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (!onGround)
        {
            animator.SetInteger("PlayerState", 5);

        }
        else if (animator.GetInteger("PlayerState") == 2 && onGround && !isJumping)
        {
            animator.SetInteger("PlayerState", 3);
        }
        else if(horizontal != 0 || vertical != 0 && onGround && !isJumping)
        {
            if(!isRunning)
            {
                animator.SetInteger("PlayerState", 1);
            }
            else
            {
                animator.SetInteger("PlayerState", 4);
            }
            
        }
        else if(!queueJump && onGround && !isJumping)
        {
            animator.SetInteger("PlayerState", 0);
        }
        if (Input.GetKey(KeyCode.Space) && !isJumping && onGround)
        {
            queueJump = true;
        }
    }

    private void FixedUpdate()
    {
        Vector3 movementVector = (transform.forward * vertical) + (transform.right * horizontal);
        if(Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            rigidBody.MovePosition(transform.position + movementVector * Time.deltaTime * (Speed * 2));
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
        animator.SetInteger("PlayerState", 2);
        yield return new WaitForSeconds(0.5f);
        if (rigidBody.velocity.y <= 0 && Physics.CheckSphere(transform.position, 0.5f, groundLayers))
        {
            rigidBody.AddForce((Vector3.up * JumpHeight) + rigidBody.velocity, ForceMode.Impulse);
        }
        isJumping = false;
    }
}
