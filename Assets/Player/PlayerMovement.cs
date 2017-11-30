using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
	[SerializeField] float stopRadius = 0.1f;

	ThirdPersonCharacter character;   			// A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentClickTarget;
	private bool isInDirectMode= false;
	private Vector3 movement;						 // vertical and horizontal movement relative to camera 
	private Transform mainCamera;                  	// A reference to the main camera in the scenes transform    
	private Vector3 mainCameraForward;              // The current forward direction of the camera
    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        character = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
		mainCamera = Camera.main.transform;
    }
	// TODO Fix issue with wasd controls conflicting with point and click movement
    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
       
		if (CrossPlatformInputManager.GetButtonDown("Fire3"))
			{
			isInDirectMode = ! isInDirectMode;
			currentClickTarget = transform.position;// RESET CLICK TARGET
			}
		if (isInDirectMode) 
		{
			ProcessDirectMovement ();
		
		} else 
		{
			
			ProcessMouseMovement ();
		}
			
    }

	void ProcessDirectMovement()
	{
		float h = CrossPlatformInputManager.GetAxis("Horizontal");
		float v = CrossPlatformInputManager.GetAxis("Vertical");
		if (mainCamera != null)
		{
			// calculate camera relative direction to move:
			mainCameraForward = Vector3.Scale(mainCamera.forward, new Vector3(1, 0, 1)).normalized;
			movement = v*mainCameraForward + h*mainCamera.right;
		}
		else
		{
			// we use world-relative directions in the case of no main camera
			movement = v*Vector3.forward + h*Vector3.right;
		}

		character.Move (movement,false,false);
	}


	void ProcessMouseMovement ()
	{
		if (Input.GetMouseButton (0)) {
			print ("Cursor raycast hit" + cameraRaycaster.hit.collider.gameObject.name.ToString ());
			switch (cameraRaycaster.layerHit) {
			case Layer.Walkable:
				currentClickTarget = cameraRaycaster.hit.point;
				//where we click     
				break;
			case Layer.Enemy:
				Debug.Log ("Not moving there");
				break;
			default:
				Debug.LogWarning ("Should not be here");
				return;
			}
		}
		var playerToClickPoint = currentClickTarget - transform.position;
		if (playerToClickPoint.magnitude >= stopRadius) {
			character.Move (playerToClickPoint, false, false);
		}
		else {
			character.Move (Vector3.zero, false, false);
		}
	}
}

