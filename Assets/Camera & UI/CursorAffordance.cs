using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAffordance : MonoBehaviour
{
	[SerializeField] Texture2D walkingCursor= null;
	[SerializeField] Texture2D enemyCursor= null;
	[SerializeField] Texture2D unknownCursor= null;
	[SerializeField] Vector2 clickSpot = new Vector2 (0, 0);

    CameraRaycaster camCaster;


	// Use this for initialization
	void Start () {
        camCaster = GetComponent<CameraRaycaster>();
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		switch (camCaster.layerHit)
		{
		case Layer.Walkable:
			Cursor.SetCursor (walkingCursor, clickSpot, CursorMode.Auto);
				break;
		case Layer.Enemy:
			Cursor.SetCursor (enemyCursor, clickSpot, CursorMode.Auto);
			break;
		case Layer.RaycastEndStop:
			Cursor.SetCursor (unknownCursor, clickSpot, CursorMode.Auto);
			break;
		default:
			break;
		}
		
	}
}
