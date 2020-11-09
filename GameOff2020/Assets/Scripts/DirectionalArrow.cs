using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class DirectionalArrow : MonoBehaviour
{
    public GameObject player;
    private Vector3 target;
    private Vector3 arrowOffset;
    // Start is called before the first frame update
    private void Start()
    {
        target = player.GetComponent<Rigidbody>().position;
        arrowOffset = player.transform.position - transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        //position of arrow
        transform.position = player.transform.position - (new Vector3(arrowOffset.x, arrowOffset.y, arrowOffset.z));

        ////rotation of arrow
        Vector3 newTarget = target;
        newTarget.y = transform.position.y;
        transform.LookAt(newTarget);
    }
}
