using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed;
    public float JumpHeight = 10f;
    public LayerMask groundLayers;
    public LayerMask CameraRotateLayerMask;
    private float horizontal;
    private float vertical;
    private Rigidbody rigidBody;
    private bool queueJump = false;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        if(Input.GetKey(KeyCode.Space))
        {
            queueJump = true;
        }
    }

    private void FixedUpdate()
    {
        Vector3 movementVector = (transform.forward * vertical) + (transform.right * horizontal);
        rigidBody.MovePosition(transform.position + movementVector * Time.deltaTime * Speed);

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit))
        {
            var newPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(newPosition);
        }

        if(queueJump)
        {
            queueJump = false;
            if (rigidBody.velocity.y <= 0 && Physics.CheckSphere(transform.position, 0.5f, groundLayers))
            {
                rigidBody.AddForce((Vector3.up * JumpHeight) + rigidBody.velocity, ForceMode.Impulse);
            }
        }
    }
}
