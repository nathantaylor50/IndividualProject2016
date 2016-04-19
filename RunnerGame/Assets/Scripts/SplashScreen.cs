using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

namespace RunnerGame
{


	/// <summary>
	/// splash screen class
	/// </summary>
	public class SplashScreen : MonoBehaviour
	{
		public string FirstLevel;
		public float AutoSkipDelay = 2.0f;
		protected float delayAfterClick = 1f;

		/// <summary>
		/// Start this instance.
		/// </summary>
		void Start ()
		{
			GUIManager.Instance.FadeEffectOn (false, 1.0f);

			if (AutoSkipDelay > 1.0f) {
				delayAfterClick = AutoSkipDelay;
				StartCoroutine (LoadFirstLevel ());
			}
		}
		
		/// <summary>
		/// Loads the first level.
		/// </summary>
		/// <returns>The first level.</returns>
		protected virtual IEnumerator LoadFirstLevel()
		{
			yield return new WaitForSeconds (delayAfterClick);
			GUIManager.Instance.FadeEffectOn (true, 1.0f);
			yield return new WaitForSeconds (1.0f);
			SceneManager.LoadScene (FirstLevel);
		}


	}

}