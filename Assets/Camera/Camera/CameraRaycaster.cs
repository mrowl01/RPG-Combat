using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using System.Collections.Generic;


namespace RPG.CameraUI {
public class CameraRaycaster : MonoBehaviour 
{
	// INSPECTOR PROPERTIES RENDERED BY CUSTOM EDITOR SCRIPT
	[SerializeField] int [] layerPriorities;

	float maxRaycastDepth = 100f;// Hard coded value
	int topPriorityLayerLastFrame = -1;// So get ? from start with default

	// Setup delegates for broadcasting layer changes to other classes
	public delegate void OnCursorLayerChange (int newLayer); // declate new delegate
	public event OnCursorLayerChange notifyLayerChangeObservers;// instantiate to notify when layer changes ( Dwight)

	public delegate void OnClickPriorityLayer (RaycastHit raycastHit, int layerHit);// declate new delegate type
	public event OnClickPriorityLayer notifyMouseClickObservers;// instantiate to notify what priority layer is

	public delegate void OnRightClick (RaycastHit raycastHit, int layerHit);// declate new delegate type
	public event OnRightClick notifyRightClickObservers;// instantiate to notify what priority layer is

	void Update()
	{

		//CHeck if pointer is over an interactable UI element
		if (EventSystem.current.IsPointerOverGameObject())
		{
			NotifyObserversIfLayerChanged (5);
			return;//Stop looking for other objects
		}

		//Raycast to max depth, every frame as things can move under mouse
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit[] raycastHits = Physics.RaycastAll (ray, maxRaycastDepth);

		RaycastHit? priorityHit = FindTopPriorityHit (raycastHits);
		if (!priorityHit.HasValue)// if not no priority object
		{
			NotifyObserversIfLayerChanged (0);// broadcast default layer
			return;
		}

		// Notify delagtes of layer change 
		var layerHit = priorityHit.Value.collider.gameObject.layer;
		NotifyObserversIfLayerChanged (layerHit);

		// Notify delegates of highest priority game objhect under mouse when clicked 
		if (Input.GetMouseButton (0)) 
		{
			notifyMouseClickObservers (priorityHit.Value, layerHit);
		}
		if (Input.GetMouseButtonDown (1)) 
			{
				notifyRightClickObservers (priorityHit.Value, layerHit);
			}
	}

	void NotifyObserversIfLayerChanged(int newLayer)
	{
		if (newLayer != topPriorityLayerLastFrame) 
		{
			topPriorityLayerLastFrame = newLayer;
			notifyLayerChangeObservers (newLayer);
		}
	}

	RaycastHit? FindTopPriorityHit (RaycastHit[] raycastHits)
	{
		// Form list of layer numbers hit
		List<int> layerOfHitColliders = new List<int>();
		foreach (RaycastHit hit in raycastHits)
		{
			layerOfHitColliders.Add (hit.collider.gameObject.layer);
		}
		// Step through layers in order of priority looking for a gameobject with that layer

		foreach (int layer in layerPriorities) 
		{
			foreach (RaycastHit hit in raycastHits) 
			{
				if (hit.collider.gameObject.layer == layer) 
				{
					return hit;//stop looking
				}
			}
		}
		return null;// because connot use Gameobject? nullable
	}
}
}
