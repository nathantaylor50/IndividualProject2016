using System.Collections;
using UnityEngine;

namespace RunnerGame
{
	/// <summary>
	/// Auto destroy timer.
	/// </summary>
	public class AutoDestroyTimer : MonoBehaviour
	{
		
		/// The timer for destruction.
		public float TimerForDestruction = 2.0f;

		/// <summary>
		/// Start this instance.
		/// </summary>
		protected virtual void Start()
		{
			StartCoroutine (Destroy ());
		}

		/// <summary>
		/// Destroy this instance after TimerForDestructions time
		/// </summary>
		protected virtual IEnumerator Destroy()
		{
			yield return new WaitForSeconds (TimerForDestruction);
			Destroy (gameObject);
		}
	}
}

