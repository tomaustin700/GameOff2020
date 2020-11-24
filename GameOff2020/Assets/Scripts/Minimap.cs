using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform player;
    float camHeightDiff;
    // Start is called before the first frame update
    private void Start()
    {
        camHeightDiff = transform.position.y - player.transform.position.y;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y = newPosition.y + camHeightDiff;
        transform.position = newPosition;
    }
}
