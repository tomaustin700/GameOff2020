using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera MainCam;
    private Camera ZoomCam;
    [SerializeField] private PlayerManager player;
    void Start()
    {
        MainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        ZoomCam = GameObject.Find("ZoomCam").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player_WithCamera").GetComponent<PlayerManager>();
        if (player.inHab)
        {
            MainCam.enabled = false;
            ZoomCam.enabled = true;
        }
        else
        {
            MainCam.enabled = true;
            ZoomCam.enabled = false;
        }
    }
}
