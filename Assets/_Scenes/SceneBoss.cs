using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Core 
{
	public class SceneBoss : MonoBehaviour
	{

		public int GetCurrentSceneInt ()
		{
			Scene currentScene = SceneManager.GetActiveScene ();
			return currentScene.buildIndex; 
		}
		public void ReloadCurrentScene()
		{
			SceneManager.LoadScene (GetCurrentSceneInt ()); 
		}
	}
}
