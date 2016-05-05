using UnityEngine;
using System.Collections;
using System;

namespace RunnerGame
{
	/// <summary>
	/// Poolable game object.
	/// </summary>
	public class PoolableGameObject : ObjectBounds
	{
		public delegate void Events();
		public event Events OnSpawnFinished;

		///
		public float ObjectLifeTimer = 0.0f;

		/// <summary>
		/// Destroy this instance.
		/// </summary>
		public virtual void Destroy()
		{
			gameObject.SetActive (false);
		}

		/// <summary>
		/// Update this instance.
		/// </summary>
		protected virtual void Update()
		{

		}

		/// <summary>
		/// Raises the enable event.
		/// </summary>
		protected virtual void OnEnable()
		{
			Size = GetObjectBounds ().extents * 2;
			if (ObjectLifeTimer > 0) {
				Invoke ("Destroy", ObjectLifeTimer);
			}
		}

		/// <summary>
		/// Raises the disable event.
		/// </summary>
		protected virtual void OnDisable()
		{
			CancelInvoke ();
		}

		/// <summary>
		/// Raises the spawn finished event.
		/// </summary>
		public void TriggerOnSpawnFinished()
		{
			if (OnSpawnFinished != null) {
				OnSpawnFinished ();
			}
		}
	}
}

