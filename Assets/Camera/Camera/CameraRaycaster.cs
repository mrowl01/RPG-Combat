using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.Analytics;
using RPG.Characters; // To detect by type 

namespace RPG.CameraUI 
{
public class CameraRaycaster : MonoBehaviour 
{
	[SerializeField] Texture2D walkingCursor= null;
	[SerializeField] Texture2D enemyCursor= null;
	[SerializeField] Vector2 clickSpot = new Vector2 (0, 0);

	const int POTENTIALLY_WALKABLE_LAYER = 8; //
	float maxRaycastDepth = 100f;// Hard coded value
	
	public delegate void OnMouseOverTerrain (Vector3 destination );
	public event OnMouseOverTerrain onMouseOverPotentiallyWalkable; 

	public delegate void OnMouseOverEnemy (Enemy enemy );
	public event OnMouseOverEnemy onMouseOverEnemy; 

		void Update()
		{

		//CHeck if pointer is over an interactable UI element
				if (EventSystem.current.IsPointerOverGameObject ()) 
			{
				// TODO implement ui interaction
			}
				else 
			{
				performRaycast ();
			}
		}

		void performRaycast ()
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (RaycastForEnemy (ray)) {return;}
			if (RaycastForPotentiallyWalkable (ray)) {return;}
		}

		bool RaycastForEnemy (Ray ray )
		{
			RaycastHit hitInfo; 
			Physics.Raycast (ray, out hitInfo, maxRaycastDepth);
			if (hitInfo.transform != null) 
			{
				var gameObjectHit = hitInfo.collider.gameObject;
				var enemyHit = gameObjectHit.GetComponent<Enemy> ();
				if (enemyHit) 
				{
					Cursor.SetCursor (enemyCursor, clickSpot, CursorMode.Auto); 
					onMouseOverEnemy (enemyHit); //delegate
					return true; 
				}
			}
			return false;
		}
		bool RaycastForPotentiallyWalkable (Ray ray)
		{
			RaycastHit hitInfo;
			LayerMask potentiallyWalkableLayer = 1 << POTENTIALLY_WALKABLE_LAYER; 
			bool potentiallyWalkableHit = Physics.Raycast (ray, out hitInfo, maxRaycastDepth, potentiallyWalkableLayer);
			if (potentiallyWalkableHit)
			{
				Cursor.SetCursor (walkingCursor, clickSpot, CursorMode.Auto);
				onMouseOverPotentiallyWalkable (hitInfo.point); // delegate
				return true;
			}
			return false; 
			}
	}
}
