using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace  RPG.CameraUI {
public class CursorAffordance : MonoBehaviour
{
	[SerializeField] Texture2D walkingCursor= null;
	[SerializeField] Texture2D enemyCursor= null;
	[SerializeField] Texture2D unknownCursor= null;
	[SerializeField] Vector2 clickSpot = new Vector2 (0, 0);
	[SerializeField]  const int walkableLayerNumber = 8;
	[SerializeField]  const int enemyLayerNumber = 9;

    CameraRaycaster camCaster;


	// Use this for initialization
	void Start () {
        camCaster = GetComponent<CameraRaycaster>();
		camCaster.notifyLayerChangeObservers += OnLayerChange;
	}
	
	// Update is called once per frame
	void OnLayerChange (int newLayer) //only called when layer changes
	{
		switch (newLayer)
		{
		case walkableLayerNumber:
			Cursor.SetCursor (walkingCursor, clickSpot, CursorMode.Auto);
				break;
		case enemyLayerNumber:
			Cursor.SetCursor (enemyCursor, clickSpot, CursorMode.Auto);
			break;
		default:
			Cursor.SetCursor (unknownCursor, clickSpot, CursorMode.Auto);
			break;
		}
		
	}

}
// TODO consider de-registering observer when leaving gamescenes
}
