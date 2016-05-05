using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RunnerGame
{
	/// <summary>
	/// Object pooler.
	/// </summary>
	public class ObjectPooler : MonoBehaviour
	{
		public static ObjectPooler PoolInstance;

		/// <summary>
		/// Awake this instance.
		/// </summary>
		protected virtual void Awake()
		{
			PoolInstance = this;
		}

		/// <summary>
		/// Start this instance.
		/// </summary>
		protected virtual void Start()
		{
			PopulateObjectPool ();
		}
		/// <summary>
		/// Populates the object pool.
		/// </summary>
		protected virtual void PopulateObjectPool()
		{
			return;
		}
		/// <summary>
		/// Gets the game object pooled.
		/// </summary>
		/// <returns>The game object pooled.</returns>
		public virtual GameObject GetGameObjectPooled()
		{
			return null;
		}
	}
}

