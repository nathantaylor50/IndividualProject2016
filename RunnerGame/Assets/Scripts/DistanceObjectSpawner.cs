using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RunnerGame
{
	/// <summary>
	/// Distance object spawner.
	/// </summary>
	public class DistanceObjectSpawner : ObjectSpawner
	{
		[Header("Gap between objects")]
		/// the minimum gap between two spawned objects
		public Vector3 MinGap = new Vector3(1,1,1);
		///maximum gap between two spawned objects
		public Vector3 MaxGap = new Vector3(1,1,1);
		[Space(10)]
		[Header("Y Spawn Position Range")]
		///minimum Y position range we can spawn the object at
		public float MinYPosition;
		///maximum Y position range we can spawn the object at
		public float MaxYPosition;
		[Space(10)]
		[Header("Z Spawn Position Range")]
		///minimum Z position range we can spawn the object at
		public float MinZPosition;
		///maximum Z position range we can spawn the object at
		public float MaxZPosition;
		[Space(10)]
		[Header("Spawn Rotation")]
		///if true spawned object will be rotated towards the direction in which it spawned
		public bool ObjectRotatedToSpawnDirection = true;

		protected Transform lastSpawnedTransform;
		protected float NextSpawnDistance;

		/// <summary>
		/// Start this instance.
		/// </summary>
		protected virtual void Start()
		{
			//get the object pooler component
			objectPooler = GetComponent<ObjectPooler> ();
		}

		/// <summary>
		/// Update this instance.
		/// </summary>
		protected virtual void Update()
		{
			CheckObjectSpawn ();
		}
		/// <summary>
		/// Checks the object spawn.
		/// </summary>
		protected virtual void CheckObjectSpawn()
		{
			//spawner will only work when the game is running
			if (isGameOn) {
				if (GameManager.Instance.StateStatus != GameManager.GameStateStatus.GameRunning) {
					lastSpawnedTransform = null;
					return;
				}
			}


			//if nothing is spawned yet then first spawn
			if ((lastSpawnedTransform == null) || (!lastSpawnedTransform.gameObject.activeInHierarchy)) {
				DistanceObjectSpawn (transform.position + MiscTools.RandomVector3 (MinGap, MaxGap));
				return;
			}

			//if last spawned object's position is far enough away from origin then spawn next object
			if (transform.InverseTransformPoint(lastSpawnedTransform.position).x < - NextSpawnDistance)
			{

				Vector3 spawnPosition = transform.position;
				DistanceObjectSpawn (spawnPosition);
			}
	
		}


		/// <summary>
		/// spawns then determines next spawn position
		/// </summary>
		/// <param name="spawnPosition">Spawn position.</param>
		protected virtual void DistanceObjectSpawn(Vector3 spawnPos)
		{
			//spawn object at position parsed
			GameObject spawnedGameObject = spawn (spawnPos, false);

			//if no spawnobject is present in hirarchy then do a fresh spawn
			if (spawnedGameObject == null) {
				lastSpawnedTransform = null;
				NextSpawnDistance = UnityEngine.Random.Range (MinGap.x, MaxGap.x);
				return;
			}

			//checks for pool script attached to object
			if (spawnedGameObject.GetComponent<PoolableGameObject> () == null) {
				throw new Exception (gameObject.name + " No PoolableGameObject Present");					
			}

			//if theres a moving script attached rotate towards needed movement
			if (ObjectRotatedToSpawnDirection) {
				spawnedGameObject.transform.rotation *= transform.rotation;
			}
			//if this is a moveingobject then move in the specified direction
			if (spawnedGameObject.GetComponent<MovingGameObject> () != null) {
				spawnedGameObject.GetComponent<MovingGameObject> ().Direction = transform.rotation * Vector3.left;
			}

			//
			if (lastSpawnedTransform != null) {
				//center object posision to spawners poisiton
				spawnedGameObject.transform.position = transform.position;

				//get relative X distance between spawner and object
				float XDistanceBetweenSpawnerAndGameObject = transform.InverseTransformPoint(lastSpawnedTransform.position).x;

				//new object is aligned with the previous one
				//taking into acount the witdh of both objects
				spawnedGameObject.transform.position += transform.rotation
				* Vector3.right
				*(XDistanceBetweenSpawnerAndGameObject
				+ lastSpawnedTransform.GetComponent<PoolableGameObject> ().Size.x / 2
				+ spawnedGameObject.GetComponent<PoolableGameObject> ().Size.x / 2);

				//based on the values defined in the inspector clamp gap to object
				spawnedGameObject.transform.position += (transform.rotation * ClampedPosition(MiscTools.RandomVector3(MinGap, MaxGap)/2));

				//if spawned object is a mooving object, tell it to move
				if (spawnedGameObject.GetComponent<MovingGameObject> () != null) {
					spawnedGameObject.GetComponent<MovingGameObject> ().Move ();
				}
			}

			//tell object its spawning is finished
			spawnedGameObject.GetComponent<PoolableGameObject>().TriggerOnSpawnFinished();

			//decide when we should try spawning next object
			NextSpawnDistance = spawnedGameObject.GetComponent<PoolableGameObject>().Size.x/2;
			//store spawned object, which will be used for next spawn
			lastSpawnedTransform = spawnedGameObject.transform;
		
		}


		/// <summary>
		/// Clampeds the position.
		/// </summary>
		/// <returns>The position.</returns>
		/// <param name="vectorToClamp">Vector to clamp.</param>
		protected virtual Vector3 ClampedPosition (Vector3 vectorToClamp) {
			vectorToClamp.y = Mathf.Clamp (vectorToClamp.y, MinYPosition, MaxYPosition);
			vectorToClamp.z = Mathf.Clamp (vectorToClamp.z, MinZPosition, MaxZPosition);
			return vectorToClamp;
		}	

	}
}

