using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject FollowTarget;
    private Vector3 CameraOffset;
    public float MaxZoomInRange = 7.0f;
    public float MaxZoomOutRange = 5f;
    public float ScrollSpeed = 3f;
    [SerializeField]
    private float CameraZoom;
    // Start is called before the first frame update
    void Start()
    {
        CameraOffset = FollowTarget.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CameraZoom = Mathf.Clamp(CameraZoom += Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed, -MaxZoomOutRange, MaxZoomInRange);
        transform.position = FollowTarget.transform.position - (new Vector3(CameraOffset.x, CameraOffset.y + CameraZoom, CameraOffset.z));
        transform.LookAt(FollowTarget.transform);
    }
}
