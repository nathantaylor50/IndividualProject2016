using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

namespace RunnerGame
{
	/// <summary>
	/// Start screen.
	/// </summary>
	public class StartScreen : MonoBehaviour
	{


		/// name of the intended level destination
		public string TargetLevelName;

		// singleton
		static public StartScreen Instance { get { return instance; } }
		static protected StartScreen instance;

		// Use this for initialization
		void Awake ()
		{
			instance = this;
		}
		
		/// <summary>
		/// on every frame check for correct input
		/// </summary>
		protected virtual void Update () 
		{
			if (Input.GetButtonDown("MainAction")) { GoToLevel(); }
		}

		/// <summary>
		/// Loads the level specified in parameters 
		/// </summary>
		public virtual void GoToLevel()
		{
			LoadingSceneManager.LoadGameScene (TargetLevelName);
		}
	}

}