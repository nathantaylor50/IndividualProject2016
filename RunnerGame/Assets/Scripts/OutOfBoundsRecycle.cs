using UnityEngine;
using System.Collections;

namespace RunnerGame
{
	[RequireComponent (typeof (PoolableGameObject))]
	/// <summary>
	/// Out of bounds recyle.
	/// </summary>
	public class OutOfBoundsRecycle : MonoBehaviour
	{
		///the distance a object can go beyond the bounds before being destroyed
		public float OutOfBoundsDistance = 5.0f;

		/// <summary>
		/// Update this instance.
		/// </summary>
		protected virtual void Update()
		{
			if (LevelManager.Instance.CheckRecycleObjectBounds (GetComponent<PoolableGameObject> ().GetObjectBounds (), OutOfBoundsDistance)) {
				GetComponent<PoolableGameObject> ().Destroy ();
			}
		}
	}
}

