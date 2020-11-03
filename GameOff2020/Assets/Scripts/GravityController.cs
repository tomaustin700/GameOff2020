using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
	
	public float gravityX;
	public float gravityY;

	// Use this for initialization
	void Start()
	{
		Physics2D.gravity = new Vector2(gravityX, gravityY);
	}
}
