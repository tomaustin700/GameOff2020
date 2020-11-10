using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class DirectionalArrow : MonoBehaviour
{
    public GameObject player;
    private Vector3 target;

    void LateUpdate()
    {
        Vector3 newPosition = player.transform.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        //set target
        target = new Vector3(0, 0);
        //rotation
        transform.LookAt(new Vector3(target.x, transform.position.y));
    }
}
