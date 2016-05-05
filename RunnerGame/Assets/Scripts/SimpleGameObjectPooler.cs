using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RunnerGame
{
	public class SimpleGameObjectPooler : ObjectPooler
	{
		/// the game object we'll instantiate 
		public GameObject GameObjectToPool;
		/// the number of objects we'll add to the pool
		public int PoolSize = 20;
		/// if true, the pool will automatically add objects to the itself if needed
		public bool PoolCanGrow = true;

		/// this object is just used to group the pooled objects
		protected GameObject PoolGroup;
		/// the actual object pool
		protected List<GameObject> pooledGameObjects;

		/// <summary>
		/// Fills the object pool with the gameobject type you've specified in the inspector
		/// </summary>
		protected override void PopulateObjectPool()
		{
			// we create a container that will hold all the instances we create
			PoolGroup = new GameObject("[SimpleGameObjectPooler] " + this.name);

			// we initialize the list we'll use to 
			pooledGameObjects = new List<GameObject>();

			// we add to the pool the specified number of objects
			for (int i = 0; i < PoolSize; i++)
			{
				AddOneObjectToThePool ();
			}
		}

		/// <summary>
		/// This method returns one inactive object from the pool
		/// </summary>
		/// <returns>The pooled game object.</returns>
		public override GameObject GetGameObjectPooled()
		{
			// we go through the pool looking for an inactive object
			for (int i=0; i< pooledGameObjects.Count; i++)
			{
				if (!pooledGameObjects[i].gameObject.activeInHierarchy)
				{
					// if we find one, we return it
					return pooledGameObjects[i];
				}
			}
			// if we haven't found an inactive object (the pool is empty), and if we can extend it, we add one new object to the pool, and return it		
			if (PoolCanGrow)
			{
				return AddOneObjectToThePool();
			}
			// if the pool is empty and can't grow, we return nothing.
			return null;
		}

		/// <summary>
		/// Adds one object of the specified type (in the inspector) to the pool.
		/// </summary>
		/// <returns>The one object to the pool.</returns>
		protected virtual GameObject AddOneObjectToThePool()
		{
			if (GameObjectToPool==null)
			{
				Debug.LogWarning("The "+gameObject.name+" ObjectPooler doesn't have any GameObjectToPool defined.", gameObject);
				return null;
			}
			GameObject newGameObject = (GameObject)Instantiate(GameObjectToPool);
			newGameObject.gameObject.SetActive(false);
			newGameObject.transform.parent = PoolGroup.transform;
			newGameObject.name=GameObjectToPool.name+"-"+pooledGameObjects.Count;
			pooledGameObjects.Add(newGameObject);
			return newGameObject;
		}
	}
}