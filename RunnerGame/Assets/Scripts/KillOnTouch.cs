using UnityEngine;
using System.Collections;

namespace RunnerGame
{
	/// <summary>
	/// Kill on touch.
	/// in this context uses the collider attached as a trigger
	/// and kills objects with player tag upon contact
	/// </summary>
	public class KillOnTouch : MonoBehaviour
	{
		/// <summary>
		/// Raises the trigger collision event.
		/// </summary>
		/// <param name="collider">Collider.</param>
		protected virtual void OnTriggerCollision (Collider collider){

		}

		/// <summary>
		/// Triggers the collision.
		/// </summary>
		/// <param name="collidingGameObject">Colliding game object.</param>
		protected virtual void TriggerCollision (GameObject collidingGameObject)
		{
			//if colliding object does not have the player tag, do nothing
			if (collidingGameObject.tag != "Player") { return;}

			PlayableCharacter player = collidingGameObject.GetComponent<PlayableCharacter> ();
			if (player == null) {
				return;
			}

			//tell the level manager to kill the player
			LevelManager.Instance.KillPlayer(player);
		}
	}
}

