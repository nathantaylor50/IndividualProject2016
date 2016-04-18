using System.Collections;
using UnityEngine;

namespace RunnerGame
{
	/// <summary>
	/// A class that Auto destroys a gameObject that its attached to after a specified time
	/// </summary>
	public class AutoDestroyTimer : MonoBehaviour
	{
		
		/// The amount of time before destruction
		public float TimerForDestruction = 2.0f;

		/// <summary>
		/// Start this instance, and 
		/// </summary>
		protected virtual void Start ()
		{
			//returns a Cotoutine of the method Destroy
			StartCoroutine (Destroy ());
		}

		/// <summary>
		/// Destroy this instance after TimerForDestructions time
		/// </summary>
		protected virtual IEnumerator Destroy ()
		{
			//yield makes the coroutine wait fot the waitforsecs to finish
			yield return new WaitForSeconds (TimerForDestruction);
			//destroys the object its attached to
			Destroy (gameObject);
		}
	}
}

