using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CameraRotation : MonoBehaviour
{
	float speed = 5f;//var speed = float = 5.0;
	[SerializeField] Transform target;   //var target = Transform;
	Camera camera;

	void Start()
	{
		camera = Camera.main;
	}

	void Update ()
	{
		if (Input.GetMouseButton (1)) {
			print ("Pressing right button");
			transform.LookAt (target);
			transform.RotateAround (target.position, Vector3.up, Input.GetAxis ("Mouse X") * speed);
		}
		float scroll = Input.GetAxis ("Mouse ScrollWheel");
		if (scroll > 0) 
		{
			print ("Scroll up");
			camera.fieldOfView--;
		} 
		else if (scroll < 0)
		{
			print ("Scroll down");
			// scroll down
			camera.fieldOfView++;
		}
	}
}
