using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector2 CameraSensitivity = new Vector2(3f, 3f );
    public float TurnSpeed = 2.0f;
    public GameObject CameraPoint;
    [SerializeField]
    private Vector2 mouseAxis;
    private Vector3 targetRotation;
    private float horizontal;
    private void Awake()
    {
        Cursor.visible = false;
    }
    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        mouseAxis = new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X")).normalized;
        if(Mathf.Abs(mouseAxis.x) <= 0.001f || Mathf.Abs(mouseAxis.y) <= 0.001f)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        Vector2 mouseDir = new Vector2(CameraPoint.transform.rotation.eulerAngles.x + (mouseAxis.x * CameraSensitivity.y), CameraPoint.transform.rotation.eulerAngles.y + (mouseAxis.y * CameraSensitivity.x));

        Vector3 angle = new Vector3(Mathf.Clamp(mouseDir.x,0f,40f), mouseDir.y + (horizontal * TurnSpeed), 0);
        if (Mathf.Abs(mouseAxis.x) > 0.001f || Mathf.Abs(mouseAxis.y) > 0.001f || horizontal != 0)
        {
            targetRotation = angle;
        }
        CameraPoint.transform.rotation = Quaternion.Slerp(CameraPoint.transform.rotation, Quaternion.Euler(targetRotation),0.5f);
    }

}
