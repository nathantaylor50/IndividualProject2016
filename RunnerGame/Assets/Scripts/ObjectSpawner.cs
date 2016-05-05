using UnityEngine;
using System;
using System.Collections;


namespace RunnerGame
{
	/// 

	[RequireComponent (typeof  (ObjectPooler))]
	/// <summary>
	/// Object spawner.
	/// </summary>
	public class ObjectSpawner : MonoBehaviour
	{
		[Header("Size Of Spawned Objects")]
		///minimum size of the spawned object
		public Vector3 MinSize = new Vector3 (1,1,1);
		///the mx size of the spawned object
		public Vector3 MaxSize = new Vector3 (1,1,1);
		///if true then preserve original ratio of the spawned object
		public bool PreserveOrigRatio = false;
		///for the editor
		[Space(10)] [Header("Rotation")]
		///minimum rotation of the spawned object
		public Vector3 MinRotation;
		///max rotation of spawned object
		public Vector3 MaxRotation;
		[Space(10)] [Header("Spawning")]
		///can spawn if true
		public bool Spawning = true;
		///if true game is running so spawning is allowed 
		public bool isGameOn = true;
		/// amount of time before the first initial spawn
		public float InitialSpawnDelay = 0.0f;

		protected ObjectPooler objectPooler;
		protected float spawnerStartTime;

		/// <summary>
		/// Awake this instance.
		/// </summary>
		protected virtual void Awake()
		{
			objectPooler = GetComponent<ObjectPooler> ();
			spawnerStartTime = Time.time;
		}

		/// <summary>
		/// Spawn the specified spawnPos and ObjectActive.
		/// </summary>
		/// <param name="spawnPos">Spawn position.</param>
		/// <param name="ObjectActive">If set to <c>true</c> object active.</param>
		public virtual GameObject spawn(Vector3 spawnPos, bool ObjectActive = true)
		{
			// 
			if (isGameOn)
			{
				if (GameManager.Instance.StateStatus != GameManager.GameStateStatus.GameRunning) {
					return null;
				}
			}
			if ((Time.time - spawnerStartTime < InitialSpawnDelay) || (!Spawning)) {
				return null;
			}

			///get next object in pool
			GameObject NextInPool = objectPooler.GetGameObjectPooled();
			if (NextInPool == null) { return null;}

			///re-scale object
			Vector3 newObjectScale;
			if (!PreserveOrigRatio) {
				newObjectScale = new Vector3 (UnityEngine.Random.Range (MinSize.x, MaxSize.x),
					UnityEngine.Random.Range (MinSize.y, MaxSize.y), UnityEngine.Random.Range (MinSize.z, MaxSize.z));
			} else {
				newObjectScale = Vector3.one * UnityEngine.Random.Range (MinSize.x, MaxSize.x);
			}
			NextInPool.transform.localScale = newObjectScale;

			//error report
			if (NextInPool.GetComponent<PoolableGameObject> () == null) {
				throw new Exception (gameObject.name + " is trying to spawn gameObjects that doent have a correct pool component (PoolableGameObject) attached.");
			}

			//position object in pool
			NextInPool.transform.position = spawnPos;

			//set objects rotation
			NextInPool.transform.eulerAngles = new Vector3 (
				UnityEngine.Random.Range (MinRotation.x, MaxRotation.x),
				UnityEngine.Random.Range (MinRotation.y, MaxRotation.y),
				UnityEngine.Random.Range (MinRotation.z, MaxRotation.z)
			);

			//set object to active
			NextInPool.gameObject.SetActive(true);

			if (ObjectActive) {
				if (NextInPool.GetComponent<PoolableGameObject> () != null) {
					NextInPool.GetComponent<PoolableGameObject> ().TriggerOnSpawnFinished ();
				}

			}
			return (NextInPool);
		}
	}
}

