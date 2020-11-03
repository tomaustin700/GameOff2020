using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        //Earth gravity is -9.5f
        Physics.gravity = new Vector3(0, -3, 0);
    }
}
